using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EscapeTheWindow
{
    public class Tile
    {
        private readonly Map _m;
        public Texture2D Texture { get; set; }
        public bool Solid { get; private set; }
        public Vector2 Position;
        public int Size;
        public int Height;
        public int Width;
        public Rectangle BoundingBox => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        public Tile(char id, Vector2 pos, Map m, int size)
        {
            _m = m;
            Size = size;
            Height = m.Game.ScreenHeight/ (9 * (int)Math.Pow(2, size));
            Width = m.Game.ScreenWidth/ (16 * (int)Math.Pow(2, size));
            if (id == '0')
            {
                Texture = m.Game.Bg;
                Solid = true;
            }
            else
            {
                Solid = false;
                Texture = m.Game.BlankTile;
            }
            Position = pos;
        }

        public void Draw(SpriteBatch sb)
        {
           sb.Draw(Texture, new Rectangle((int)Position.X - _m.Game.Window.Position.X, (int)Position.Y - _m.Game.Window.Position.Y, Width, Height), Color.White);
        }

        public void Update()
        {
            // TODO : Add Special Tiles Update logic
        }

    }
}
