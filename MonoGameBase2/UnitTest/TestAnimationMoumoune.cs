using GameBaseArilox.API.Graphic;
using GameBaseArilox.Implementation.Entities;
using GameBaseArilox.Implementation.Core;

namespace GameBaseArilox.UnitTest
{
    public class TestAnimationMoumoune : GameModel
    {
        private ISprite _sprite;

        public TestAnimationMoumoune()
        {
            CameraUpdater.AddCamera(new Camera2D(Viewport, null, API.Enums.CameraType.Fixed));
            Moumoune m = new Moumoune(100,100);
            AddEntity(m);
        }
    }
}
