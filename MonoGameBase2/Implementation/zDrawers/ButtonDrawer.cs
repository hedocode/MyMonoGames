using GameBaseArilox.API.Graphic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameBaseArilox.Implementation.GUI;

namespace GameBaseArilox.Implementation.zDrawers
{
    public class ButtonDrawer : IDrawer
    {
        private GameModel _game;

        private List<Button> _toDraw;

        public ButtonDrawer(GameModel game)
        {
            _game = game;
            _toDraw = new List<Button>();
            game.AddToDrawers(this);
        }

        public void AddContent(string contentId, object content)
        {
            
        }

        public void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (Button button in _toDraw)
            {
                Draw(spriteBatch, button);
            }
        }

        public void Draw(SpriteBatch sb, Button b)
        {
            sb.Draw(b.Texture, new Rectangle((int)b.Left, (int)b.Top, (int)b.Width, (int)b.Height), Color.White);
            if (_game.SpriteFont != null && b.Text != null)
            {
                sb.DrawString(
                    _game.SpriteFont,
                    b.Text,
                    new Vector2(
                        b.Left + b.Width/ 2f - _game.SpriteFont.MeasureString(b.Text).X / 2,
                        b.Top + b.Height / 2f - _game.SpriteFont.MeasureString(b.Text).Y / 2
                    ),
                    Color.Black
                );
            }
        }

        public void AddButton(Button b)
        {
            _toDraw.Add(b);
        }
    }
}
