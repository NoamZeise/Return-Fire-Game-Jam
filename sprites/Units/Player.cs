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
        private static double FIRE_DELAY = 250;
        private double fireTimer = 0;
        public int Ammo { get; private set; }
        private KeyboardState previous;
        public Player(Vector2 position, Texture2D texture, Bullet bullet) : base(position, texture, bullet)
        {
            health = 3;
            Ammo = 0;
            depth = 0.9f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _velocity = Vector2.Zero;
            Controls(sprites);
            updatePosition((float)gameTime.ElapsedGameTime.TotalSeconds);
            _position.X = MathHelper.Clamp(_position.X, 0, Game1.SCREEN_WIDTH - Rectangle.Width);
            _position.Y = MathHelper.Clamp(_position.Y, 0, Game1.SCREEN_HEIGHT - Rectangle.Height);
            if (checkBulletCollisions(sprites))
            {
                if (_shooting)
                    health--;
                else
                    Ammo++;
            }

            if (health <= 0)
                isRemoved = true;

            if (_shooting)
                colour = Color.BlueViolet;
            else
                colour = Color.DarkGray;

            previous = Keyboard.GetState();
            fireTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
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

        protected override void Shoot(List<Sprite> sprites)
        {
            if(fireTimer > FIRE_DELAY && Ammo > 0 && _shooting)
            {
                fireTimer = 0;
                Ammo--;
                base.Shoot(new Vector2(0, -500f));
                sprites.Add(_bullet.Clone() as Bullet);
            }
        }

        public void TakeDamage() =>
            health--;
    }
}
