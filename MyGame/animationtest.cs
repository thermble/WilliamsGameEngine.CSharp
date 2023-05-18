using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace MyGame
{
    class Sillything : AnimatedSprite
    {
        //test animations from a spritesheet, and test changing the current animation with button inputs.
        private const int DefaultMsPerFrame = 60;
        private string currentanim = "nil";
        bool tfl = false; //placeholder example for touchingfloor
        

        public Sillything(Vector2f pos):base(pos) //base pos might be for checking per frame. test later.
        {
            Texture = Game.GetTexture("C:/Users/gouldre/source/repos/WilliamsGameEngine.CSharp/MyGame/Resources/particlefx.png");
            


        }
        public override void Update(Time elapsed)
        {
            
            base.Update(elapsed);
            IsPlaying();
            
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                if (!IsPlaying()||currentanim!="run")
                {


                    Anmexmp();
                    PlayAnimation("run", AnimationMode.OnceForwards);
                }
                                   
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                if(!IsPlaying()||currentanim=="nil")
                {
                    Anmxp2();
                    PlayAnimation("jump", AnimationMode.OnceForwards);
                }

            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.U))
            {
                
            }
            if (!Keyboard.IsKeyPressed(Keyboard.Key.Up)&&!Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                currentanim="nil";
            }
        }
        public void Spritetotal()
        {
            var plrsp = new List<IntRect>
            {
                new IntRect(  0, 0, 16, 16), //diamond
                new IntRect( 16, 0, 16, 16),//diagmond
                new IntRect(32, 0, 16, 16),//bigcircl
                new IntRect(48, 0, 16, 16),//circl

                new IntRect(0, 16, 16, 16),//smalcirc
                new IntRect(16, 16, 16, 16),//line
                new IntRect(32, 16, 16, 16),//poofcloud
                new IntRect(48, 16, 16, 16),//groundfast

                new IntRect(0, 32, 16, 16),//groundfast2
                new IntRect( 16, 32, 16, 16),//groundfast3
                new IntRect(32,32,16,16),//fastobj
                new IntRect(48,32,16,16),//slash1

                new IntRect(0, 48,16,16),//slash2
                new IntRect(16,48,16,16),//slash3
                new IntRect(32,48,16,16),//slash4
                new IntRect(48,48,16,16),//triangle?
            };
        }
        private void Anmexmp()
        {
            var frames = new List<IntRect>
            {
                new IntRect(0,0,16,16),
                new IntRect(16,0,16,16),

            };
            AddAnimation("run", frames);
            currentanim="run";
        }
        private void Anmxp2()
        {
            var frames = new List<IntRect>
            {
                new IntRect(48,32,16,16),
                new IntRect(0,48,16,16),
                new IntRect(16,48,16,16),
                new IntRect(32,48,16,16),

            };
            AddAnimation("jump", frames);
            currentanim="jump";
        }
    }
}
