using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameBase.Graphic
{
    public class TextureColorGenerator
    {
        public Texture2D CreateTextureColor(SpriteBatch sb, int r, int g, int b)
        {
            Texture2D t = new Texture2D(sb.GraphicsDevice, 1, 1);
            Color color = new Color(new Vector3(r, g, b));
            t.SetData(new[] { color });
            return t;
        }
    }
}
