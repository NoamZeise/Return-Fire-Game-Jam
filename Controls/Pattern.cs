using Devtober_2020.sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Devtober_2020.Controls
{
    class Pattern
    {
        double _timer;
        public double _delay;

        public int _shooterNum;
        public float _currentRotation = 45;
        public float _spin;
        public float _speed;
        public float _seperation;
        Vector2[] Velocities;
        public Bullet _bullet;
        public Sprite _parent;
        public Vector2 _position;
        bool manual = false;
        public Pattern(Sprite parent, Bullet bullet, Vector2 position, int shooterNum, double delay, float spin, float speed, float separation, float initialRotation, bool isManual)
        {
            _position = position;
            _shooterNum = shooterNum;
            Velocities = new Vector2[_shooterNum];
            _delay = delay;
            _timer = delay;
            _spin = spin;
            _speed = speed;
            _seperation = separation;
            _currentRotation = initialRotation;
            _bullet = bullet;
            _parent = parent;
            manual = isManual;
            setVelocites();
        }
        public Pattern(Pattern pattern, int shooterNum, double delay, float spin, float speed, float separation, float initialRotation)
        {
            _position = pattern._position;
            _shooterNum = shooterNum;
            Velocities = new Vector2[_shooterNum];
            _delay = delay;
            _timer = delay;
            _spin = spin;
            _speed = speed;
            _seperation = separation;
            _currentRotation = initialRotation;
            _parent = pattern._parent;
            _bullet = pattern._bullet;
            setVelocites();
        }


        public void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (_shooterNum < 1)
                _shooterNum = 1;
            if(!manual)
                _timer += gameTime.ElapsedGameTime.TotalSeconds;
            _currentRotation += _spin * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _currentRotation = normaliseAngle(_currentRotation);

            setVelocites();
            if(_timer > _delay && !manual)
            {
                _timer = 0;
                shoot(sprites);
            }

            _position = _parent.Rectangle.Center.ToVector2();
            _position.X -= _bullet.Rectangle.Width / 2;

            /*
            Console.WriteLine("Shooter Number: " + _shooterNum);
            Console.WriteLine("Speed: " + _speed);
            Console.WriteLine("Spin: " + _spin);
            Console.WriteLine("rotation: " + _currentRotation);
            Console.WriteLine("delay : " + _delay);
            Console.WriteLine("Separation: " + _seperation);
            */
        }

        void setVelocites()
        {
            float deltaAngle = _seperation;
            if(_seperation == 0)
                deltaAngle = 360f / (float)_shooterNum;
            float[] angles = new float[_shooterNum];
            Velocities = new Vector2[_shooterNum];
            for (int i = 0; i < _shooterNum; i++)
            {
                angles[i] = _currentRotation + (i * deltaAngle);
                angles[i] = normaliseAngle(angles[i]);

                Velocities[i].X = (float)Math.Sin(MathHelper.ToRadians(angles[i])) * _speed;
                Velocities[i].Y = (float)Math.Cos(MathHelper.ToRadians(angles[i])) * _speed;
            }
        }

        private static float normaliseAngle(float angle)
        {
            while (angle >= 360)
            {
                angle -= 360;
            }
            while (angle <= -360)
            {
                angle += 360;
            }
            return angle;
        }

        void shoot(List<Sprite> sprites)
        {
            for (int i = 0; i < _shooterNum; i++)
            {
                sprites.Add(_bullet.Clone(_position, Velocities[i], _parent) as Bullet);
            }
        }

        public void Shoot(List<Sprite> sprites)
        {
            shoot(sprites);
        }

        public void setParameters(float[] parameters)
        {
            //int shooterNum, double delay, float spin, float speed, float separation, float initialRotation
            _shooterNum = (int)parameters[0];
            _delay = (double)parameters[1];
            _spin = parameters[2];
            _speed = parameters[3];
            _seperation = parameters[4];
            _currentRotation = parameters[5];
        }
    }
}
