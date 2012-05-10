using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WeekendNightGames
{
    class PongGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Background textures for the game
        Texture2D powerBar;
        Texture2D pTablebackground;
        int currentPower = 100;
        int powerPhase = -1;
        bool takeShot = false;
        SoloCup[] pyramid;
        int xstart= 575;
        int ycupLocations = 130;
        int xmax = 750;
        int ymax = 250;
         public PongGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 1280; 
            this.graphics.PreferredBackBufferHeight = 720;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            pyramid = new SoloCup[10];
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


            // TODO: use this.Content to load your game content here

            //Load the screen backgrounds
            pTablebackground = Content.Load<Texture2D>("8bittable");
            powerBar = Content.Load<Texture2D>("powerbar") as Texture2D;

            //load cups

            for (int i = 0; i < 11; i++)
            { 
                Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
                pyramid[i].Initialize(Content.Load<Texture2D>("soloCup"), playerPosition); 
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed && takeShot == false)
            {
                takeShot = true;
                shoot();
            }
            updatePower();
            currentPower = (int)MathHelper.Clamp(currentPower, 0, 100);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            DrawTable();    
            //Draw the negative space for the health bar
            spriteBatch.Draw(powerBar, new Rectangle(100, 100, powerBar.Width, powerBar.Height), new Rectangle(0, 45, 0, powerBar.Height), Color.Gray);

            //Draw the current health level based on the current Health
            spriteBatch.Draw(powerBar, new Rectangle(100, 100, powerBar.Width, (int)(powerBar.Height * ((double)currentPower / 100))), new Rectangle(0, 45, 0, 0), Color.Blue);
            
            //Draw the box around the health bar
            spriteBatch.Draw(powerBar, new Rectangle(100, 100, powerBar.Width, powerBar.Height), new Rectangle(0, 0, 44, powerBar.Height), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void shoot()
        {
            //this is where the shot arc and such are drawn
        }

        private void updatePower()
        {
            if (takeShot == true)
                return;

            if (powerPhase == -1)
                currentPower--;
            else if (powerPhase == 1)
                currentPower++;

            if (currentPower == 0 || currentPower == 100)
            {
                powerPhase = powerPhase * -1;
                currentPower += powerPhase;
            }
            

 
        }
        private void DrawTable()
        {
            //Draw all of the elements that are part of the Controller detect screen
            spriteBatch.Draw(pTablebackground, Vector2.Zero, Color.White);
        }
    }
}