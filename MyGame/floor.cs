/*using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace MyGame
{
    class Floor : GameObject
    {
        private readonly Sprite _sprite = new Sprite();
        public Floor(Vector2f pos)
        {
            _sprite.Texture=Game.GetTexture("Resources/ship.png");
            _sprite.Position= pos;
            pos.X=pos.X+_sprite.TextureRect.Width;

            AssignTag("floor");
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
        public override void Update(Time elapsed)
        {
            int msElapsed = elapsed.AsMilliseconds();
            Vector2f pos = _sprite.Position;
        }
    }
}
*/
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
    class Floor : GameObject
    {

        private readonly Sprite _sprite = new Sprite();
        Vector2f scaleshare = new Vector2f(0.0f, 0.0f);
        private IntRect[] sprts = new IntRect[]  //AAAAAAAAAAAAH
            {
                new IntRect(  0, 0, 32, 32), //diamond     0
                new IntRect( 32, 0, 32, 32),//diagmond     1
                new IntRect(64, 0, 32, 32),//bigcircl      2
                new IntRect(0, 32, 32, 32),//circl         3

                new IntRect(32, 32, 32, 32),//smalcirc      4
                new IntRect(64, 32, 32, 32),//line         5
                new IntRect(0, 64, 32, 32),//poofcloud    6
                new IntRect(32, 64, 32, 32),//groundfast   7

                new IntRect(64, 64, 32, 32),//groundfast2   8
            };
        public Floor(Vector2f pos, Vector2f scale, int sch)
        {
            
            _sprite.Texture= Game.GetTexture("C:/Users/gouldre/source/repos/WilliamsGameEngine.CSharp/MyGame/Resources/floors.png");
            _sprite.TextureRect=sprts[sch];
            _sprite.Position = pos;
            _sprite.Scale=scale;
             scaleshare = scale;
            _sprite.Origin=new Vector2f(16, 16);
            
            AssignTag("floor");
            SetCollisionCheckEnabled(true);
        }
        public Vector2f GetScaleM()
        {
            return _sprite.Scale; //returns multipliers.
        }
        public Vector2f GetBaseDim()
        {
            return new Vector2f(32, 32);
        }
        public Vector2f GetCenter()
        {
            return _sprite.Position;
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
            base.HandleCollision(otherGameObject);
        }
        public override void Update(Time elapsed)
        {
            int msElapsed = elapsed.AsMilliseconds();
            Vector2f pos = _sprite.Position;
            pos = _sprite.Position;
        }
    }
}
