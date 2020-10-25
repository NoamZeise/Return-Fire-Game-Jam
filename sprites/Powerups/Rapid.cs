using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites.Powerups
{
    class Rapid : Powerup
    {
        public Rapid(Vector2 position, Texture2D texture) : base(position, texture)
        {
        }

        public override float[] returnPattenParameters()
        {
            float shooterNum = 1;
            float delay = 0.17f;
            float spin = 0;
            float speed = 600f;
            float separation = 0;
            float initalRotation = 180;

            Remove();
            return new float[6] { shooterNum, delay, spin, speed, separation, initalRotation };
        }
    }
}
