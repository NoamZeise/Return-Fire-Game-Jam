using Devtober_2020.Controls;
using Devtober_2020.sprites.Powerups;
using Devtober_2020.sprites;
using Devtober_2020.sprites.Units;
using Devtober_2020.sprites.Units.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Devtober_2020.Menu;
using System.Threading;

namespace Devtober_2020
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int SCREEN_WIDTH = 640;
        public static int SCREEN_HEIGHT = 480;

        Player player;
        Bullet bullet;
        List<Sprite> sprites;
        SpriteFont Font;

        EnemySpawnManager spawnManager;

        Random rnd = new Random();
        List<Powerup> powerups;
        hpUp hpPower;
        int powerUpElimintaions = 0;
        int currentElimintaions = 0;

        bool inMenu = true;
        bool showHelp = false;
        KeyboardState previousKey;
        Texture2D menuScreen;
        Texture2D helpScreen;
        Button Play;
        Button Help;
        Button Exit;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.ApplyChanges();

            powerUpElimintaions = rnd.Next(3, 7);
            powerups = new List<Powerup>();

            menuScreen = Content.Load<Texture2D>("Menu/menuscreen");
            helpScreen = Content.Load<Texture2D>("Menu/helpscreen");

            Play = new Button(Content.Load<Texture2D>("Menu/play-unselected"), Content.Load<Texture2D>("Menu/play-selected"), new Vector2(0, 240));
            Play.OnClick += Play_Click;

            Exit = new Button(Content.Load<Texture2D>("Menu/exit-unselected"), Content.Load<Texture2D>("Menu/exit-selected"), new Vector2(0, 400));
            Exit.OnClick += Exit_Click;

            Help = new Button(Content.Load<Texture2D>("Menu/help-unselected"), Content.Load<Texture2D>("Menu/help-selected"), new Vector2(0, 320));
            Help.OnClick += Help_Click;
        }

        private void Play_Click(object sender, EventArgs e)
        {
            inMenu = false;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Exit();
        }
        private void Help_Click(object sender, EventArgs e)
        {
            showHelp = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sprites = new List<Sprite>();
            bullet = new Bullet(Vector2.Zero, Content.Load<Texture2D>("Sprites/bullet"));
            player = new Player(new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT - 100), Content.Load<Texture2D>("Sprites/player"), bullet);
            Font = Content.Load<SpriteFont>("Font");
            spawnManager = new EnemySpawnManager(new Enemy(new Vector2(200, -30), Content.Load<Texture2D>("Sprites/player"), bullet));
            sprites.Add(player);

            powerups.Add(new Triple(Vector2.Zero, Content.Load<Texture2D>("Sprites/PowerUp/puTriple")));
            powerups.Add(new Rapid(Vector2.Zero, Content.Load<Texture2D>("Sprites/PowerUp/rapid")));
            powerups.Add(new Swirl(Vector2.Zero, Content.Load<Texture2D>("Sprites/PowerUp/swirl")));
            hpPower = new hpUp(Vector2.Zero, Content.Load<Texture2D>("Sprites/PowerUp/hp"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && previousKey.IsKeyUp(Keys.Escape))
            {
                if (showHelp)
                    showHelp = false;
                else
                {
                    inMenu = !inMenu;
                }
            }
            if (inMenu)
            {
                IsMouseVisible = true;
                Play.Update(gameTime);
                Exit.Update(gameTime);
                Help.Update(gameTime);
            }
            else
            {
                IsMouseVisible = false;
                spawnManager.Update(gameTime, sprites);
                UpdateSprites(gameTime);
                base.Update(gameTime);
            }

            previousKey = Keyboard.GetState();
        }

        private void UpdateSprites(GameTime gameTime)
        {
            
            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].Update(gameTime, sprites);
                if (sprites[i] is Player)
                    foreach (var sprite in sprites)
                    {
                        if (sprite is Enemy)
                            if (sprite.Rectangle.Intersects(sprites[i].Rectangle))
                            {
                                player.TakeDamage();
                                (sprite as Enemy).playerCollision();
                            }
                        if(sprite is Powerup)
                        {
                            if(sprite.Rectangle.Intersects(sprites[i].Rectangle))
                            {
                                player.powerUp((sprite as Powerup).returnPattenParameters());
                            }
                        }
                        if(sprite is hpUp)
                        {
                            if (sprite.Rectangle.Intersects(sprites[i].Rectangle))
                            {
                                player.hpUp();
                                (sprite as hpUp).Remove();
                            }
                        }
                    }
                if (sprites[i] is Enemy)
                {
                    if ((sprites[i] as Enemy).ReachedBottom)
                        player.TakeDamage();
                    if(sprites[i].isRemoved) //check to release powerup
                    {
                        currentElimintaions++;
                        if(currentElimintaions >= powerUpElimintaions)
                        {
                            spawnPowerUp(sprites[i].Rectangle.Center.ToVector2());
                            currentElimintaions = 0;
                            powerUpElimintaions = rnd.Next(3, 7);
                        }
                    }
                }
                if (sprites[i].isRemoved)
                    sprites.RemoveAt(i--);

            }
            if(player.isRemoved)
            {
                inMenu = true;
                sprites.Clear();
                player.Reset(new Vector2(SCREEN_WIDTH / 2, SCREEN_HEIGHT - 100));
                spawnManager.Reset();
                sprites.Add(player);
            }
        }

        private void spawnPowerUp(Vector2 position)
        {
            if (rnd.Next(5) == 1)
            {
                hpUp hpup = hpPower.Clone() as hpUp;
                hpup.setPosition(position);
                sprites.Add(hpup);
            }
            else
            {
                Powerup pUp = powerups[rnd.Next(powerups.Count)].Clone() as Powerup;
                pUp.setPosition(position);
                sprites.Add(pUp);
            }
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            if (inMenu)
            {
                spriteBatch.Draw(menuScreen, Vector2.Zero, Color.White);
                Play.Draw(gameTime, spriteBatch);
                Exit.Draw(gameTime, spriteBatch);
                Help.Draw(gameTime, spriteBatch);
                if(showHelp)
                    spriteBatch.Draw(helpScreen, Vector2.Zero, Color.White);
            }
            else
            {
                foreach (var sprite in sprites)
                    sprite.Draw(spriteBatch);

                spriteBatch.DrawString(Font, "Ammo : " + player.Ammo, new Vector2(20, 20), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Font, "Health : " + player.health, new Vector2(20, 40), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
