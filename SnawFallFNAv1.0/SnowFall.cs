using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnawFallFNAv1._0;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SnawFallFNAv1
{
    public class SnowFall : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graf;
        SpriteBatch sprite;
        private static KeyboardState keyboardState = Keyboard.GetState();
        private static KeyboardState lastKeyboardState;

        private static MouseState mouseState;
        Random rnd;
        private double seconds = 0;
        private int frames = 0;
        Texture2D imageTextureBack, imageTextureSnow;
        public List<Snow> snowflakes;
        public SnowFall()
        {
         
            rnd = new Random();
            graf = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graf.PreferredBackBufferWidth = 1280;
            graf.PreferredBackBufferHeight = 1024;
            graf.IsFullScreen = true;
            IsMouseVisible = true;
            graf.ApplyChanges();
            snowflakes = new List<Snow>();
            AddCreateSnow();

        }
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            sprite = new SpriteBatch(GraphicsDevice);
            imageTextureBack = Texture.Load("..\\Background1.png", Content);
            imageTextureSnow = Texture.Load("..\\Snow.png", Content);
        }

        protected override void Update(GameTime gameTime)
        {

           

            if (seconds >= 0.05)
            {
                keyboardState = Keyboard.GetState();

                mouseState = Mouse.GetState();

                foreach (var snowflake in snowflakes)
                {
                    snowflake.Y += snowflake.Severity - snowflake.Severity/2;
                    if (snowflake.Y > graf.PreferredBackBufferHeight)
                    {
                        snowflake.Y = -20;
                        snowflake.X = rnd.Next(0, graf.PreferredBackBufferWidth);
                    }
                }
                seconds = 0;
                if (KeyPressed(Keys.Escape))
                {
                    this.Exit();
                }
                if (mouseState.LeftButton == ButtonState.Pressed)
                {

                    snowflakes.Clear();
                    AddCreateSnow();
                }
            }
            base.Update(gameTime);
            
           

        }
        public static bool KeyPressed(Keys input)
        {
            if (keyboardState.IsKeyDown(input) && !lastKeyboardState.IsKeyDown(input))
            {
                return true;
            }
            return false;

        }

        public void AddCreateSnow()
        {
            var rnd = new Random();

            for (int i = 0; i < 5000; i++)
            {
                snowflakes.Add(new Snow
                {
                    X = rnd.Next(graf.PreferredBackBufferWidth),
                    Y = -rnd.Next(graf.PreferredBackBufferHeight),
                    Severity = rnd.Next(10, 25),

                });
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            sprite.Begin();
            sprite.Draw(imageTextureBack, new Rectangle(0, 0, graf.PreferredBackBufferWidth, graf.PreferredBackBufferHeight), Color.White);
            foreach (var c in snowflakes)
            {
                if (c.Y > -20)
                {
                    sprite.Draw(imageTextureSnow, new Rectangle(c.X, c.Y, c.Severity, c.Severity), Color.White);
                }
                

            }
            seconds += gameTime.ElapsedGameTime.TotalSeconds;
            sprite.End();
            base.Draw(gameTime);
        }
    }
}
