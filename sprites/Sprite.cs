using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites
{
    public abstract class Sprite
    {
        protected Vector2 _position;
        protected Texture2D _texture;
        protected Color colour = Color.White;
        public bool isRemoved { get; protected set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
            }
        }
        public Sprite(Vector2 position, Texture2D texture)
        {
            isRemoved = false;
            _position = position;
            _texture = texture;
        }
        public abstract void Update(GameTime gameTime, List<Sprite> sprites);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, colour);
        }
        
    }
}
