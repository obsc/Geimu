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

namespace Geimu {

    /// <summary>
    /// This is the main type for your game
    /// </summary>

    public class GameEngine : Microsoft.Xna.Framework.Game {
        // Graphics Managers
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        // TODO: audio

        // Boundaries of the window
        protected Rectangle bounds;

        // Square objects
        protected Square square0, square1;
        protected SquareController squareControl0, squareControl1;

        public GameEngine() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            bounds = this.Window.ClientBounds;

            square0 = new Square(200, 300, 0, bounds);
            square1 = new Square(500, 300, 1, 0.75f, 0.0f, bounds);

            squareControl0 = square0.controller;
            squareControl1 = square1.controller;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Square.LoadContent(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
#if XBOX
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) {
#else
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
#endif
                this.Exit();
            }

            // TODO: Add your update logic here
            squareControl0.readInput();
            squareControl1.readInput();

            square0.Update();
            square1.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            square0.Draw(spriteBatch);
            square1.Draw(spriteBatch);

            base.Draw(gameTime);
        }

    }

}
