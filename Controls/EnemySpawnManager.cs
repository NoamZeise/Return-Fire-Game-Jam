using Devtober_2020.sprites;
using Devtober_2020.sprites.Units.Enemies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Devtober_2020.Controls
{
    class EnemySpawnManager
    {
        Random rnd = new Random();

        double patternSpawnTimer = 5;
        double patternSpawnDelay = 8;
        double timer = 0;
        double patternsSpawned = 0;
        int maxFile = 5;
        Dictionary<Enemy, double> enemyQueue;
        Enemy clonableEnemy;
        public EnemySpawnManager(Enemy enemy) 
        {
            clonableEnemy = enemy;
            enemyQueue = new Dictionary<Enemy, double>();
        }

        public void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (patternSpawnTimer > patternSpawnDelay)
                ImplimentPattern(gameTime, sprites);
            else
                patternSpawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void ImplimentPattern(GameTime gameTime, List<Sprite> sprites)
        {
            if (enemyQueue.Count == 0 && timer == 0)
                getPatternFromFile();
            else if (enemyQueue.Count == 0)
            {
                timer = 0;
                patternSpawnTimer = 0;
            }
            else
            {
                List<Enemy> removed = new List<Enemy>();
                foreach (var enemy in enemyQueue)
                {
                    if (enemy.Value <= timer)
                    {
                        sprites.Add(enemy.Key);
                        removed.Add(enemy.Key);
                    }
                }
                foreach (var enemy in removed)
                    enemyQueue.Remove(enemy);


                timer += gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void getPatternFromFile()
        {

            if (patternsSpawned > 8)
                maxFile = 8;
            if (patternsSpawned > 22)
                maxFile = 12;

            patternsSpawned++;


            int fileNum = rnd.Next(1, maxFile);

            if (patternsSpawned == 14)
            {
                fileNum = 8;
                maxFile = 9;
            }
            if (patternsSpawned == 28)
            {
                fileNum = 12;
                maxFile = 13;
            }
            //adjust spawn times based on file difficulty

            if (fileNum < 5 && maxFile < 9)
                patternSpawnDelay = 5;
            else if (fileNum < 5)
                patternSpawnDelay = 2;

            else if (fileNum < 9 && maxFile < 12)
                patternSpawnDelay = 7;
            else if (fileNum < 9)
                patternSpawnDelay = 5;

            else if (fileNum < 12)
                patternSpawnDelay = 12;

            XmlReader xmlR = XmlReader.Create("Patterns/"+ fileNum +".xml");

            while (xmlR.Read())
            {
                if (xmlR.Name == "enemy")
                {
                    int initalX = 0;
                    if (xmlR.GetAttribute("initialX") == "rnd")
                        initalX = rnd.Next(Game1.SCREEN_WIDTH - clonableEnemy.Rectangle.Width);
                    else
                        initalX = Convert.ToInt32(xmlR.GetAttribute("initialX"));
                    int hp = 5;
                    if (xmlR.GetAttribute("hp") != "")
                        hp = Convert.ToInt32(xmlR.GetAttribute("hp"));

                    Enemy clone = clonableEnemy.Clone(new Vector2((float)initalX, (float)Convert.ToDouble(xmlR.GetAttribute("initialY"))), new Vector2(randomPosNeg((float)Convert.ToDouble(xmlR.GetAttribute("velX"))), (float)Convert.ToDouble(xmlR.GetAttribute("velY"))), hp) as Enemy;
                    clone.Pattern = new Pattern(clone, clone._bullet, clone.Rectangle.Center.ToVector2(), Convert.ToInt32(xmlR.GetAttribute("shooter")), Convert.ToDouble(xmlR.GetAttribute("delay")), (float)Convert.ToDouble(xmlR.GetAttribute("spin")), (float)Convert.ToDouble(xmlR.GetAttribute("speed")), (float)Convert.ToDouble(xmlR.GetAttribute("separation")), (float)Convert.ToDouble(xmlR.GetAttribute("initial")), false);

                    if (hp <= 3)
                        clone.setColor(Color.LightGreen);
                    else if (hp <= 5)
                        clone.setColor(Color.Orange);
                    else if (hp <= 8)
                        clone.setColor(Color.PaleVioletRed);
                    else
                        clone.setColor(Color.DarkGray);

                    enemyQueue.Add(clone, Convert.ToDouble(xmlR.GetAttribute("start")));
                }
            }
        }

        float randomPosNeg(float value)
        {
            if (rnd.Next() % 2 == 0)
                return value;
            else
                return value * -1;
        }

        public void Reset()
        {
            patternSpawnTimer = 5;
            patternSpawnDelay = 8;
            timer = 0;
            patternsSpawned = 0;
            maxFile = 5;
            enemyQueue.Clear();
        }
    }
}
