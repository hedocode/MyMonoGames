using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EscapeTheWindow
{
    public class Stickman
    {
        private readonly Game2 _game;
        private Vector2 _position;
        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public int MaxVelocity => _game.ScreenHeight/2;
        private Vector2 _velocity;
        public Vector2 Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                if (value.X <= MaxVelocity && value.X >= -MaxVelocity)
                {
                    _velocity = value;
                }
            }
        }

        public int Width => _game.ScreenWidth / (int)(16 * (Math.Pow(2, _game.Map.Taille))) -1;
        public int Height => _game.ScreenWidth / (int)(16 * (Math.Pow(2, _game.Map.Taille))) -1;

        public Vector2 MapPosition => new Vector2( (int) ( (_position.X+Width/2.0) / _game.ScreenWidth *16 * (Math.Pow(2, _game.Map.Taille))) , (int) ( (_position.Y+Height/2.0) / _game.ScreenHeight * 9 * (Math.Pow(2, _game.Map.Taille))) );

        private Texture2D _currentTexture;

        private int Ratio => Width/3;
        private Rectangle BoundingBox
            => new Rectangle((int) Position.X + Ratio, (int) Position.Y, Ratio, Height);

        private bool _inc = true;
        public SpriteEffects Reverse = SpriteEffects.None;
        private double _stickmanState;
        private Texture2D _static;
        private Texture2D _pull;
        private Texture2D _push;
        private Texture2D _traction;
        private Texture2D _a1;
        private Texture2D _a2;
        private Texture2D _a3;
        private Texture2D _a4;
        private Texture2D _a5;
        private Texture2D _a6;

        //private Texture2D _box;

        private SpriteFont _sf;


        public Stickman(Game2 game, Vector2 pos)
        {
            _game = game;
            Position = pos;
            Velocity = new Vector2(0, 0);
        }

        public void Update(GameTime gt)
        {
            //Registering Near Tiles
            Tile topTile = _game.Map.GetTile((int)MapPosition.X, (int)MapPosition.Y-1);
            Tile botTile = _game.Map.GetTile((int)MapPosition.X, (int)MapPosition.Y + 1);
            Tile rightTile = _game.Map.GetTile((int) MapPosition.X + 1, (int) MapPosition.Y);
            Tile leftTile = _game.Map.GetTile((int)MapPosition.X-1, (int)MapPosition.Y);
            Tile currentTile = _game.Map.GetTile((int)MapPosition.X, (int)MapPosition.Y);
            Tile topleftTile = _game.Map.GetTile((int)MapPosition.X-1, (int)MapPosition.Y-1);
            Tile toprightTile = _game.Map.GetTile((int)MapPosition.X+1, (int)MapPosition.Y-1);
            Tile botleftTile = _game.Map.GetTile((int)MapPosition.X-1, (int)MapPosition.Y+1);
            Tile botrightTile = _game.Map.GetTile((int)MapPosition.X+1, (int)MapPosition.Y+1);

            //Stickman Update
            if (_game.Map.GetTile((int)(MapPosition.X), (int)((_position.Y + Height / 2.0) / _game.ScreenHeight * 9 * (Math.Pow(2, _game.Map.Taille)) + 0.6))?.Solid == false)
            {
                Velocity += _game.Gravity * (float)gt.ElapsedGameTime.TotalSeconds;
            }
            _position.X += Velocity.X * (float)gt.ElapsedGameTime.TotalSeconds;
            _position.Y += Velocity.Y * (float)gt.ElapsedGameTime.TotalSeconds;


            // Window Collisions
            if (_position.X + Ratio  < _game.Window.Position.X && _velocity.X < 0)
            {
                _velocity.X = 0;
                _position.X = _game.Window.Position.X - Ratio - 3;
            }
            else if (_position.X + Width - Ratio > _game.Window.Position.X + _game.WindowWidth && _velocity.X > 0)
            {
                _velocity.X = 0;
                _position.X = _game.Window.Position.X + _game.WindowWidth - Width + Ratio;
            }

            if (_position.Y < _game.Window.Position.Y)
            {
                Vector2 vel = _velocity;
                _velocity.Y = 0;
                _position.Y = _game.Window.Position.Y;
                _game.Window.Position += new Point(0, (int)(0.05 * vel.Y));
            }
            if (_position.Y + Height > _game.Window.Position.Y + _game.WindowHeight)
            {
                Vector2 vel = _velocity;
                _velocity.Y = 0;
                if (vel.Y > (float)_game.ScreenHeight/6)
                {
                    _game.Window.Position += new Point(0, (int)(0.05 * vel.Y));
                }
                _position.Y = _game.Window.Position.Y + _game.WindowHeight - Height;
            }


            // Map Collisions
            if (leftTile != null && BoundingBox.Intersects(leftTile.BoundingBox) && leftTile.Solid)
            {
                _position.X = currentTile.Position.X - Ratio;
                _velocity.X = 0;
            }
            if (rightTile != null && BoundingBox.Intersects(rightTile.BoundingBox) && rightTile.Solid)
            {
                _position.X = currentTile.Position.X + Ratio + 4;
                _velocity.X = 0;
            }
            if (botTile != null && BoundingBox.Intersects(botTile.BoundingBox) && botTile.Solid)
            {
                _position.Y = currentTile.Position.Y;
                _velocity.Y = 0;
            }
            if (topTile != null && BoundingBox.Intersects(topTile.BoundingBox) && topTile.Solid)
            {
                _position.Y = currentTile.Position.Y;
                _velocity.Y = 0;
            }

            if ( topleftTile != null && (BoundingBox.Intersects(topleftTile.BoundingBox) && topleftTile.Solid))
            {
                if ((float)(_position.X / _game.ScreenWidth * 16 * (Math.Pow(2, _game.Map.Taille))) - (int)(_position.X / _game.ScreenWidth * 16 * (Math.Pow(2, _game.Map.Taille))) < (float)(_position.Y / _game.ScreenHeight * 9 * (Math.Pow(2, _game.Map.Taille))) - (int)(_position.Y / _game.ScreenHeight * 9 * (Math.Pow(2, _game.Map.Taille))))
                {
                    _position.Y = currentTile.Position.Y;
                    _velocity.Y = 0;
                }
                else
                {
                    _position.X = currentTile.Position.X - Ratio;
                    _velocity.X = 0;
                }
            }

            // TODO: Fix Collisions
            if (botleftTile != null && (BoundingBox.Intersects(botleftTile.BoundingBox) && botleftTile.Solid))
            {
                if ( ( botleftTile.Position.X + (_game.ScreenWidth / 16.0 * (Math.Pow(2, _game.Map.Taille)))) - (float)(_position.X / _game.ScreenWidth * 16 * (Math.Pow(2, _game.Map.Taille))) > (float)((_position.Y + Height) / _game.ScreenHeight * 9 * (Math.Pow(2, _game.Map.Taille))) - botleftTile.Position.Y )
                {
                    _position.Y = currentTile.Position.Y;
                    _velocity.Y = 0;
                }
                else
                {
                    _position.X = currentTile.Position.X - Ratio;
                    _velocity.X = 0;
                }
            }
            if (toprightTile != null && (BoundingBox.Intersects(toprightTile.BoundingBox) && toprightTile.Solid))
            {
                if ((float)((int)((_position.X + Width) / _game.ScreenWidth) * 16 * (Math.Pow(2, _game.Map.Taille))) - (int)((int)((_position.X + Width) / _game.ScreenWidth) * 16 * (Math.Pow(2, _game.Map.Taille))) > (float)((_position.Y-Height) / _game.ScreenHeight * 9 * (Math.Pow(2, _game.Map.Taille))) - (int)((_position.Y -Height) / _game.ScreenHeight * 9 * (Math.Pow(2, _game.Map.Taille))))
                {
                    _position.Y = currentTile.Position.Y;
                    _velocity.Y = 0;
                }
                else
                {
                    _position.X = currentTile.Position.X + Ratio;
                    _velocity.X = 0;
                }
            }
            // TODO: FIX Collisions
            if (botrightTile != null && (BoundingBox.Intersects(botrightTile.BoundingBox) && botrightTile.Solid))
            {
                if ((float)((_position.X + Width) / _game.ScreenWidth * 16 * (Math.Pow(2, _game.Map.Taille))) - (int)((_position.X + Width) / _game.ScreenWidth * 16 * (Math.Pow(2, _game.Map.Taille))) > (float)((_position.Y + Height) / _game.ScreenHeight * 9 * (Math.Pow(2, _game.Map.Taille))) - (int)((_position.Y + Height) / _game.ScreenHeight * 9 * (Math.Pow(2, _game.Map.Taille))))
                {
                    _position.Y = currentTile.Position.Y;
                    _velocity.Y = 0;
                }
                else
                {
                    _position.X = topTile.Position.X + (float) Width/3;
                    _velocity.X = 0;
                }
            }



            //Animations
            switch ((int)_stickmanState)
            {
                case -1:
                    _currentTexture = _static;
                    break;

                case 0:
                    _currentTexture = _a1;
                    break;

                case 1:
                    _currentTexture = _a2;
                    break;

                case 2:
                    _currentTexture = _a3;
                    break;

                case 3:
                    _currentTexture = _a4;
                    break;

                case 4:
                    _currentTexture = _a5;
                    break;

                case 5:
                    _currentTexture = _a6;
                    break;

                case 6:
                    _currentTexture = _push;
                    break;

                case 7:
                    _currentTexture = _pull;
                    break;

                case 8:
                    _currentTexture = _traction;
                    break;
            }
            if ((int)Velocity.X == 0)
            {
                _stickmanState = -1;
            }
            else
            {
                if (_inc)
                {
                    _stickmanState += 10 * gt.ElapsedGameTime.TotalSeconds;
                    if (_stickmanState >= 5)
                    {
                        _inc = false;
                    }
                }
                else
                {
                    _stickmanState -= 10 * gt.ElapsedGameTime.TotalSeconds;
                    if (_stickmanState <= 0)
                    {
                        _inc = true;
                    }
                }
            }

        }

        public void Draw(SpriteBatch sb)
        {
            //sb.Draw(_box, new Rectangle(BoundingBox.X - _game.Window.Position.X, BoundingBox.Y - _game.Window.Position.Y,BoundingBox.Width,BoundingBox.Height), Color.White);


            sb.Draw(_currentTexture,
                new Rectangle((int)Position.X - _game.Window.Position.X, (int)Position.Y - _game.Window.Position.Y, Width, Height),
                null,
                Color.White,
                0f,
                new Vector2(0,0),
                Reverse, 
                0f
                );

            //Display Stickman Infos
            if (_game.DisplayInfo)
            {
                sb.DrawString(_sf, "StickmanInfo :", new Vector2(10, 10), Color.Blue);
                sb.DrawString(_sf, MapPosition + " ", new Vector2(110, 10), Color.Blue);
                sb.DrawString(_sf, _position + " ", new Vector2(180, 10), Color.Blue);
                sb.DrawString(_sf, (int)_stickmanState + " ", new Vector2(410, 10), Color.Blue);
            }
        }

        public void LoadContent(ContentManager content)
        {
            _sf = content.Load<SpriteFont>("font1");
            _static = content.Load<Texture2D>("IMGS/Stickman_Static");
            _currentTexture = _static;
            _stickmanState = -1;
            _pull = content.Load<Texture2D>("IMGS/Stickman_Pull");
            _push = content.Load<Texture2D>("IMGS/Stickman_Push");
            _traction = content.Load<Texture2D>("IMGS/Stickman_Traction0");
            _a1 = content.Load<Texture2D>("IMGS/Stickman_Running1");
            _a2 = content.Load<Texture2D>("IMGS/Stickman_Running2");
            _a3 = content.Load<Texture2D>("IMGS/Stickman_Running3");
            _a4 = content.Load<Texture2D>("IMGS/Stickman_Running4");
            _a5 = content.Load<Texture2D>("IMGS/Stickman_Running5");
            _a6 = content.Load<Texture2D>("IMGS/Stickman_Running6");
            //_box = content.Load<Texture2D>("IMGS/box");
        }

        public bool Grab()
        {
            if ( (_position.X - 5 < _game.Window.Position.X || _position.X + Width + 5 > _game.Window.Position.X + _game.WindowWidth) /*&& _velocity.Y == 0*/)
            {
                if (_position.X - 5 < _game.Window.Position.X)
                {
                    float oldpos = _position.X;
                    _position.X = _game.Window.Position.X;
                    if (_game.Map.TileMap[(int)MapPosition.X, (int)MapPosition.Y].Solid)
                    {
                        _position.X = oldpos;
                    }
                    _stickmanState = oldpos > _position.X ? 6 : 7;
                    Reverse = SpriteEffects.FlipHorizontally;
                    
                }
                else
                {
                    float oldpos = _position.X;
                    _position.X = _game.Window.Position.X + _game.WindowWidth - Width;
                    _stickmanState = oldpos < _position.X ? 6 : 7;
                    Reverse = SpriteEffects.None;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
