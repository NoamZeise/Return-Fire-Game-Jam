using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.sprites.Powerups
{
    class Triple : Powerup
    {
        public Triple(Vector2 position, Texture2D texture) : base(position, texture)
        {
        }

        public override float[] returnPattenParameters()
        {
            float shooterNum = 3;
            float delay = 0.3f;
            float spin = 0;
            float speed = 300f;
            float separation = 15;
            float initalRotation = 165;


            Remove();
            return new float[6] {shooterNum, delay, spin, speed, separation, initalRotation };
            
        }
    }
}
