﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Geimu {

    // Class for handling Projectile
    public class ProjectileQueue {
        // Max constants for a single queue
        public const int MAX_AGE = 120;
        public const int MAX_NUM = 64;

        // Scale of the projectile
        public const float SCALE = 0.25f;

        // Texture
        private static Texture2D sSprite;

        // Boundaries of the windows
        protected static Rectangle mBounds;

        // Size of the projectile
        protected static Rectangle mSize;

        // Color of the projectiles
        protected Color mTint;

        public Color tint {
            set { mTint = value; }
        }

        // Internal Queue
        protected Projectile[] queue;
        protected int head;
        protected int tail;
        protected int size;

        // Struct for a single projectile
        public struct Projectile {
            public Vector2 pos;
            public Vector2 vel;
            public int age;
        }

        // Constructs a new projectile queue
        public ProjectileQueue(Rectangle bounds) {
            mBounds = bounds;

            mTint = Color.White;

            queue = new Projectile[MAX_NUM];
            head = 0;
            tail = -1;
            size = 0;

            // Create a pool of projectiles
            for (int i = 0; i < MAX_NUM; i++) {
                queue[i] = new Projectile();
            }

        }

        // Loads texture into memory
        public static void LoadContent(ContentManager content) {
            sSprite = content.Load<Texture2D>("images\\Projectile");
            mSize = new Rectangle(0, 0, (int)(sSprite.Width * SCALE), (int)(sSprite.Height * SCALE));
        }

        // Adds a new projectile to the queue
        public void AddProjectile(Vector2 pos, Vector2 vel) {
            if (size == MAX_NUM) {
                head = ((head + 1) % MAX_NUM);
                size--;
            }

            tail = ((tail + 1) % MAX_NUM);
            queue[tail].pos = pos;
            queue[tail].vel = vel;
            queue[tail].age = 0;
            size++;
        }

        // Used for calculating rotations
        float VectorToAngle(Vector2 vector) {
            return (float)Math.Atan2(vector.X, -vector.Y);
        }

        // Updates each projectile
        public void Update() {
            // Removes old projectiles
            while (size > 0 && queue[head].age > MAX_AGE) {
                head = ((head + 1) % MAX_NUM);
                size--;
            }

            for (int i = 0; i < size; i++) {
                int id = ((head + i) % MAX_NUM);

                queue[id].pos += queue[id].vel;

                HandleWalls(id);

                queue[id].age++;
            }

        }

        private void HandleWalls(int id) {
            if (queue[id].pos.X < mBounds.X + mSize.Width / 2) {
                queue[id].pos.X = mBounds.X + mSize.Width / 2;
                queue[id].vel.X = -queue[id].vel.X;
            }
            else if (queue[id].pos.X > mBounds.Width - mSize.Width / 2) {
                queue[id].pos.X = mBounds.Width - mSize.Width / 2;
                queue[id].vel.X = -queue[id].vel.X;
            }

            if (queue[id].pos.Y < mBounds.Y + mSize.Height / 2) {
                queue[id].pos.Y = mBounds.Y + mSize.Height / 2;
                queue[id].vel.Y = -queue[id].vel.Y;
            }
            else if (queue[id].pos.Y > mBounds.Height - mSize.Height / 2) {
                queue[id].pos.Y = mBounds.Height - mSize.Height / 2;
                queue[id].vel.Y = -queue[id].vel.Y;
            }

        }

        // Draws each projectile
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Rectangle sector = new Rectangle(0, 0, sSprite.Width, sSprite.Height);
            Vector2 origin = new Vector2(sSprite.Width / 2, sSprite.Height / 2);

            for (int i = 0; i < size; i++) {
                int id = ((head + i) % MAX_NUM);

                float rotate = VectorToAngle(queue[id].vel) - (float)(Math.PI / 2);

                spriteBatch.Draw(sSprite, queue[id].pos, sector, mTint, rotate, origin, SCALE, SpriteEffects.None, 0);
            }

            spriteBatch.End();
        }

    }

}