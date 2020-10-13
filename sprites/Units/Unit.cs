using Devtober_2020.sprites.Units.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites.Units
{
    abstract class Unit : Sprite
    {
        protected Bullet _bullet;
        protected Vector2 _velocity;
        public int health { get; protected set; }
        public Unit(Vector2 position, Texture2D texture, Bullet bullet) : base(position, texture)
        {
            _bullet = bullet;
        }

        protected bool checkBulletCollisions(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite is Bullet)
                {
                    if ((sprite as Bullet).Parent != this && sprite.Rectangle.Intersects(this.Rectangle))
                    {
                        if (this is Enemy && (sprite as Bullet).Parent is Enemy)
                            continue;
                        (sprite as Bullet).collision(this);
                        return true;
                    }
                }
            }
            return false;
        }

        protected void updatePosition(float elapsedTime) =>
                        _position += new Vector2(_velocity.X * elapsedTime, _velocity.Y * elapsedTime);

        protected void Shoot(Vector2 velocity) =>
            _bullet.Shoot(new Vector2(Rectangle.X + Rectangle.Width / 2 - _bullet.Rectangle.Width / 2, Rectangle.Y), velocity, this);
        protected abstract void Shoot(List<Sprite> sprites);
        
    }
}
