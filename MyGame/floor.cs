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

        public Floor(Vector2f pos, Vector2f scale)
        {
            _sprite.Texture = Game.GetTexture("Resources/floor.png");
            _sprite.Scale=new Vector2f(1.0f, 1.0f);
            _sprite.Position = pos;
            _sprite.Scale=scale;
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
