using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EscapeTheWindow
{
    public class Map
    {
        public Game2 Game;
        public Tile[,] TileMap;

        public Tile GetTile(int x, int y)
        {
            if (x >= 0 && x <= 16*(int)Math.Pow(2, Taille) && y >= 0 && y <= 9*(int) Math.Pow(2, Taille))
            {
                return TileMap[x, y];
            }
            return null;
        }

        public int Taille { get; private set; }

        public Map(Game2 game)
        {
            Game = game;
            Taille = 0;
            TileMap = new Tile[(16*((int) Math.Pow(2, Taille))), (9*((int) Math.Pow(2, Taille)))];
            for (int x = 0; x < (16*(Math.Pow(2, Taille))); x++)
            {
                for (int y = 0; y < (9*(Math.Pow(2, Taille))); y++)
                {
                    TileMap[x, y] = new Tile('0',
                        new Vector2(x*game.ScreenWidth/(float) (16*(int) Math.Pow(2, Taille)),
                            y*game.ScreenHeight/(float) (9*(int) Math.Pow(2, Taille))), this, Taille);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int x = 0; x < (16*(Math.Pow(2, Taille))); x++)
            {
                for (int y = 0; y < (9*(Math.Pow(2, Taille))); y++)
                {
                    TileMap[x, y].Draw(sb);
                }
            }
        }

        public void LoadMap(string mapName)
        {
            Taille = Int16.Parse("" + mapName[0]);
            TileMap = new Tile[(16*2 ^ Taille), (9*2 ^ Taille)];
            StreamReader reading = new StreamReader("../../../Content/Maps/" + mapName + ".txt");
            var line = reading.ReadLine();
            for (int y = 0; y < (9*(Math.Pow(2, Taille))); y++)
            {
                for (int x = 0; x < (16*(Math.Pow(2, Taille))); x++)
                {
                    if (line != null)
                    {
                        char current = line[x];
                        TileMap[x, y] = new Tile(current,
                            new Vector2(x*Game.ScreenWidth/(float) (16*(int) Math.Pow(2, Taille)),
                                y*Game.ScreenHeight/(float) (9*(int) Math.Pow(2, Taille))), this, Taille);
                    }
                }
                line = reading.ReadLine();
            }
        }

        public void Update(GameTime gt)
        {
            for (int y = 0; y < (9*(Math.Pow(2, Taille))); y++)
            {
                for (int x = 0; x < (16*(Math.Pow(2, Taille))); x++)
                {
                    TileMap[x, y].Update();
                }
            }
        }
    }
}
