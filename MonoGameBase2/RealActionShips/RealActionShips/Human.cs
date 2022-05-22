using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RealActionShips
{
    class Human
    {
        private Image bodyDown;
        private Image bodyUp;
        private Image chest;
        private Image hair;
        private Image eyes;
        private Image mouth;
        private Image foot;

        private Vector2 position;
        private Vector2 velocity;

        private int Strengh;
        private int Intelligence;
        private int Reputation;
        private int Culture;
        private int Humour;
        private int Curiosity;
        private int Income;
        private int AnotherOne;

        public Human(Image back, Image bodyDown, Image bodyUp, Image bottom, Image chest, Image hair, Image eyes, Image mouth, Image foot, Vector2 pos, Vector2 vel)
        {
            this.bodyDown = bodyDown;
            this.bodyUp = bodyUp;
            this.chest = chest;
            this.hair = hair;
            this.eyes = eyes;
            this.mouth = mouth;
            this.foot = foot;

            back.Position = new Vector2(position.X + 50, position.Y + 50);
            bodyDown.Position = new Vector2(position.X + 100, position.X + 200);


            position = pos;
            velocity = vel;
        }

        public void Update(double elapsedTime)
        {
            //TODO : Write Human collision and action logic.

        }

        public void Draw(SpriteBatch sb)
        {
            //TODO : Draw all the body parts
            sb.Draw(bodyDown.Texture, new Rectangle(0,0,0,0), Color.White);
        }
    }
}
