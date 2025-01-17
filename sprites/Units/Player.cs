﻿using Devtober_2020.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites.Units
{
    class Player : Unit
    {
        private float _speed = 300f;
        private bool _shooting = false;
        private double fireTimer = 0;
        public int Ammo { get; private set; }
        private KeyboardState previous;
        Pattern gun;
        float[] basicParameters;
        bool hasPowerUp = false;
        static double POWER_UP_TIME = 6;
        double powerUpTimer = 0;
        public Player(Vector2 position, Texture2D texture, Bullet bullet) : base(position, texture, bullet)
        {
            health = 3;
            Ammo = 0;
            depth = 0.9f;
            bullet.setColour(Color.LightBlue);
            gun = new Pattern(this, bullet, this._position, 1, 0.3 ,0f, 500f, 0, 180, true);
            basicParameters = new float[6] { 1, 0.3f, 0f, 500f, 0f, 180f};
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _velocity = Vector2.Zero;
            Controls(sprites);
            updatePosition((float)gameTime.ElapsedGameTime.TotalSeconds);
            _position.X = MathHelper.Clamp(_position.X, 0, Game1.SCREEN_WIDTH - Rectangle.Width);
            _position.Y = MathHelper.Clamp(_position.Y, 0, Game1.SCREEN_HEIGHT - Rectangle.Height);
            gun.Update(gameTime, sprites);
            if (checkBulletCollisions(sprites))
            {
                if (_shooting)
                    health--;
                else
                    Ammo++;
            }
            if(hasPowerUp)
            {
                if(powerUpTimer >= POWER_UP_TIME)
                {
                    hasPowerUp = false;
                    gun.setParameters(basicParameters);
                }
            }

            if (health <= 0)
                isRemoved = true;

            if (_shooting)
                colour = Color.BlueViolet;
            else
                colour = Color.DarkGray;

            previous = Keyboard.GetState();
            fireTimer += gameTime.ElapsedGameTime.TotalSeconds;
            powerUpTimer += gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void Controls(List<Sprite> sprites)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _velocity += new Vector2(0, -_speed);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _velocity += new Vector2(-_speed, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _velocity += new Vector2(0, _speed);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _velocity += new Vector2(_speed, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                Shoot(sprites);
                _shooting = true;
            }
            else
            {
                _shooting = false;
            }
        }

        protected void Shoot(List<Sprite> sprites)
        {
            if(fireTimer > gun._delay && Ammo > 0 && _shooting)
            {
                fireTimer = 0;
                Ammo--;
                gun.Shoot(sprites);
            }
        }

        public void TakeDamage() =>
            health--;

        public void powerUp(float[] parameters)
        {
            hasPowerUp = true;
            powerUpTimer = 0;
            gun.setParameters(parameters);
        }

        public void hpUp()
        {
            health++;
        }
        public void Reset(Vector2 position)
        {
            health = 3;
            Ammo = 0;
            _position = position;
            isRemoved = false;
        }

        public int getHealth =>
            (int)health;
    }
}
