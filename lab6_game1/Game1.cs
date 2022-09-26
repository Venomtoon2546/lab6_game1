using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace lab6_game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private KeyboardState _keyboardState;

        Random r = new Random();
        Texture2D ballTexture;
        Texture2D charTexture;
        Vector2 charPosition = new Vector2(0,250); 
        Vector2[] ballPosition = new Vector2[4];
        int[] ballColor = new int[4];
        bool personHit;

        int direction = 0;
        int speed = 2;

        int frame;
        int totalframe;
        int framepersec;
        float timeperframe;
        float totalelapsed;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice); 
            charTexture = Content.Load<Texture2D>("Char01 (1)"); 
            ballTexture = Content.Load<Texture2D>("ball");
            frame = 0;
            totalframe = 4;
            framepersec = 8;
            timeperframe = (float)1 / framepersec;
            totalelapsed = 0;

            for (int i = 0; i < 4; i++)
            {
                ballPosition[i].X = r.Next(_graphics.GraphicsDevice.Viewport.Width - ballTexture.Width / 6);
                ballPosition[i].Y = r.Next(_graphics.GraphicsDevice.Viewport.Height - ballTexture.Height);
                ballColor[i] = r.Next(6);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            GraphicsDevice device = _graphics.GraphicsDevice;
            _keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();

            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                direction = 1;
                charPosition.X = charPosition.X - speed;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                direction = 2;
                charPosition.X = charPosition.X + speed;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                direction = 3;
                charPosition.Y = charPosition.Y - speed;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (_keyboardState.IsKeyDown(Keys.Down))
            {
                direction = 0;
                charPosition.Y = charPosition.Y + speed;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            Rectangle charRectangle = new Rectangle((int)charPosition.X, (int)charPosition.Y, 32, 48);

            for(int i = 0;i<4;i++)
            {
                Rectangle blockRectangle = new Rectangle((int)ballPosition[i].X, (int)ballPosition[i].Y, 24, 24);

                if (charRectangle.Intersects(blockRectangle) == true)
                {
                    personHit = true;
                    ballPosition[i].X = r.Next(_graphics.GraphicsDevice.Viewport.Width - ballTexture.Width / 6);
                    ballPosition[i].Y = r.Next(_graphics.GraphicsDevice.Viewport.Height - ballTexture.Height);
                    ballColor[i] = r.Next(6);
                }
                else if (charRectangle.Intersects(blockRectangle) == false)
                {
                    personHit = false;
                }
            }
            


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice device = _graphics.GraphicsDevice;
            if (personHit == true) 
            {
                device.Clear(Color.CornflowerBlue);
            } 
            else 
            { 
                device.Clear(Color.CornflowerBlue); 
            }

            _spriteBatch.Begin(); 
            for(int i=0;i<4;i++)
            {
                _spriteBatch.Draw(ballTexture, ballPosition[i], new Rectangle(24*ballColor[i], 0, 24, 24), Color.White);
            }
            
            _spriteBatch.Draw(charTexture, charPosition, new Rectangle(32 * frame, 48 * direction, 32, 48), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        void UpdateFrame(float elapsed)
        {
            totalelapsed += elapsed;
            if (totalelapsed > timeperframe)
            {
                frame = (frame + 1) % totalframe;
                totalelapsed -= timeperframe;
            }
        }
    }
}
