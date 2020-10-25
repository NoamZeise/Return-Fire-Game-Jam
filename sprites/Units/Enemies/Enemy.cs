using Devtober_2020.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites.Units.Enemies
{
    class Enemy : Unit, ICloneable
    {
        public bool ReachedBottom = false;

        public Pattern Pattern;
        public Enemy(Vector2 position, Texture2D texture, Bullet bullet) : base(position, texture, bullet)
        {
            depth = 0.8f;
            health = 5;
            bullet.setColour(Color.White);
            Pattern = new Pattern(this, _bullet, _position, 1, 3, 0f, 200f, 0, 0, false);
        }

        public object Clone()
        {
            Pattern = new Pattern(this, _bullet, _position, 1, 3, 0f, 200f, 0, 0, false);
            _velocity = new Vector2(200f, 20f);
            return this.MemberwiseClone();
        }

        public object Clone(Vector2 uPosition, Vector2 uVelocity, int hp)
        {
            _velocity = uVelocity;
            _position = uPosition;
            health = hp;
            return this.MemberwiseClone();
        }
        

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Pattern.Update(gameTime, sprites);

            updatePosition((float)gameTime.ElapsedGameTime.TotalSeconds);
            if (_position.Y > Game1.SCREEN_HEIGHT + Rectangle.Height)
            {
                isRemoved = true;
                ReachedBottom = true;
            }
            if (checkBulletCollisions(sprites))
            {
                health--;
            }
            if (health <= 0)
                isRemoved = true;

            movePattern();
        }


        public virtual void playerCollision() =>
            isRemoved = true;

        void movePattern()
        {
            if (_position.X <= 0)
                _velocity.X = Math.Abs(_velocity.X);
            if (_position.X >= Game1.SCREEN_WIDTH - Rectangle.Width)
                _velocity.X = -Math.Abs(_velocity.X);
        }

        public void setColor(Color color)
        {
            this.colour = color;
        }
    }
}
