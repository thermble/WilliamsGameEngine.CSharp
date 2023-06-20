using GameEngine;
using Microsoft.VisualBasic;
using SFML.System;
using System.Runtime.InteropServices;

namespace MyGame
{
    class GameScene : Scene
    {
        private int _score;
        private int _lives = 1;
        int stagenumbpass = 0;
        public GameScene(int stagenumber) //map data store efficiently on a list pls
        {
            
            Score score = new Score(new Vector2f(10.0f, 10.0f));
            AddGameObject(score);
            stagenumbpass=stagenumber;
            switch (stagenumber)
            {
                case 1: //simple platform with an elevation.
                    Ship ship = new Ship(new Vector2f(100.0f, 700.0f));
                    AddGameObject(ship);
                    Floor floor = new Floor(new Vector2f(1000.0f, 1000.0f), new Vector2f(70.0f, 10.0f), 11); //real floor
                    AddGameObject(floor);
                    
                    floor = new Floor(new Vector2f(1600.0f, 900.0f), new Vector2f(20.0f, 10.0f), 11);
                    AddGameObject(floor);


                    Exitflag exitflag = new Exitflag(new Vector2f(1800.0f, 705.0f), new Vector2f(1.5f,1.5f),stagenumber); //turn into goalpoint
                    AddGameObject(exitflag);
                    break;
                case 2:
                    ship=new Ship(new Vector2f(100.0f, 400.0f));
                    AddGameObject(ship);
                    floor = new Floor(new Vector2f(200.0f, 900.0f), new Vector2f(30.0f, 20.0f), 11);
                    AddGameObject(floor);
                    floor=new Floor(new Vector2f(1920.0f, 900.0f), new Vector2f(15.0f, 10.0f), 11);
                    AddGameObject(floor);
                    floor=new Floor(new Vector2f(500.0f, 620.0f), new Vector2f(3.0f, 10.0f), 11);
                    AddGameObject(floor);
                    exitflag=new Exitflag(new Vector2f(1800.0f, 705.0f), new Vector2f(1.5f, 1.5f), stagenumber);
                    AddGameObject(exitflag);
                    break;
                case 4:
                    ship=new Ship(new Vector2f(100.0f, 700.0f));
                    AddGameObject(ship);
                    floor = new Floor(new Vector2f(200.0f, 900.0f), new Vector2f(20.0f, 10.0f), 11);
                    AddGameObject(floor);
                    floor=new Floor(new Vector2f(1920.0f, 900.0f), new Vector2f(20.0f, 10.0f), 11);
                    AddGameObject(floor);
                    exitflag=new Exitflag(new Vector2f(1800.0f, 705.0f), new Vector2f(1.5f, 1.5f),stagenumber);
                    AddGameObject(exitflag);
                    break;
                case 3: //up and down, maybe around 
                    ship=new Ship(new Vector2f(100.0f, 700.0f));
                    AddGameObject(ship);
                    floor = new Floor(new Vector2f(1000.0f, 900.0f), new Vector2f(70.0f, 10.0f), 11);
                    AddGameObject(floor);
                    floor = new Floor(new Vector2f(960.0f, 600.0f), new Vector2f(5.0f, 30.0f),11);
                    AddGameObject(floor);
                    exitflag= new Exitflag(new Vector2f(1600.0f, 705.0f), new Vector2f(1.5f, 1.5f), stagenumber);
                    AddGameObject(exitflag);
                    break;
                case 5:
                    ship = new Ship(new Vector2f(100.0f, 900.0f));
                    AddGameObject(ship);
                    floor = new Floor(new Vector2f(100.0f, 1000.0f), new Vector2f(8.0f, 3.0f), 11);
                    AddGameObject(floor);

                    break;

            }



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
        public int Stagenumwhar()
        {
            return stagenumbpass;
        }
        public void Nextstage()
        {
            //setscene next whatever
        }
        public void DecreaseLives()
        {
            --_lives;
            if (_lives<=0)
            {
                /*GameOverScene gameOverScene = new GameOverScene(_score);
                Game.SetScene(gameOverScene);*/
                GameScene scene = new GameScene(stagenumbpass);
                Game.SetScene(scene);
            }
        }
    }
}