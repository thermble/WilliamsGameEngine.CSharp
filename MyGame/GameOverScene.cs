using GameEngine;

namespace MyGame
{
    internal class GameOverScene:Scene
    {
        public GameOverScene(int score)
        {
            GameOverMessage gameOverMessage = new GameOverMessage(score);
            AddGameObject(gameOverMessage);
        }
    }
}
