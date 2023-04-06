using GameEngine;
using SFML.System;

namespace MyGame
{
    class GameScene : Scene
    {
        private int _score;
        private int _lives = 3;
        public GameScene()
        {
            Ship ship = new Ship();
            AddGameObject(ship);
            //lines to test meteor. works.
            //Meteor meteor = new Meteor(new Vector2f(650, 250));
            //AddGameObject(meteor);

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