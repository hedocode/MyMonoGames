using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EscapeTheWindow
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game2 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Texture2D Bg;
        public Texture2D BlankTile;
        public Map Map { get; private set; }
        public Vector2 Gravity => new Vector2(0, (float)ScreenHeight / 2);
        public int ScreenHeight;
        public int ScreenWidth;
        public int WindowHeight => ScreenHeight/2;
        public int WindowWidth => ScreenWidth/2;
        public Stickman Sm;
        private readonly Inputs _inputs;
        public bool DisplayInfo;
        private SpriteFont _sf;


        public Game2()
        {
            _graphics = new GraphicsDeviceManager(this);
            ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Content.RootDirectory = "Content";
            _inputs = new Inputs(this);

            //Adjusting display to Screen
            if ((float) ScreenWidth/ScreenHeight > 16.0/9)
            {
                while ((float) ScreenWidth/ScreenHeight > 16.0/9)
                {
                    do
                    {
                        ScreenWidth--;
                    } while (ScreenWidth%16 != 0);
                }
                while ((float)ScreenWidth / ScreenHeight < 16.0 / 9)
                {
                    do
                    {
                        ScreenHeight--;
                    } while (ScreenHeight%9 != 0);
                }
            }
            else if ((float) ScreenWidth/ScreenHeight < 16.0/9)
            {
                while ((float)ScreenWidth / ScreenHeight < 16.0 / 9)
                {
                    do
                    {
                        ScreenHeight--;
                    } while (ScreenHeight%9 != 0);
                }
                while ((float)ScreenWidth / ScreenHeight > 16.0 / 9)
                {
                    do
                    {
                        ScreenWidth--;
                    } while (ScreenWidth%16 != 0);
                }
            }
            

            //Window Settings
            Window.IsBorderless = false;
            Window.AllowUserResizing = false;
            _graphics.IsFullScreen = false;
            IsMouseVisible = true;
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
            Sm = new Stickman(this, new Vector2(ScreenWidth / 2, ScreenHeight / 2));
            _graphics.PreferredBackBufferHeight = ScreenHeight / 2;
            _graphics.PreferredBackBufferWidth = ScreenWidth / 2;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Bg = Content.Load<Texture2D>("IMGS/Background");
            BlankTile = Content.Load<Texture2D>("IMGS/BlankTile");
            _sf = Content.Load<SpriteFont>("font1");
            Sm.LoadContent(Content);

            Map = new Map(this);
            Map.LoadMap("0ETWMap1");
            // TODO: use this.Content to load your game content here
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

            // TODO: Add your update logic here
            _inputs.Update(gameTime);
            Sm.Update(gameTime);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);

            Map.Draw(_spriteBatch);
            Sm.Draw(_spriteBatch);
            if (DisplayInfo)
            {
                _spriteBatch.DrawString(_sf, "{" + Window.Position.X + " " + Window.Position.Y + "}", new Vector2(10, 30), Color.Blue);
                _spriteBatch.DrawString(_sf, "{" + ScreenWidth + " " + ScreenHeight + "}", new Vector2(100, 30), Color.Blue);

            }

            _spriteBatch.End();
           

            base.Draw(gameTime);
        }
    }
}
