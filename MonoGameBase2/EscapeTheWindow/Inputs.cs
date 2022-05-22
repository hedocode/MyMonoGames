using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EscapeTheWindow
{
    internal class Inputs
    {
        private readonly Game2 _game;
        private KeyboardState _stdin;
        private bool _upIsDown;
        private bool _f3IsDown;
        private bool _isGrab;

        public Inputs(Game2 game)
        {
            _game = game;
        }

        public void Update(GameTime gt)
        {
            _stdin = Keyboard.GetState();

            //Movement
            if (!_isGrab)
            {
                if (_stdin.IsKeyUp(Keys.Up))
                {
                    _upIsDown = false;
                }
                if (_stdin.IsKeyDown(Keys.Up))
                {
                    if (!_upIsDown)
                    {
                        _game.Sm.Velocity = _game.Sm.Velocity + new Vector2(0, -(float)_game.ScreenHeight/3);
                    }
                    _upIsDown = true;
                }


                if (_stdin.IsKeyDown(Keys.Down))
                {
                    _game.Sm.Velocity += new Vector2(0, 100) * (float)gt.ElapsedGameTime.TotalSeconds;
                }

                if (_stdin.IsKeyUp(Keys.Right) && _stdin.IsKeyUp(Keys.Left))
                {
                    _game.Sm.Velocity *= new Vector2((float)0.5, 1);
                }

                if (_stdin.IsKeyDown(Keys.Right))
                {
                    _game.Sm.Velocity += new Vector2((float)_game.ScreenHeight/3, 0) * (float)gt.ElapsedGameTime.TotalSeconds;
                    _game.Sm.Reverse = SpriteEffects.None;
                }
                if (_stdin.IsKeyDown(Keys.Left))
                {
                    _game.Sm.Velocity += new Vector2(-(float)_game.ScreenHeight/3, 0) * (float)gt.ElapsedGameTime.TotalSeconds;
                    _game.Sm.Reverse = SpriteEffects.FlipHorizontally;
                }
            }
            

            //Actions
            if (_stdin.IsKeyDown(Keys.Space))
            {
                _isGrab = _game.Sm.Grab();
                if (_isGrab)
                {
                    if (_stdin.IsKeyDown(Keys.Right) && _game.Window.Position.X < _game.Sm.Position.X + _game.Sm.Width / 5.0)
                    {
                        _game.Window.Position += new Point((int)(0.10 * gt.ElapsedGameTime.TotalMilliseconds), 0);
                    }
                    if (_stdin.IsKeyDown(Keys.Left) && (_game.Window.Position.X + _game.WindowWidth) >= (_game.Sm.Position.X + _game.Sm.Width) )
                    {
                        _game.Window.Position -= new Point((int)(0.10 * gt.ElapsedGameTime.TotalMilliseconds), 0);
                    }
                }
                
            }
            if (_stdin.IsKeyUp(Keys.Space))
            {
                _isGrab = false;
            }

            //Others
            if (_stdin.IsKeyDown(Keys.F3))
            {
                if (!_f3IsDown)
                {
                    _game.DisplayInfo = !_game.DisplayInfo;
                    _f3IsDown = true;
                }
            }
            if (_stdin.IsKeyUp(Keys.F3))
            {
                _f3IsDown = false;
            }
        }
    }
}
