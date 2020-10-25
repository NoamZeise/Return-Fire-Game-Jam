using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites
{
    class Bullet : Sprite, ICloneable
    {

        public Sprite Parent { get; private set; }
        private Vector2 _velocity = Vector2.Zero;
        
        public Bullet(Vector2 position, Texture2D texture) : base(position, texture)
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public object Clone(Vector2 position, Vector2 velocity, Sprite parent)
        {
            Shoot(position, velocity, parent);
            return this.MemberwiseClone();
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _position += new Vector2(_velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds, _velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (_position.Y > 1000 || _position.Y < -300 || _position.X > 1000 || _position.X < -300)
                isRemoved = true;
        }
        public void Shoot(Vector2 position, Vector2 velocity, Sprite parent)
        {
            _position = position;
            _velocity = velocity;
            Parent = parent;
        }

        public void collision(Sprite sprite)
        {
            isRemoved = true;
        }

        public void setColour(Color color)
        {
            colour = color;
        }
    }
}
