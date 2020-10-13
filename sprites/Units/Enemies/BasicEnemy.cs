using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites.Units.Enemies
{
    class BasicEnemy : Enemy, ICloneable
    {
        public BasicEnemy(Vector2 position, Texture2D texture, Bullet bullet) : base(position, texture, bullet)
        {
            _velocity = new Vector2(200f, 20f);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            movePattern();
            base.Update(gameTime, sprites);
        }

        protected override void movePattern()
        {
            if (_position.X <= 0)
                _velocity.X = Math.Abs(_velocity.X);
            if(_position.X >= Game1.SCREEN_WIDTH - Rectangle.Width)
                _velocity.X = -Math.Abs(_velocity.X);
        }

    }
}
