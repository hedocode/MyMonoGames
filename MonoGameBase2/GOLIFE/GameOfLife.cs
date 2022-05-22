using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameBase.Graphic;
using GameBaseArilox._2DEnvironment;

namespace GameBaseArilox
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameOfLife : Game
    {
        private GraphicsDeviceManager? _graphics;
        private SpriteBatch? _spriteBatch;
        private TextureColorGenerator? _tcg = new TextureColorGenerator();
        private Texture2D? _blankTexture;

        private SpriteFont? _spriteFont;

        private bool _pause;
        private bool _displayDebug = true;

        private KeyboardState _oldKeyboardState;
        private KeyboardState _keyboardState;

        private MouseState _mouseState;
        private MouseState _oldMouseState;

        private CellMapManager? _cmm = new CellMapManager();

        private Cell? _selectedCell;

        private int _mapWidth = 60;
        private int _mapHeight = 30;
        private int _cellWidth = 16;
        private int _cellHeight = 16;

        private int _frequency = 50;

        private double _timer;

        private CellMap _cellMap;


        //Screen dimensions properties
        public int ScreenWidth => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public int ScreenHeight => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public int WindowWidth
        {
            get { return _graphics.GraphicsDevice.Viewport.Width; }
            set { _graphics.PreferredBackBufferWidth = value; _graphics.ApplyChanges(); }
        }

        public int WindowHeight
        {
            get { return _graphics.GraphicsDevice.Viewport.Height; }
            set { _graphics.PreferredBackBufferHeight = value; _graphics.ApplyChanges(); }
        }

        public GameOfLife()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = false;
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
            WindowHeight = ScreenHeight;
            WindowWidth = ScreenWidth;

            _mapWidth = ScreenWidth / _cellHeight;
            _mapHeight = ScreenHeight / _cellWidth;

            _cellMap = new CellMap(_mapWidth, _mapHeight, CellMapType.BlackAndWhite);

            _oldKeyboardState = Keyboard.GetState();
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
            _spriteFont = Content.Load<SpriteFont>("FONTS/arial");
            _blankTexture = _tcg.CreateTextureColor(_spriteBatch, 59, 75, 2);
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
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (_keyboardState.IsKeyDown(Keys.Space) && _oldKeyboardState.IsKeyUp(Keys.Space))
            {
                _cmm.ColorMap(_cellMap);
            }
            if (_keyboardState.IsKeyDown(Keys.Back) && _oldKeyboardState.IsKeyUp(Keys.Back))
            {
                _cmm.EmptyMap(_cellMap);
            }
            if (_keyboardState.IsKeyDown(Keys.Enter) && _oldKeyboardState.IsKeyUp(Keys.Enter))
            {
                _cmm.BwMap(_cellMap);
            }
            if (_keyboardState.IsKeyDown(Keys.A) && _oldKeyboardState.IsKeyUp(Keys.A))
            {
                _cmm.Step(_cellMap);
            }
            if (_keyboardState.IsKeyDown(Keys.P) && _oldKeyboardState.IsKeyUp(Keys.P) || _keyboardState.IsKeyDown(Keys.L))
            {
                _frequency--;
            }
            if (_keyboardState.IsKeyDown(Keys.M) && _oldKeyboardState.IsKeyUp(Keys.M) || _keyboardState.IsKeyDown(Keys.K))
            {
                _frequency++;
            }
            if (_keyboardState.IsKeyDown(Keys.F1) && _oldKeyboardState.IsKeyUp(Keys.F1))
            {
                _displayDebug = !_displayDebug;
            }
            if (_keyboardState.IsKeyDown(Keys.CapsLock) && _oldKeyboardState.IsKeyUp(Keys.CapsLock))
            {
                _pause = !_pause;
            }
            if (_keyboardState.IsKeyDown(Keys.Y) && _oldKeyboardState.IsKeyUp(Keys.Y))
            {
                _frequency = 500;
            }
            if (_keyboardState.IsKeyDown(Keys.U) && _oldKeyboardState.IsKeyUp(Keys.U))
            {
                _frequency = 100;
            }
            if (_keyboardState.IsKeyDown(Keys.I) && _oldKeyboardState.IsKeyUp(Keys.I))
            {
                _frequency = 20;
            }
            if (_keyboardState.IsKeyDown(Keys.O) && _oldKeyboardState.IsKeyUp(Keys.O))
            {
                _frequency = 5;
            }
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                _selectedCell = _cellMap[_cmm.SetInBounds(_mouseState.Position.X / _cellWidth, 0, _mapWidth - 1),
                                    _cmm.SetInBounds(_mouseState.Position.Y / _cellHeight, 0, _mapHeight - 1)];
                _selectedCell.Color = Color.White;
            }
            if (_mouseState.RightButton == ButtonState.Pressed)
            {
                _selectedCell = _cellMap[_cmm.SetInBounds(_mouseState.Position.X / _cellWidth, 0, _mapWidth - 1),
                                    _cmm.SetInBounds(_mouseState.Position.Y / _cellHeight, 0, _mapHeight - 1)];
                _selectedCell.Color = Color.Black;
            }


            if (!_pause)
            {
                _timer += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (_timer >= _frequency)
                {
                    _cmm.Step(_cellMap);
                    _timer = 0;
                }
            }


            // TODO: Add your update logic here

            _oldKeyboardState = _keyboardState;
            _oldMouseState = _mouseState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    DrawCell(_spriteBatch, _cellMap[x, y], x, y);
                }
            }

            int x1 = _cmm.SetInBounds(_mouseState.Position.X / _cellWidth, 0, _mapWidth - 1);
            int y1 = _cmm.SetInBounds(_mouseState.Position.Y / _cellHeight, 0, _mapHeight - 1);

            _spriteBatch.Draw(_blankTexture, new Rectangle((_mouseState.X / _cellWidth) * _cellWidth, (_mouseState.Y / _cellHeight) * _cellHeight, _cellWidth, _cellHeight), Color.Red);
            if (_displayDebug)
            {
                _spriteBatch.DrawString(_spriteFont, "[X:" + x1 + ",Y:" + y1 + "]", Vector2.One, Color.Red);
                _spriteBatch.DrawString(_spriteFont, "Neighboor count :" + _cmm.CountNeighboors(_cellMap, x1, y1, Cell.RGB.White), new Vector2(1, 25), Color.Red);
                _spriteBatch.DrawString(_spriteFont, "MainColor : " + _cellMap[x1, y1].MainColor, new Vector2(1, 45), Color.Red);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void DrawCell(SpriteBatch spriteBatch, Cell cell, int x, int y)
        {
            spriteBatch.Draw(_blankTexture, new Rectangle(x * _cellWidth, y * _cellHeight, _cellWidth, _cellHeight), cell.Color);
        }
    }
}
