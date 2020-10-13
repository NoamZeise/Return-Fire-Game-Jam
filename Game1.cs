using Devtober_2020.sprites;
using Devtober_2020.sprites.Units;
using Devtober_2020.sprites.Units.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
        BasicEnemy basicEnemy;
        List<Sprite> sprites;
        SpriteFont Font;

        double enemyTimer = 0;
        static double ENEMY_SPAWN_RATE = 5;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.ApplyChanges();
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
            basicEnemy = new BasicEnemy(new Vector2(200, -30), Content.Load<Texture2D>("Sprites/player"), bullet);
            Font = Content.Load<SpriteFont>("Font");

            sprites.Add(player);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            UpdateSprites(gameTime);

            

            enemyTimer += gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        private void UpdateSprites(GameTime gameTime)
        {
            if(enemyTimer > ENEMY_SPAWN_RATE)
            {
                enemyTimer = 0;
                sprites.Add(basicEnemy.Clone() as BasicEnemy);
            }    
            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].Update(gameTime, sprites);
                if (sprites[i] is Enemy)
                    if ((sprites[i] as Enemy).ReachedBottom)
                        player.TakeDamage();
                if (sprites[i].isRemoved)
                    sprites.RemoveAt(i--);
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

            foreach (var sprite in sprites)
                sprite.Draw(spriteBatch);

            spriteBatch.DrawString(Font, "Ammo : " + player.Ammo, new Vector2(20, 20), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(Font, "Health : " + player.health, new Vector2(20, 40), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
