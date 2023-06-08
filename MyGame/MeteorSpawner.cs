using GameEngine;
using SFML.Graphics;
using SFML.System;
using System.Runtime.Serialization;

namespace MyGame
{
    class MeteorSpawner : GameObject
    {
        //#ms between meteor spawn.
        private const int SpawnDelay = 999999999;
        private int _timer;
        public override void Update(Time elapsed)
        {
            //determines time passed and adjust timer.
            int msElapsed = elapsed.AsMilliseconds();
            _timer-=msElapsed;

            //if the timer elapses, the meteor spawns.
            if(_timer<=0)
            {
                _timer=SpawnDelay;
                Vector2u size = Game.RenderWindow.Size;

                //spawning the meteor off the right side of screen.
                //also its no more than 100px wide, so
                float meteorX = size.X+100;

                //spawn meteor along the height of window at random.
                float meteorY = Game.Random.Next() % size.Y;

                //Create a metooor!!! also adds to scene. NO METEOR!!!
                /*Meteor meteor = new Meteor(new Vector2f(meteorX, meteorY));
                Game.CurrentScene.AddGameObject(meteor);*/
            }
        }
    }
}
