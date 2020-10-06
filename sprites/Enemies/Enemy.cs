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
        public Enemy(Vector2 position, Texture2D texture) : base(position, texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            throw new NotImplementedException();
        }
    }
}
