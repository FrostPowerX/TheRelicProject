using SFML.System;
using SFML.Graphics;
using Font = SFML.Graphics.Font;
using KeyEventArgs = SFML.Window.KeyEventArgs;
using Color = SFML.Graphics.Color;
using View = SFML.Graphics.View;

namespace The_Relic
{
    internal class GameOverState : LoopState
    {
        Texture texture;
        Sprite creditsSprite;

        Button mainMenu;
        Button retry;

        float posOffSet = 64;
        float separationOffSet = 128;

        public event Action OnMenuPressed;
        public event Action OnRetryPressed;

        public GameOverState(RenderWindow window) : base(window) { }

        protected override void Start()
        {
            string creditsFilePath = "Assets/Sprites/Game Over.png";
            string buttonImageFilePath = "Assets/Sprites/Button.png";
            string buttonFontFilePath = "Assets/Fonts/Olivia.ttf";

            Vector2f mainMenuPos = new Vector2f(window.Size.X / 2 + separationOffSet, window.Size.Y / 1.8f + posOffSet);
            Vector2f retryPos = new Vector2f(window.Size.X / 2 - separationOffSet, window.Size.Y / 1.8f + posOffSet);

            texture = new Texture(creditsFilePath);
            creditsSprite = new Sprite(texture);

            mainMenu = new Button(window, buttonImageFilePath, buttonFontFilePath, mainMenuPos, "Menu");
            retry = new Button(window, buttonImageFilePath, buttonFontFilePath, retryPos, "Retry");

            mainMenu.SetScale(new Vector2f(0.5f, 0.5f));
            retry.SetScale(new Vector2f(0.5f, 0.5f));

            Vector2f halfCredits = new Vector2f(creditsSprite.GetGlobalBounds().Width / 2, creditsSprite.GetGlobalBounds().Height / 2);

            creditsSprite.Origin = halfCredits;
            creditsSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);

            mainMenu.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            retry.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);

            View view = new View(window.GetView());

            view.Center = new Vector2f (window.Size.X / 2, window.Size.Y / 2);

            window.SetView(view);

            mainMenu.OnPressed += OnMenuPressed;
            retry.OnPressed += OnRetryPressed;
            window.Closed += OnCloseWindow;
        }

        protected override void Update(float deltaTime)
        {
            Cursor.Update(deltaTime, window);

            mainMenu.Update();
            retry.Update();
        }

        protected override void Draw()
        {
            window.Draw(creditsSprite);
            mainMenu.Draw();
            retry.Draw();

            Cursor.Draw(window);
        }

        protected override void Finish()
        {
            mainMenu.OnPressed -= OnMenuPressed;
            retry.OnPressed -= OnRetryPressed;
            window.Closed -= OnCloseWindow;
        }
    }
}
