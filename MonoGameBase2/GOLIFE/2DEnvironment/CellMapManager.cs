using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameBaseArilox._2DEnvironment
{
    public class CellMapManager
    {
        public void ColorMap(CellMap map)
        {
            ColorMap((Cell[,])map, map.Width, map.Height);
        }

        public void ColorMap(Cell[,] map, int width, int height)
        {
            Random r = new Random();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = new Cell(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                }
            }
        }

        public void EmptyMap(CellMap map)
        {
            EmptyMap((Cell[,])map, map.Width, map.Height);
        }

        public void EmptyMap(Cell[,] map, int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    map[x, y] = new Cell();
                }
            }
        }

        public void BwMap(CellMap map)
        {
            BwMap((Cell[,])map, map.Width, map.Height);
        }

        public void BwMap(Cell[,] map, int width, int height)
        {
            Random r = new Random();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (r.Next(0, 2) == 0)
                        map[x, y] = new Cell(false);
                    else
                        map[x, y] = new Cell(true);
                }
            }
        }


        public int CountNeighboors(CellMap map, int x, int y, Cell.RGB rgb)
        {
            return CountNeighboors((Cell[,])map, map.Width, map.Height, x, y, rgb);
        }

        public int CountNeighboors(Cell[,] map, int mapWidth, int mapHeight, int x, int y, Cell.RGB rgb)
        {
            int count = 0;
            for (int a = -1; a <= 1; a++)
            {
                for (int b = -1; b <= 1; b++)
                {
                    Point coord = ToreValue(x + a, y + b, mapWidth, mapHeight);
                    if (map[coord.X, coord.Y].MainColor == rgb)
                        count++;
                }
            }
            if (map[x, y].MainColor == rgb)
                count--;
            return count;
        }

        public void Step(CellMap map)
        {
            Step((Cell[,]) map, map.Width, map.Height);
        }

        public void Step(Cell[,] map, int mapWidth, int mapHeight)
        {
            Cell[,] newMap = CpyMap(map, mapWidth, mapHeight);
            List<Cell> done = new List<Cell>();
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    int nb = CountNeighboors(map, mapWidth, mapHeight, x, y, Cell.RGB.White);

                    if (map[x, y].MainColor == Cell.RGB.Black)
                    {
                        if (nb == 3)
                            newMap[x, y].Color = Color.White;
                    }
                    if (map[x, y].MainColor == Cell.RGB.White && !(nb == 2 || nb == 3))
                    {
                        newMap[x, y].Color = Color.Black;
                    }
                }
            }
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    map[x, y].Color = newMap[x, y].Color;
                }
            }
        }

        public Point ToreValue(int x, int y, int mapWidth, int mapHeight)
        {
            return new Point((x % mapWidth + mapWidth) % mapWidth, (y % mapHeight + mapHeight) % mapHeight);
        }

        public int SetInBounds(int value, int mini, int maxi)
        {
            if (value >= mini && value <= maxi)
                return value;
            return value < mini ? 0 : maxi;
        }

        public Cell[,] CpyMap(CellMap map)
        {
            return CpyMap((Cell[,]) map, map.Width, map.Height);
        }
        
        public Cell[,] CpyMap(Cell[,] map, int mapWidth, int mapHeigh)
        {
            Cell[,] newMap = new Cell[mapWidth, mapHeigh];
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeigh; y++)
                {
                    newMap[x, y] = new Cell { Color = map[x, y].Color };
                }
            }
            return newMap;
        }
    }
}
