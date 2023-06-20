using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Exitflag : GameObject
    {
        private readonly Sprite _sprite = new Sprite();
        int flagloop = 0;
        float unitimer= 3.0f;
        int animtimer = 0;
        int garbaj = 0;

        public Exitflag(Vector2f pos, Vector2f scale, int stagenum)
        {
            _sprite.Texture=Game.GetTexture("C:/Users/gouldre/source/repos/WilliamsGameEngine.CSharp/MyGame/Resources/flagss.png");
            _sprite.Position=pos;
            _sprite.Scale=scale;
            garbaj=stagenum;
            _sprite.Origin=new Vector2f(16,24);
            SetCollisionCheckEnabled(true);
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            if (otherGameObject.HasTag("player"))
            {
                GameScene newscene = new GameScene(garbaj+1);
                
                Game.SetScene(newscene);
                //call from gamescene
            }
        }
        public override void Update(Time elapsed)
        {
            unitimer-=0.1f;
            var scrongle = new IntRect[] // List<IntRect>
{
                new IntRect(  0, 0, 32, 48), //jump1  0
                new IntRect( 32, 0, 32, 48),//jump2   1
                new IntRect(64, 0, 32, 48),//jump3    2
                new IntRect(0, 48, 32, 48),//land    3
                new IntRect(32, 48, 32, 48),//idle     4


}; //🤮  get this out of update when possible
            int msElapsed = elapsed.AsMilliseconds();
            Vector2f pos = _sprite.Position;
            _sprite.TextureRect=scrongle[animtimer%5];
            if (unitimer<=0)
            {
                unitimer=1.5f;
                animtimer++;
            }
            
        }
    }
}
