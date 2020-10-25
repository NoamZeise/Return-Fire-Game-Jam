using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.Menu
{
    public class Button
    {
        private MouseState mousePos;
        private MouseState prevMousePos;
        private bool hovering = false;
        private Color TextColour;

        public Texture2D Texture;
        public Texture2D Selected;
        public Vector2 Position;
        public EventHandler OnClick;
        public bool clicked;

        public string Text;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }
        public Button(Texture2D texture, Texture2D selected, Vector2 position)
        {
            Texture = texture;
            Selected = selected;
            Position = position;
            TextColour = Color.Black;
        }
        public void Update(GameTime gameTime)
        {

            mousePos = Mouse.GetState();

            var mouseRect = new Rectangle(mousePos.X, mousePos.Y, 1, 1);

            hovering = false;

            if (mouseRect.Intersects(Rectangle))
            {
                hovering = true;

                if (mousePos.LeftButton == ButtonState.Released && prevMousePos.LeftButton == ButtonState.Pressed)
                {
                    OnClick?.Invoke(this, new EventArgs());
                }
            }

            prevMousePos = mousePos;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            if (hovering)
                spriteBatch.Draw(Texture, Position, Color.White);
            else
                spriteBatch.Draw(Selected, Position, Color.White);
        }
    }
}
