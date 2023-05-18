using GameEngine;
using SFML.Graphics; //kicking resets vertical velocity and gives you a quickly decaying boost in the direction you kicked. kicking locks you in the direction you kick/bounce for the duration of kick/bound.
using SFML.System; //try for a vertical velocity value, if (floortouch=false) {fall exponentially faster up to a speed limit} 
using SFML.Window; //jumping adds a good chunk of vertical velocity that slowly decreases from not actively touching a floor.
using System;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static GameEngine.AnimatedSprite;
/*make side to side movement on ground greater than aerial side to side movement
Make character do a sliding turn change if you hold a direction and are currently "sliding" (holding A, moving positive X still)
make character have stages to falling (if gravaccel is greater than blabla then rising, if >___ && <___ then arc of jump, if gravaccel big negative then falling)
character will have a small "dashing" or running start animation when starting a run. (holding A, barely moving positive X until >___)
MAKE SURE TO USE tfl AND IsKeyPressed FOR ANIMATIONS
maybe spawn an animation of wind every time you jump. Landing too. Maybe a little effect per step? Sounds for said frames?
make game border big and sprite of character smaller.
shift is kick until i figure out how to have individual W inputs.
if kicking while grounded, gain a boost forward greater than basic grounded movement and a very small jump up.

Add using ___;s of a Class tailored for the animations. Such as FacingLeft or Sliding. (beware when making sliding sprite face correctly.)
don't clip to floors if too far below them.

There will be a special state of how many consecutive wallkicks have been used for things like a requirement of wallkicks to break through something or to hit an enemy, etc.


MAKE EVERYTHING AN ANIMATION :)))))))))))
*/
namespace MyGame    //maybe ledge grabbing? if kickingstate=false grab ledge
{                   //reverse horizontal velocity and give a little jump when bounding. 
    class Ship : AnimatedSprite //maybe some special walls you can't hop off of? Maybe some walls that you can break *through* with kick and walls that break and still bounce you.
    {
        //lots of garbo stuff here------------------------------------------------------
        private const float Speed = 0.05f;
        private const int FireDelay = 170;
        private int _fireTimer = 0; //weird update method to tell certain animations to update faster or slower.
        private readonly Sprite _sprite = new Sprite(); //make every sprite for the character a short animation :)    48x61
        const float gravaccel = 0.02f; //affects gravity.
        const float grndacc = 0.03f; //any instance that has different accel properties between ground and air shall have an if for the bool.
        const float airacc = 0.015f;//acceleration in the air
        const float maxspeed = 0.65f; //use for horizontal. make these not constant to be manipulated during kickstates etc.
        float tmaxspeed = 0.25f; //maxspeed for kicking. completely caps out speed to avoid going stupid high speeds.
        float xspeed = 0f; //horiz movement
        float yspeed = 0f;
        const float restspeed = 0;
        int driftcheck = 0;
        bool tfl = false; //touchinglfoor
        bool twl = false;//touching wall
        bool tcl = false;
        const float gravcap = 0.8f;
        int floorcheck = 0;
        float floorpos = 0f;
        bool kicking = false;
        float timer = 1.5f; //speed of animations. can make separate timers for faster or slower anims.
        int buttonstate = 0; //0 is left, 1 is right
                                          //         runloop generates 7,8,9,10 from the 0,1,2,3 that the Modulus generates.
        List<int> RUNLOOP = new List<int> { 7, 8, 9, 10 }; //refers to list with scrongle[RUNLOOP[ANMCOUNTER%(framecount)]]
        List<int> FALLLOOP = new List<int> { 0, 1, 2 };  //IF ANYTHING NEEDS ANIMATING WITH OR WITHOUT SPEED FACTORED, USE ABOVE METHOD.
        int ANMCOUNTER = 0;


        //add timer resetter at bottom
        //------------------------------------------------------------------------------

