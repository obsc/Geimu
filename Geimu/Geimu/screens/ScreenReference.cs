﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geimu {

    public class ScreenReference {
        // References to screens
        protected MainMenu menuScreen;
        protected OptionsScreen optionsScreen;
        protected GameScreen gameScreen;
        protected PauseScreen pauseScreen;
        protected EndScreen endScreen;

        public MainMenu menu {
            get { return menuScreen; }
        }

        public OptionsScreen options {
            get { return optionsScreen; }
        }

        public GameScreen game {
            get { return gameScreen; }
        }

        public PauseScreen pause {
            get { return pauseScreen; }
        }

        public EndScreen end {
            get { return endScreen; }
        }

        public ScreenReference() {
            menuScreen = new MainMenu();
            optionsScreen = new OptionsScreen();
            gameScreen = new GameScreen();
            pauseScreen = new PauseScreen();
            endScreen = new EndScreen();
        }

    }

}
