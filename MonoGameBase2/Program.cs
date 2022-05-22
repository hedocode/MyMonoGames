using Microsoft.Xna.Framework;

namespace GameBaseArilox
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Game game = new GameSelector())
            {
                game.Run();
            }
        }
    }
}
