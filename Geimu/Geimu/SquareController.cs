﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Geimu {

    // Class for reading input from keyboard
    public class SquareController {
        // Keyboard keys for different players
        public readonly Keys[,] PLAYER_KEYS = { { Keys.W, Keys.S, Keys.A, Keys.D, Keys.LeftShift },
                                                { Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.RightShift } };

        // Player id
        protected int mPlayer;

        // 1 = down/right, -1 = up/left, 0 = still
        private int mXdir;
        private int mYdir;
        
        // true = walking
        private bool mWalk;

        public int xDir {
            get { return mXdir; }
        }

        public int yDir {
            get { return mYdir; }
        }

        public bool walk {
            get { return mWalk; }
        }

        // Constructs a controller for a given player id
        public SquareController(int id) {
            if (id > 1) id = 0;
            mPlayer = id;
        }

        // Reads input from keyboard and updates fields
        public void readInput() {
            KeyboardState state = Keyboard.GetState();
            Keys up, down, left, right, slow;
            up = PLAYER_KEYS[mPlayer, 0];
            down = PLAYER_KEYS[mPlayer, 1];
            left = PLAYER_KEYS[mPlayer, 2];
            right = PLAYER_KEYS[mPlayer, 3];
            slow = PLAYER_KEYS[mPlayer, 4];

            mXdir = mYdir = 0;
            mWalk = false;

            if (state.IsKeyDown(up) && !state.IsKeyDown(down))
                mYdir = -1;
            else if (state.IsKeyDown(down) && !state.IsKeyDown(up))
                mYdir = 1;

            if (state.IsKeyDown(left) && !state.IsKeyDown(right))
                mXdir = -1;
            if (state.IsKeyDown(right) && !state.IsKeyDown(left))
                mXdir = 1;

            if (state.IsKeyDown(slow))
                mWalk = true;

        }

    }

}