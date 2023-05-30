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
            Floor floor = new Floor(new Vector2f(100.0f, 300.0f), new Vector2f(20.0f, 1.5f));
            AddGameObject(floor);
            floor = new Floor(new Vector2f(800.0f, 500.0f), new Vector2f(3.0f, 1.0f));
            AddGameObject(floor);
            Ceiling ceiling = new Ceiling(new Vector2f(150.0f, 200.0f),new Vector2f(5.0f, 1.0f));
            AddGameObject(ceiling);
            Wall wall = new Wall(new Vector2f(300.0f, 100.0f), new Vector2f(1.0f, 4.0f));
            AddGameObject(wall);

            Box box = new Box(new Vector2f(400.0f, 200.0f), new Vector2f(1.0f, 1.0f));
            AddGameObject(box);




            MeteorSpawner meteorSpawner = new MeteorSpawner();
            AddGameObject(meteorSpawner);

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
                GameOverScene gameOverScene = new GameOverScene(_score);
                Game.SetScene(gameOverScene);
            }
        }
    }
}