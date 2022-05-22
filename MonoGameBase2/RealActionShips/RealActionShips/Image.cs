using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace RealActionShips
{
    class Image
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Texture2D Texture { get; set; }
        public Dictionary<string, ImageEffect> effects;
    }
}
