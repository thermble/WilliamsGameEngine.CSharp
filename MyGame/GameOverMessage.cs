using GameEngine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MyGame
{
    class GameOverMessage:GameObject
    {
        private readonly Text _text = new Text();
        public GameOverMessage(int score)
        {
            _text.Font=Game.GetFont("Resources/Courneuf-Regular.ttf");
            _text.Position=new Vector2f(50.0f, 50.0f);
            _text.CharacterSize=48;
            _text.FillColor=Color.Red;
            _text.DisplayedString="YOU DIED\n\nbut you got this score: "+score+"\n\nPRESS ENTER TO DO IT ALL OVER";


        }
        public override void Draw()
        {
            Game.RenderWindow.Draw(_text);
        }
        public override void Update(Time elapsed)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                GameScene scene = new GameScene(1);
                Game.SetScene(scene);
            }
        }
    }
}
