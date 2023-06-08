using GameEngine;
using SFML.System;

namespace MyGame
{
    class GameScene : Scene
    {
        private int _score;
        private int _lives = 1;
        public GameScene()
        {
            Ship ship = new Ship(new Vector2f(100.0f,100.0f));
            AddGameObject(ship);
            Floor floor = new Floor(new Vector2f(1000.0f, 900.0f), new Vector2f(60.0f, 10.0f),0);
            AddGameObject(floor);
            floor = new Floor(new Vector2f(1200.0f, 600.0f), new Vector2f(2.0f, 10.0f), 6);
            AddGameObject(floor);


            Box box = new Box(new Vector2f(400.0f, 200.0f), new Vector2f(1.0f, 1.0f)); //turn into goalpoint
            AddGameObject(box);

            Score score = new Score(new Vector2f(10.0f, 10.0f));
            AddGameObject(score);
        }
        public int GetScore()
        {
            return _score;
        }
        public void IncreaseScore() //what.  it just has ++score. ok.
        {
            ++_score;
        }
        public int GetLives()
        {
            return _lives;
        }
        public void DecreaseLives()
        {
            --_lives;
            if (_lives<=0)
            {
                /*GameOverScene gameOverScene = new GameOverScene(_score);
                Game.SetScene(gameOverScene);*/
                GameScene scene = new GameScene();
                Game.SetScene(scene);
            }
        }
    }
}