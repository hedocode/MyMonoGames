using GameBaseArilox.API.Core;
using GameBaseArilox.Implementation.GUI;
using Microsoft.Xna.Framework;

namespace GameBaseArilox.Implementation.zUpdaters
{
    public class ButtonUpdater : IUpdater
    {
        private List<Button> buttons = new List<Button>();
        private GameModel _game;

        public ButtonUpdater(GameModel game)
        {
            game.AddToUpdaters(this);
            _game = game;
        }

        public void Update(GameTime gameTime)
        {
            Point MousePosition = _game.InputsManager.GetMousePosition();

            bool LeftClicked = _game.InputsManager.MouseInput.IsLeftButtonClick();
            bool LeftPressed = _game.InputsManager.MouseInput.IsLeftButtonPressed();

            foreach (Button button in buttons)
            {
                bool MouseHoverButton = (
                    MousePosition.X >= button.Left
                   && MousePosition.X <= button.Right
                   && MousePosition.Y >= button.Top
                   && MousePosition.Y <= button.Bot
                );
                if (
                   MouseHoverButton
                   && LeftPressed
                )
                {
                    button.OnClick();
                }
                /*else if (
                    MouseHoverButton
                    && !LeftClicked
                )
                {
                    button.OnHover();
                }
                else if (
                    MouseHoverButton
                    && LeftPressed
                ) {
                    button.OnPressed();
                } else {
                    button.OnRelease();
                }*/
            }
        }

        public void AddButton(Button b)
        {
            buttons.Add(b);
        } 
    }
}
