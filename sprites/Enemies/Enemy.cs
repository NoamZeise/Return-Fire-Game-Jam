using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites.Enemies
{
    class Enemy : Sprite
    {
        Bullet _bullet;

        private static double fireDelay = 400;
        private double fireTimer = 0;
        public Enemy(Vector2 position, Texture2D texture, Bullet bullet) : base(position, texture)
        {
            _bullet = bullet;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Shoot(sprites);
            fireTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        void Shoot(List<Sprite> sprites)
        {
            if (fireTimer > fireDelay)
            {
                fireTimer = 0;
                _bullet.Shoot(new Vector2(_position.X + _texture.Width / 2 - _bullet.Rectangle.Width / 2, _position.Y), new Vector2(0, 400f), this);

                sprites.Add(_bullet.Clone() as Bullet);
            }
        }
    }
}
