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
    class Box : GameObject
    {
        private readonly Sprite _sprite = new Sprite();
        public Box(Vector2f pos, Vector2f scale)
        {
            _sprite.Texture=Game.GetTexture("C:/Users/gouldre/source/repos/WilliamsGameEngine.CSharp/MyGame/Resources/box.png");
            _sprite.Position=pos;
            _sprite.Scale=scale;
            //32x32, create collision "lines" on edges, distance and height multiplied by scale. i yearn for satisfaction
            AssignTag("box");
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
            base.HandleCollision(otherGameObject);
        }
        public override void Update(Time elapsed)
        {
            int msElapsed=elapsed.AsMilliseconds();
            Vector2f pos = _sprite.Position;
            _sprite.Position=pos;
        }
    }
}
