using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace MyGame
{
    class Sillything : AnimatedSprite
    {
        //test animations from a spritesheet, and test changing the current animation with button inputs.
        private const int DefaultMsPerFrame = 60;
        private string currentanim = "nil";
        bool tfl = false; //placeholder example for touchingfloor
        private readonly Sprite _sprite = new Sprite();
        float xspeed = 0f;
        float yspeed = 0f;
        float accel = 0f;
        float decel = 0f;
        float xcalerate = 0.0f;
        float ycalerate = 0.0f;
        double lifespanms = 0.0;
        int facingdirection = 0; //0 left, 1 right



        private IntRect[] sprts = new IntRect[]  //AAAAAAAAAAAAH
            {
                new IntRect(  0, 0, 16, 16), //diamond     0
                new IntRect( 16, 0, 16, 16),//diagmond     1
                new IntRect(32, 0, 16, 16),//bigcircl      2
                new IntRect(48, 0, 16, 16),//circl         3

                new IntRect(0, 16, 16, 16),//smalcirc      4
                new IntRect(16, 16, 16, 16),//line         5
                new IntRect(32, 16, 16, 16),//poofcloud    6
                new IntRect(48, 16, 16, 16),//groundfast   7

                new IntRect(0, 32, 16, 16),//groundfast2   8
                new IntRect( 16, 32, 16, 16),//groundfast3 9
                new IntRect(32,32,16,16),//fastobj         10
                new IntRect(48,32,16,16),//slash1          11

                new IntRect(0, 48,16,16),//slash2          12
                new IntRect(16,48,16,16),//slash3          13
                new IntRect(32,48,16,16),//slash4          14
                new IntRect(48,48,16,16),//triangle?       15
            };

        
        public Sillything(Vector2f pos, Vector2f scale, int sch, int facingdirection, float xsp, float ysp, float accelerate, float xsac, float ysac, int dietimer):base(pos) //add scaleration and acceleration.
        {
            _sprite.Texture= Game.GetTexture("C:/Users/gouldre/source/repos/WilliamsGameEngine.CSharp/MyGame/Resources/particlefx.png");
            _sprite.TextureRect=sprts[sch];
            _sprite.Position=pos;
            _sprite.Scale=scale;
            xcalerate=xsac;
            ycalerate=ysac;
            xspeed=xsp;
            yspeed=ysp;
            lifespanms=dietimer;
            switch (sch)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    _sprite.Origin=new Vector2f(8, 8);
                  //  _sprite.Scale=new Vector2f(-1.0f-(Math.Abs(xspeed)), 1.0f+(yspeed*0.75f));


                    //modifiers to xspeed yspeed scaleration.
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                    

                case 15:
                    SetOriginMode(OriginMode.BottomRight);
                    switch (facingdirection)
                    {
                        case 0:
                            _sprite.Scale=new Vector2f(-scale.X, scale.Y);

                            break;
                        case 1:
                            _sprite.Scale=new Vector2f(scale.X, scale.Y);
                            break;
                    }

                    break;


            }
            //make a single sprite taker of the full image, and catalog each particle inside the function

        } 
        double decaytimer = 0;

        public override void Update(Time elapsed)
        {
            int msElapsed = elapsed.AsMilliseconds();
            decaytimer+=msElapsed;
            Vector2f pos = _sprite.Position;
            float x = pos.X;
            float y = pos.Y;
            float sx = _sprite.Scale.X;
            float sy = _sprite.Scale.Y;
            x+=xspeed*msElapsed;
            y+=yspeed*msElapsed;

            sx+=xcalerate*msElapsed;

            sy+=ycalerate*msElapsed;

            _sprite.Position=new Vector2f(x, y);
            _sprite.Scale=new Vector2f(sx, sy);
            if (decaytimer>=lifespanms)
            {
                MakeDead();
            }

        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }


    }
}
