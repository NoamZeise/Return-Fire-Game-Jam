using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites
{
    class Background : Sprite
    {
        public Background(Vector2 position, Texture2D texture) : base(position, texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _position += new Vector2(0, 500 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (_position.Y >= this.Rectangle.Height)
                _position = Vector2.Zero;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
            spriteBatch.Draw(_texture, new Vector2(0, _position.Y - this.Rectangle.Height), Color.White);
        }

        public void Reset()
        {
            _position = Vector2.Zero;
        }
    }
}
