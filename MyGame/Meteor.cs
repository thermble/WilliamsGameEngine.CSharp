﻿using GameEngine;
using SFML.Graphics;
using SFML.System;
using System;
using System.Reflection.Metadata.Ecma335;

namespace MyGame
{
    class Meteor :GameObject
    {
        private const float Speed = 0.2f;
        private readonly Sprite _sprite = new Sprite();
        public Meteor(Vector2f pos)
        {
            _sprite.Texture=Game.GetTexture("Resources/meteor.png");
            _sprite.Position=pos;
            AssignTag("meteor");
            SetCollisionCheckEnabled(true);
        }

        public override void Draw()
        {
            Game.RenderWindow.Draw(_sprite);
        }
        public override void Update(Time elapsed)
        {
            int msElapsed = elapsed.AsMilliseconds();
            Vector2f pos = _sprite.Position;

            if(pos.X < _sprite.GetGlobalBounds().Width*-1)
            {
                GameScene scene = (GameScene)Game.CurrentScene;
                scene.DecreaseLives();
                
                MakeDead();
            }
            else
            {
                _sprite.Position=new Vector2f(pos.X-Speed*msElapsed, pos.Y);
            }
        }
        public override FloatRect GetCollisionRect()
        {
            return _sprite.GetGlobalBounds();
        }
        public override void HandleCollision(GameObject otherGameObject)
        {
            if (otherGameObject.HasTag("laser"))
            {
                otherGameObject.MakeDead();
                GameScene scene = (GameScene)Game.CurrentScene;
                scene.IncreaseScore();
            }
            Vector2f pos = _sprite.Position;
            pos.X=pos.X+(float)_sprite.GetGlobalBounds().Width/2.0f;
            pos.Y=pos.Y+(float)_sprite.GetGlobalBounds().Height/2.0f;
            Explosion explosion = new Explosion(pos);
            Game.CurrentScene.AddGameObject(explosion);
            MakeDead();
        }
    }
}
