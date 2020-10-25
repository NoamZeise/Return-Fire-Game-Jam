using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites.Powerups
{
    public abstract class Powerup : Sprite, ICloneable
    {
        public Powerup(Vector2 position, Texture2D texture) : base(position, texture)
        {
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _position.Y += 100f * (float)gameTime.ElapsedGameTime.TotalSeconds; 
        }
         
        public void setPosition(Vector2 position) =>
            _position = position;

        public abstract float[] returnPattenParameters();

        public void Remove() =>
            isRemoved = true;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
