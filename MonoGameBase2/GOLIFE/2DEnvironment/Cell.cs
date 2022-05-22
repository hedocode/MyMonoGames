using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace GameBaseArilox._2DEnvironment
{
    public class Cell
    {
        //private float _resistance;
        private int _red;
        private int _green;
        private int _blue;

        public enum RGB
        {
            Red,
            Green,
            Blue,
            Black,
            White,
            None
        }

        public Color Color
        {
            get
            {
                return new Color(Red, Green, Blue);
            }
            set
            {
                Red = value.R;
                Green = value.G;
                Blue = value.B;
            }
        }

        public RGB MainColor
        {
            get
            {
                if(Red > Blue && Red > Green)
                    return RGB.Red;
                if (Blue > Red && Blue > Green)
                    return RGB.Blue;
                if (Green > Red && Green > Blue)
                    return RGB.Green;
                if(Red == 255 && Green == 255 && Blue == 255)
                    return RGB.White;
                if(Red == 0 && Green == 0 && Blue == 0)
                    return RGB.Black;
                return RGB.None;
            }
        }

        /*
        public float Resistance
        {
            get { return _resistance; }
            set { _resistance = SetInBounds(value, 1, null); }
        }*/

        public int Red
        {
            get { return _red; }
            set { _red = (int)SetInBounds(value, 0, 255); }
        }

        public int Green
        {
            get { return _green; }
            set { _green = (int)SetInBounds(value, 0, 255); }
        }

        public int Blue
        {
            get { return _blue; }
            set { _blue = (int)SetInBounds(value, 0, 255); }
        }

        public Cell()
        {
            Red = 0;
            Green = 0;
            Blue = 0;
        }

        public Cell(bool empty) : this()
        {
            if (!empty) return;
            Red = 255;
            Green = 255;
            Blue = 255;
        }

        public Cell(int red, int green, int blue) : this()
        {
            _red = red;
            _green = green;
            _blue = blue;
        }

        public int SetInBounds(int value, int mini, int maxi)
        {
            if (value >= mini && value <= maxi)
                return value;
            return value < mini ? 0 : maxi;
        }

        public static Cell operator +(Cell cell, Color color)
        {
            cell.Red += color.R;
            cell.Green += color.G;
            cell.Blue += color.B;
            return cell;
        }

        public static Cell operator +(Cell cell, Cell cell2)
        {
            cell.Red += cell2.Red;
            cell.Green += cell2.Green;
            cell.Blue += cell2.Blue;
            return cell;
        }

        public static Cell operator -(Cell cell, Color color)
        {
            cell.Red -= color.R;
            cell.Green -= color.G;
            cell.Blue -= color.B;
            return cell;
        }

        public static Cell operator -(Cell cell, Cell cell2)
        {
            cell.Red -= cell2.Red;
            cell.Green -= cell2.Green;
            cell.Blue -= cell2.Blue;
            return cell;
        }

        public static Cell operator *(Cell cell, int multiplier)
        {
            cell.Red *= multiplier;
            cell.Green *= multiplier;
            cell.Blue *= multiplier;
            return cell;
        }

        public static explicit operator Cell(Color color)
        {
            return new Cell(color.R, color.G, color.B);
        }

        public static Cell operator /(Cell cell, int multiplier)
        {
            cell.Red /= multiplier;
            cell.Green /= multiplier;
            cell.Blue /= multiplier;
            return cell;
        }

        public void Trade(Cell cell)
        {
            switch (MainColor)
            {
                case RGB.Red:
                    switch (cell.MainColor)
                    {
                        case RGB.Red:
                            Blue += 16;
                            Red -= 16;
                            break;
                        case RGB.Green:
                            cell.Red += 16;
                            cell.Green -= 16;
                            break;
                        case RGB.Blue:
                            cell.Green -= 16;
                            cell.Blue -= 16;
                            break;
                        case RGB.Black:
                            break;
                        case RGB.White:
                            break;
                        default:
                            break;
                    }
                    break;
                case RGB.Green:
                    break;
                case RGB.Blue:
                    break;
                case RGB.Black:
                    break;
                case RGB.White:
                    break;
                default:
                    break;
            }
        }
    }
}
