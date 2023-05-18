using GameEngine;

namespace MyGame
{
    static class MyGame
    {
        private const int WindowWidth = 1920;
        private const int WindowHeight = 1080;

        private const string WindowTitle = "almost platformer";

        private static void Main(string[] args)
        {
            // Initialize the game.
            Game.Initialize(WindowWidth, WindowHeight, WindowTitle);

            // Create our scene.
            GameScene scene = new GameScene();
            Game.SetScene(scene);

            // Run the game loop.
            Game.Run();
        }
    }
}