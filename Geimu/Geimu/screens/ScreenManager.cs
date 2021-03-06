﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Geimu {

    // Class for managing game screens
    public class ScreenManager {
        // List of current screens
        protected List<Screen> screens = new List<Screen>();

        // Lists of screens to remove and add
        protected List<Screen> removeList = new List<Screen>();
        protected List<Screen> addList = new List<Screen>();

        // Managers
        public GameEngine game {
            get;
            protected set;
        }

        public GraphicsDevice graphicsDevice {
            get { return game.GraphicsDevice; }
        }

        public ContentManager contentManager {
            get { return game.Content; }
        }

        public SpriteBatch spriteBatch {
            get;
            protected set;
        }

        public ScreenReference screenReference {
            get;
            protected set;
        }

        public DataReference dataReference {
            get { return game.dataReference; }
        }

        // Game boundaries
        protected Rectangle mBounds;

        public Rectangle bounds {
            get { return mBounds; }
        }

        // Input state
        public InputState input = new InputState();

        // Constructs a new screen manager
        public ScreenManager(GameEngine gameEngine) {
            game = gameEngine;
            screenReference = new ScreenReference();
        }

        // Starts up the game with the main menu
        public void Initialize() {
            mBounds = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            AddScreen(screenReference.menuScreen);
        }

        // Creates a new sprite batch
        public void LoadContent() {
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        // Updates each screen
        public void Update(GameTime gameTime) {
            input.Update();

            foreach (Screen s in removeList) {
                screens.Remove(s);
            }
            removeList.Clear();

            foreach (Screen s in addList) {
                screens.Add(s);
            }
            addList.Clear();

            foreach (Screen s in screens) {
                if (s.updateState) {
                    s.HandleInput(input);
                    s.Update(gameTime);
                }
            }
        }

        // Draws each screen
        public void Draw(GameTime gameTime) {
            foreach (Screen s in screens) {
                if (s.drawState)
                    s.Draw(gameTime);
            }
        }

        // Adds a screen to the top of the screen manager
        public void AddScreen(Screen screen) {
            screen.screenManager = this;
            screen.Initialize();

            screen.LoadContent();

            addList.Add(screen);
        }

        // Removes a screen from the screen manager
        public void RemoveScreen(Screen screen) {
            screen.UnloadContent();

            removeList.Add(screen);
        }

        // Removes all current screens
        public void RemoveAll() {
            foreach (Screen s in screens) {
                RemoveScreen(s);
            }
        }

    }

}
