using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites.Units.Enemies
{
    abstract class Enemy : Unit
    {

        protected double fireDelay = 400;
        private double fireTimer = 0;
        public Enemy(Vector2 position, Texture2D texture, Bullet bullet) : base(position, texture, bullet)
        {
            colour = Color.Tomato;
            depth = 0.8f;
            health = 5;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Shoot(sprites);
            updatePosition((float)gameTime.ElapsedGameTime.TotalSeconds);
            if (_position.Y > Game1.SCREEN_HEIGHT + 200)
                isRemoved = true;
            if (checkBulletCollisions(sprites))
            {
                health--;
            }
            if (health <= 0)
                isRemoved = true;

            fireTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        protected override void Shoot(List<Sprite> sprites)
        {
            if (fireTimer > fireDelay)
            {
                fireTimer = 0;
                base.Shoot(new Vector2(0, 400f));
                sprites.Add(_bullet.Clone() as Bullet);
            }
        }
        protected abstract void movePattern();
     }
}
