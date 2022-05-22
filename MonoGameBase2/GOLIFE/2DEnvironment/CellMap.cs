namespace GameBaseArilox._2DEnvironment
{
    public class CellMap
    {
        private static Cell[,] _map;
        private int _width;
        private int _height;
        private CellMapManager _cmm = new CellMapManager();

        public CellMap(int width, int height, CellMapType type)
        {
            _map = new Cell[width, height];
            switch (type)
            {
                case CellMapType.BlackAndWhite:
                    _cmm.BwMap(_map, width, height);
                    break;
                case CellMapType.Multicolor:
                    _cmm.ColorMap(_map, width, height);
                    break;
            }
            _width = width;
            _height = height;
        }

        public static explicit operator Cell[,] (CellMap cell)
        {
            return _map;
        }


        public Cell this[int x, int y]
        {
            get { return _map[x, y]; }
            set { _map[x, y] = value; }
        } 

        public int Width
        {
            get { return _width; }
            set
            {
                if(value > 0)
                    _width = value;
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                if (value > 0)
                    _height = value;
            }
        }

        public void RedefineSize(int width, int heigh)
        {
            
        }
    }
}