        //creates ship!!!
        public Ship(Vector2f pos):base(pos)
        {
            var scrongle = new IntRect[] // List<IntRect>
            {
                new IntRect(  0, 0, 48, 61), //jump1  1
                new IntRect( 48, 0, 48, 61),//jump2   2
                new IntRect(96, 0, 48, 61),//jump3    3
                new IntRect(144, 0, 48, 61),//land    4
                new IntRect(0, 61, 48, 61),//idle     5
                new IntRect(48, 61, 48, 61),//idle2    6

                new IntRect(96, 61, 48, 61),//dash    7
                new IntRect(144, 61, 48, 61),//run   8
                new IntRect(0, 122, 48, 61),//run2    9
                new IntRect( 48, 122, 48, 61),//run3  10

                new IntRect(96,122,48,61),//run4      11
                new IntRect(144,122,48,61),//kick   12
                new IntRect(0,183,48,61),//bounce 13
            };//🤮 i need a more efficient method of getting the spritesheet!!!!!!!
            _sprite.Texture=Game.GetTexture("C:/Users/gouldre/source/repos/WilliamsGameEngine.CSharp/MyGame/Resources/playerspr.png");
            _sprite.TextureRect=scrongle[5];
            _sprite.Position=new Vector2f(100, 100);
            _sprite.Origin =  new Vector2f(24, 61);
            
        }

        //functs overriden from gameobject (???)
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            if (otherGameObject.HasTag("floor"))
            {

                
                Vector2f flcv = _sprite.Position;
                Floor f = (Floor)otherGameObject;
                FloatRect flc = f.GetCollisionRect();
                tfl=true;
                floorpos=flc.Top;   //implement a check when you're too far below the floor to properly clip.
            }
            else if (otherGameObject.HasTag("wall"))
            {
                twl=true;
                //etc
            }
            Console.WriteLine(otherGameObject);
        }
        public void SpeedBasedUpd()
        {

            switch (buttonstate)
            {
                case 0:
                    _sprite.Scale=new Vector2f(-1.0f-(Math.Abs(xspeed)), 1.0f+(yspeed*0.75f));
                    return;
                case 1:
                    _sprite.Scale=new Vector2f(1.0f+(Math.Abs(xspeed)), 1.0f+(yspeed*0.75f));
                    return;

            }
            

                


            //the higher the Xspeed, the more stretch there is
            //the more+ the Yspeed, the more squished. the -less yspeed, the more stretched.

        }

