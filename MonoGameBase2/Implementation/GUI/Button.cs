using GameBaseArilox.API.Core;
using GameBaseArilox.API.Detection;
using GameBaseArilox.API.Graphic;
using GameBaseArilox.Implementation.Core;
using GameBaseArilox.API.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameBaseArilox.Implementation.GUI
{
    public class Button : IRectangle, IClickable
    {
        private float _x;
        private float _y;

        public float Height { get; set; }
        public float Width { get; set; }
        public Vector2 Position
        {
            get
            {
                return new Vector2(_x, _y);
            }
            set
            {
                _x = value.X;
                _y = value.Y;
            }
        }

        public float Top => Position.Y;
        public float Bot => Position.Y + Height;
        public float Right => Position.X + Width;
        public float Left => Position.X;

        public bool Verif;
        public string Text;


        public List<ICoordinates> Points { get; set; }

        public ICoordinates Center => new Vector2D(Left + Width / 2, Top + Height / 2);


        public float Rotation { get; set; }
        public ISprite Sprite { get; set; }
        public IDetectionArea Hitbox { get; set; }

        public Action A;
        public Texture2D Texture;

        public Button (Texture2D texture2D, Vector2 position, float width, float height, string text, Action a)
        {
            Points = new List<ICoordinates>
            {
                new Vector2D(position),
                new Vector2D(position.X+width,position.Y),
                new Vector2D(position.X,position.Y+height),
                new Vector2D(position.X+width,position.Y+height)
            };
            _x = position.X;
            _y = position.Y;
            Width = width;
            Height = height;
            A = a;
            Text = text;
            Texture = texture2D;
        }

        public Button(Texture2D texture2D, float x, float y, float width, float height, string text, Action a)
        {
            _x = x;
            _y = y;
            Width = width;
            Height = height;
            A = a;
            Text = text;
            Texture = texture2D;
        }

        public void OnClick()
        {
            A();
        }

        public void OnHover()
        {

        }

        public void OnPressed()
        {
            
        }

        public void OnRelease()
        {
            
        }
    }
}
