using GameEngine;
using SFML.Graphics;
using SFML.System;

namespace MyGame
{
    class Score:GameObject
    {
        private readonly Text _text = new Text();
        public Score(Vector2f pos)
        {
            _text.Font = Game.GetFont("Resources/Courneuf-Regular.ttf");
            _text.Position=pos;
            _text.CharacterSize=25;
            _text.FillColor=Color.White;
            AssignTag("score");
        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
        }
        public override void Update(Time elapsed)
        {
            GameScene scene = (GameScene)Game.CurrentScene;
            _text.DisplayedString="Score: "+scene.GetScore();
        }
    }
}