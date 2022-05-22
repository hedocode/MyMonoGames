using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameBase.Graphic;
using GameBaseArilox.UnitTest;
using WoodenPong;
using Loto;
using EscapeTheWindow;

namespace GameBaseArilox
{
    public class GameSelector : GameModel
    {

        private TextureColorGenerator _tcg = new TextureColorGenerator();
        private Texture2D? _blankTexture;

        public GameSelector()
        {
        }

        public void OpenGame(Type gameClassName) {
            this.Exit();
            Game game = (Game) Activator.CreateInstance(gameClassName);
            game.Run();
        }

        public void OpenParticles()
        {
            this.Exit();

            Game game = new TestParticleGenerator();
            game.Run();
        }


        public void OpenShapes()
        {
            this.Exit();

            Game game = new TestShapes();
            game.Run();
        }

        public void OpenTextSpritesAnimations()
        {
            this.Exit();

            Game game = new TestTextSpritesAnimation();
            game.Run();
        }

        public void OpenEscapeTheWindow()
        {
            this.Exit();

            Game game = new Game2();
            game.Run();
        }

        public void OpenTestAngles()
        {
            this.Exit();
            Game game = new TestAngle();
            game.Run();
        }

        public void OpenPong()
        {
            this.Exit();
            Game game = new Pong();
            game.Run();
        }

        public void OpenGameOfLife()
        {
            this.Exit();
            Game game = new GameOfLife();
            game.Run();
        }

        public void OpenLoto()
        {
            this.Exit();
            Game game = new Game1();
            game.Run();
        }

        protected override void LoadContent() {
            base.LoadContent();
            _blankTexture = _tcg.CreateTextureColor(SpriteBatch, 59, 75, 2);
            AddButton(10, 10, "Particles", OpenParticles);
            AddButton(210, 70, "Shapes", OpenShapes);
            AddButton(10, 70, "TextAnimation", OpenTextSpritesAnimations);
            AddButton(210, 10, "TestAngle", OpenTestAngles);
            AddButton(10, 150, "Pong", OpenPong);
            AddButton(210, 150, "Game Of Life", OpenGameOfLife);
            AddButton(10, 300, "Loto", OpenLoto);
            AddButton(210, 300, "EscapeTheWindow", OpenEscapeTheWindow);
            
        }

        private void AddButton(int x, int y, string name, Action action)
        {
            Implementation.GUI.Button b = new Implementation.GUI.Button(_blankTexture, new Vector2(x, y), 150, 50, name, action);
            ButtonUpdater.AddButton(b);
            ButtonDrawer.AddButton(b);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
