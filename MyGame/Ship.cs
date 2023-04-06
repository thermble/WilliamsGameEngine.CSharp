using GameEngine;
using SFML.Graphics; //kicking resets vertical velocity and gives you a quickly decaying boost in the direction you kicked. kicking locks you in the direction you kick/bounce for the duration of kick/bound.
using SFML.System; //try for a vertical velocity value, if (floortouch=false) {fall exponentially faster up to a speed limit} 
using SFML.Window; //jumping adds a good chunk of vertical velocity that slowly decreases from not actively touching a floor.
                    //slight acceleration to running side to side. should be there just to make going from still to running not jarring.
namespace MyGame    //maybe ledge grabbing? if kickingstate=false grab ledge
{                   //reverse horizontal velocity and give a little jump when bounding. 
    class Ship : GameObject //maybe some special walls you can't hop off of? Maybe some walls that you can break *through* with kick and walls that break and still bounce you.
    {
        private const float Speed = 0.05f; 
        private const int FireDelay = 170;
        private int _fireTimer;
        private readonly Sprite _sprite = new Sprite();
        const float accel = 0.03f;
        const float maxspeed = 0.3f;
        float xspeed = 0;
        float yspeed = 0f;
        const float restspeed = 0;
        int driftcheck =0;
        //creates ship!!!
        public Ship()
        {
            _sprite.Texture=Game.GetTexture("Resources/ship.png");
            _sprite.Position=new Vector2f(100, 100);
        }

        //functs overriden from gameobject (???)
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            Vector2f pos = _sprite.Position;
            float x = pos.X;
            float y = pos.Y;
            

            int msElapsed = elapsed.AsMilliseconds();
                                                                //Add collision to borders.
            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) { if (yspeed>-maxspeed) { yspeed-=accel; } else { yspeed=-maxspeed; } y+=yspeed*msElapsed; } else
            {
                if (yspeed<restspeed) { yspeed+=accel; }
                y+=yspeed*msElapsed;
                driftcheck = msElapsed;
            }//RISE, MY CREATION!
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) { if (yspeed<maxspeed) { yspeed+=accel; } else { yspeed=maxspeed; } y+=yspeed*msElapsed; } else
            {
                if (yspeed>restspeed) { yspeed-=accel; }
                y+=yspeed*msElapsed;
                driftcheck = msElapsed;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) { if (xspeed>-maxspeed) { xspeed-=accel; } else { xspeed=-maxspeed; } x+=xspeed*msElapsed; } else
            {
                if (xspeed<restspeed) { xspeed+=accel; }
                x+=xspeed*msElapsed;
                driftcheck = msElapsed;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { if (xspeed<maxspeed) { xspeed+=accel; } else { xspeed=maxspeed; } x+=xspeed*msElapsed; } else
            {
                if (xspeed>restspeed) { xspeed-=accel; }
                x+=xspeed*msElapsed;
                driftcheck=msElapsed;
            }
            if (!Keyboard.IsKeyPressed(Keyboard.Key.D)&&!Keyboard.IsKeyPressed(Keyboard.Key.A)&&!Keyboard.IsKeyPressed(Keyboard.Key.S)&&!Keyboard.IsKeyPressed(Keyboard.Key.W))//after a certain amount of time has passed, clear both Xspeed and Yspeed
            {
                if(xspeed<0.03f&&xspeed>-0.03f)//make the check equal to the accel or slightly greater
                {
                    xspeed=0;
                }
                if(yspeed<0.03f&&yspeed>-0.03f)
                {
                    yspeed=0;
                }
            }
                
            _sprite.Position=new Vector2f(x, y);

            if (_fireTimer>0) { _fireTimer-=msElapsed; }
            
            if(Keyboard.IsKeyPressed(Keyboard.Key.Space)&& _fireTimer<=0)
            {
                _fireTimer=FireDelay;
                FloatRect bounds = _sprite.GetGlobalBounds();
                float laserX = x+bounds.Width;
                float laserY = y+bounds.Height/2.0f;
                Laser laser = new Laser(new Vector2f(laserX, laserY));
                float laserX2 = x+bounds.Width;
                float laserY2 = y+bounds.Height/1.3f;
                Laser laser2 = new Laser(new Vector2f(laserX2, laserY2));
                float laserX3 = x+bounds.Width;
                float laserY3 = y+bounds.Height/4.0f;
                Laser laser3 = new Laser(new Vector2f(laserX3, laserY3));
                Game.CurrentScene.AddGameObject(laser);
                Game.CurrentScene.AddGameObject(laser2);
                Game.CurrentScene.AddGameObject(laser3);
            }
        }
    }
}
