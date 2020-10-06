using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites
{
    class Player : Sprite
    {
        private Vector2 _velocity;
        private float _speed = 300f;
        private bool _shooting = false;
        private static double fireDelay = 100;
        private double fireTimer = 0;
        public int Ammo { get; private set; }

        private KeyboardState previous;
        Bullet _bullet;
        public Player(Vector2 position, Texture2D texture, Bullet bullet) : base(position, texture)
        {
            Ammo = 100;
            _bullet = bullet; 
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _velocity = Vector2.Zero;
            Controls(sprites);
            _position += new Vector2(_velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, _velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            
            previous = Keyboard.GetState();
            if (_shooting)
                colour = Color.Red;
            else
                colour = Color.White;

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
            if (Keyboard.GetState().IsKeyDown(Keys.X) && previous.IsKeyUp(Keys.X))
            {
                _shooting = !_shooting;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                Shoot(sprites);
            }
        }

        void Shoot(List<Sprite> sprites)
        {
            if(fireTimer > fireDelay && Ammo > 0 && _shooting)
            {
                fireTimer = 0;
                Ammo--;
                _bullet.Shoot(new Vector2(_position.X + _texture.Width / 2 - _bullet.Rectangle.Width / 2, _position.Y), new Vector2(0, -500f), this);

                sprites.Add(_bullet.Clone() as Bullet);
            }
        }
    }
}
