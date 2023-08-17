using SFML.System;
using SFML.Graphics;
using Font = SFML.Graphics.Font;
using KeyEventArgs = SFML.Window.KeyEventArgs;
using Color = SFML.Graphics.Color;
using View = SFML.Graphics.View;

namespace The_Relic
{
    internal class CreditsMenuStates : LoopState
    {
        Texture texture;
        Sprite creditsSprite;

        float timeAcum;

        public event Action OnBackPressed;

        public CreditsMenuStates(RenderWindow window) : base(window) { }

        private void OnPressBackKey(object sender, KeyEventArgs args)
        {
            if (Input.GetAxis("Cancel") == 1) OnBackPressed?.Invoke();
        }

        protected override void Start()
        {
            string creditsFilePath = "Assets/Sprites/Credits.png";

            texture = new Texture(creditsFilePath);
            creditsSprite = new Sprite(texture);

            Vector2f halfCredits = new Vector2f(creditsSprite.GetGlobalBounds().Width / 2, creditsSprite.GetGlobalBounds().Height / 2);

            creditsSprite.Origin = halfCredits;
            creditsSprite.Position = new Vector2f(window.Size.X / 2, window.Size.Y);

            View view = new View(window.GetView());

            view.Center = new Vector2f (window.Size.X / 2, 0);

            window.SetView(view);

            timeAcum = 0;

            window.KeyPressed += OnPressBackKey;
            window.Closed += OnCloseWindow;
        }

        protected override void Update(float deltaTime)
        {
            timeAcum += deltaTime;

            View view = new View(window.GetView());

            view.Move(new Vector2f(0, 100 * deltaTime));

            window.SetView(view);

            if (timeAcum > 13f) OnBackPressed?.Invoke();
        }

        protected override void Draw()
        {
            window.Draw(creditsSprite);
        }

        protected override void Finish()
        {
            window.KeyPressed -= OnPressBackKey;
            window.Closed -= OnCloseWindow;
        }
    }
}