        public override void Update(Time elapsed)
        {
            Console.WriteLine(xspeed);
            timer-=(Math.Abs(xspeed));
            
            
            
            Console.WriteLine(timer);
            var scrongle = new IntRect[] // List<IntRect>
            {
                new IntRect(  0, 0, 48, 61), //jump1  0
                new IntRect( 48, 0, 48, 61),//jump2   1
                new IntRect(96, 0, 48, 61),//jump3    2
                new IntRect(144, 0, 48, 61),//land    3
                new IntRect(0, 61, 48, 61),//idle     4
                new IntRect(48, 61, 48, 61),//idle2    5

                new IntRect(96, 61, 48, 61),//dash    6
                new IntRect(144, 61, 48, 61),//run   7
                new IntRect(0, 122, 48, 61),//run2    8
                new IntRect( 48, 122, 48, 61),//run3  9

                new IntRect(96,122,48,61),//run4      10
                new IntRect(144,122,48,61),//kick   11
                new IntRect(0,183,48,61),//bounce 12
                new IntRect(48,183,48,61), //whoops! 13 //MAKE TURNING/SLOWING
                new IntRect(96,183,48,61),//crouch

            }; //🤮  get this out of update when possible
            Vector2f pos = _sprite.Position;
            float x = pos.X;
            float y = pos.Y;
            Console.WriteLine(pos.Y + " "+pos.X);
            if (pos.X<0||pos.X>1920||pos.Y<0||pos.Y>1080)
            {
                GameScene scene = (GameScene)Game.CurrentScene;
                scene.DecreaseLives();
            }
            int msElapsed = elapsed.AsMilliseconds();
            
            
            //Oh boy! here comes the overload of sprite stuff! I am going to gravely regret putting this stuff here later. :)

//////////////////////////////////////////////////
            if (tfl) { yspeed=0;
                _sprite.TextureRect=scrongle[4];
                y=floorpos+1;//just enough height to still be touching. may cause issues later. Ceilings will cause issues if are too tall spritewise
            }else
            {
                if (0.2f>yspeed&&yspeed>-0.2f)
                {
                    _sprite.TextureRect=scrongle[1];
                }
                else if (yspeed>0.2f)
                {
                    _sprite.TextureRect=scrongle[2];
                }
                else if (yspeed<-0.2f)
                {
                    _sprite.TextureRect=scrongle[0];
                }
            }
/////////////////////////////////////////////////  

                                                           //Add collision to borders. also put animation stuff here?
            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && tfl==true) //{ if (yspeed>-maxspeed) { yspeed-=gravaccel; } else { yspeed=-maxspeed; } y+=yspeed*msElapsed; tfl=false; } else
            {
                yspeed-=0.55f;
                tfl=false;
                
                /*if (yspeed<restspeed) { yspeed+=gravaccel; }
                y+=yspeed*msElapsed;
                driftcheck = msElapsed;*/
            }//RISE, MY CREATION!
            if (!tfl) { if (yspeed<gravcap) { yspeed+=gravaccel; } else { yspeed=gravcap; } y+=yspeed*msElapsed; } else
            {
                if (yspeed>restspeed) { yspeed-=gravaccel; }
                y+=yspeed*msElapsed;
                driftcheck = msElapsed;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) { buttonstate=0; if (xspeed>-tmaxspeed) { if (tfl) { _sprite.TextureRect=scrongle[6]; xspeed-=grndacc; } else { xspeed-=airacc; } } else { xspeed=-tmaxspeed; if (tfl) { _sprite.TextureRect=scrongle[RUNLOOP[ANMCOUNTER%4]]; } } x+=xspeed*msElapsed; } else
            {
                if (xspeed<restspeed&&tfl) { xspeed+=grndacc; }
                x+=xspeed*msElapsed;
                driftcheck = msElapsed;
          //if moving right and pressing left, sliding. if slowly or barely moving left, dashing. if normal speed, running.
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) { buttonstate=1; if (xspeed<tmaxspeed) { if (tfl) { _sprite.TextureRect=scrongle[6]; xspeed+=grndacc; } else { xspeed+=airacc; } } else { xspeed=tmaxspeed; if (tfl) { _sprite.TextureRect=scrongle[RUNLOOP[ANMCOUNTER%4]]; } } x+=xspeed*msElapsed; } else
            {
                if (xspeed>restspeed&&tfl){ xspeed-=grndacc; }  //if not touching floor, no deceleration for you :)
                x+=xspeed*msElapsed;
                driftcheck=msElapsed;
                           //if moving left and pressing right, slide. if slowly or barely moving right, dashing. if normal speed, running.
                                          //may make animation play faster as running speeds up.
            }
            if (!Keyboard.IsKeyPressed(Keyboard.Key.D)&&!Keyboard.IsKeyPressed(Keyboard.Key.A))//after a certain amount of time has passed, clear both Xspeed and Yspeed
            {
                if(xspeed<0.03f&&xspeed>-0.03f)//make the check equal to the gravaccel or slightly greater
                {
                    xspeed=0;
                }
            }
           if (Keyboard.IsKeyPressed(Keyboard.Key.S)&&tfl&&!Keyboard.IsKeyPressed(Keyboard.Key.D)&&!Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                _sprite.TextureRect=scrongle[13];
            }
            _sprite.Position=new Vector2f(x, y); 

            if (!kicking&&tmaxspeed>maxspeed) //slowing down after kicking.
            {
                tmaxspeed-=0.01f;
            }


            //if (!tfl) {if (yspeed<gravcap) { yspeed+=gravaccel; }else { yspeed=gravcap; }}            
            
            if (tfl) { yspeed=0; }
            Console.WriteLine(ANMCOUNTER);
            
            

            if (_fireTimer>0) { _fireTimer-=msElapsed; }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space)&& _fireTimer<=0)
            {
                _fireTimer=FireDelay;
                FloatRect bounds = _sprite.GetGlobalBounds();
                float laserX = x+bounds.Width;
                float laserY = y+bounds.Height/2.0f;
                Explosion explosion=new Explosion(new Vector2f(laserX, laserY));
                float laserX2 = x+bounds.Width;
                float laserY2 = y+bounds.Height/1.3f;
                Laser laser2 = new Laser(new Vector2f(laserX2, laserY2));
                float laserX3 = x+bounds.Width;
                float laserY3 = y+bounds.Height/4.0f;
                Laser laser3 = new Laser(new Vector2f(laserX3, laserY3));
                Game.CurrentScene.AddGameObject(explosion);
                Game.CurrentScene.AddGameObject(laser2);
                Game.CurrentScene.AddGameObject(laser3);
            }
            tfl=false;
            SpeedBasedUpd();
            if (timer<=0)
            {
                timer=1.5f;//"time" until next frame
                ANMCOUNTER++;
            }
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
        private void UpdToAnim()
        {
            
        }
    }
}
