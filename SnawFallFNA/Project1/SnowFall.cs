using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Project1
{
    public class SnowFall : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graf;
        SpriteBatch sprite;
        private static KeyboardState keyboardState = Keyboard.GetState();
        private static KeyboardState lastKeyboardState;

        private static MouseState mouseState;
        private int lastGameTime = 0, gametime = 0;
        Random rnd;
        Texture2D imageTextureBack, imageTextureSnow;
        Vector2 posBack;
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
            posBack = new Vector2(0, 0);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            sprite = new SpriteBatch(GraphicsDevice);
            imageTextureBack = Texture.Load("..\\Background1.png", Content);
            imageTextureSnow = Texture.Load("..\\Snow1.png", Content);
        }

        protected override void Update(GameTime gameTime)
        {
            
            lastGameTime += DateTime.Now.Millisecond;
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            mouseState = Mouse.GetState();
            
            foreach (var snowflake in snowflakes)
            {
                snowflake.Y += snowflake.Severity;
                if (snowflake.Y > graf.PreferredBackBufferHeight)
                {
                    snowflake.Y = -snowflake.Y;
                    snowflake.X = rnd.Next(0, graf.PreferredBackBufferWidth);
                }
            }
            if (KeyPressed(Keys.Escape))
            {
               this.Exit();
            }
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                snowflakes.Clear();
                AddCreateSnow();
            }

            gametime += DateTime.Now.Millisecond;
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
                    Severity = rnd.Next(5, 30),
                    
                }) ;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            sprite.Begin();
            sprite.Draw(imageTextureBack, new Rectangle(0, 0, graf.PreferredBackBufferWidth, graf.PreferredBackBufferHeight), Color.White);
            foreach (var c in snowflakes)
            {
                if (c.Y > 0)
                {
                    sprite.Draw(imageTextureSnow, new Rectangle(c.X, c.Y, c.Severity, c.Severity), Color.White);
                }
            }
            sprite.End();
            base.Draw(gameTime);
        }
    }
}
