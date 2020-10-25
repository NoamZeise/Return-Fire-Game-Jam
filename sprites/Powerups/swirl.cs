using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites.Powerups
{
    class Swirl : Powerup
    {
        public Swirl(Vector2 position, Texture2D texture) : base(position, texture)
        {
        }

        public override float[] returnPattenParameters()
        {
            float shooterNum = 6;
            float delay = 0.17f;
            float spin = 2500;
            float speed = 60f;
            float separation = 60;
            float initalRotation = 180;

            Remove();
            return new float[6] { shooterNum, delay, spin, speed, separation, initalRotation };
        }
    }
}
