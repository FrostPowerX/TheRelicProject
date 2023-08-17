using SFML.System;
using SFML.Graphics;
using Color = SFML.Graphics.Color;
using View = SFML.Graphics.View;
using KeyEventArgs = SFML.Window.KeyEventArgs;

namespace The_Relic
{
    internal class OptionsMenuState : LoopState
    {
        private Texture backgroundTxture;
        private Sprite backgroundSprt;

        Button soundButton;
        Button controllsButton;
        Button backButton;

        public event Action OnSoundPressed;
        public event Action OnControllsPressed;
        public event Action OnBackPressed;

        float firstButtonOffset = 65f;
        float buttonSpacing = 160f / 2;

        public OptionsMenuState(RenderWindow window) : base(window) { }

        private void OnPressBackKey(object sender, KeyEventArgs args)
        {
            if (Input.GetAxis("Cancel") == 1) OnBackPressed?.Invoke();
        }
        private void OnPressSound() => OnSoundPressed?.Invoke();
        private void OnPressControlls() => OnControllsPressed?.Invoke();
        private void OnPressBack() => OnBackPressed?.Invoke();

        protected override void Start()
        {
            string buttonImageFilePath = "Assets/Sprites/Button.png";
            string buttonFontFilePath = "Assets/Fonts/Olivia.ttf";
            string backGroundFilePath = "Assets/Sprites/BackGround.png";

            backgroundTxture = new Texture(backGroundFilePath);
            backgroundSprt = new Sprite(backgroundTxture);

            FloatRect backgroundRect = backgroundSprt.GetLocalBounds();

            backgroundSprt.Origin = new Vector2f(backgroundRect.Width / 2f, backgroundRect.Height / 2);
            backgroundSprt.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);

            Vector2f soundButtonPosition = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset);
            Vector2f controllsButtonPosition = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset + buttonSpacing);
            Vector2f backButtonPosition = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset + buttonSpacing * 2);

            soundButton = new Button(window, buttonImageFilePath, buttonFontFilePath, soundButtonPosition, "Sounds");
            controllsButton = new Button(window, buttonImageFilePath, buttonFontFilePath, controllsButtonPosition, "Controlls");
            backButton = new Button(window, buttonImageFilePath, buttonFontFilePath, backButtonPosition, "Back");

            soundButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            controllsButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            backButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);

            soundButton.SetScale(new Vector2f(0.5f, 0.5f));
            controllsButton.SetScale(new Vector2f(0.5f, 0.5f));
            backButton.SetScale(new Vector2f(0.5f, 0.5f));

            View view = new View(window.GetView());

            view.Center = new Vector2f(window.Size.X / 2f, window.Size.Y / 2f);

            window.SetView(view);

            soundButton.OnPressed += OnPressSound;
            controllsButton.OnPressed += OnPressControlls;
            backButton.OnPressed += OnPressBack;
            window.KeyPressed += OnPressBackKey;
            window.Closed += OnCloseWindow;
        }

        protected override void Update(float deltaTime)
        {
            Cursor.Update(deltaTime, window);

            soundButton.Update();
            controllsButton.Update();
            backButton.Update();

            soundButton.SetPosition(new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset));
            controllsButton.SetPosition(new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset + buttonSpacing));
            backButton.SetPosition(new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset + buttonSpacing * 2));
        }

        protected override void Draw()
        {
            window.Draw(backgroundSprt);
            soundButton.Draw();
            controllsButton.Draw();
            backButton.Draw();

            Cursor.Draw(window);
        }

        protected override void Finish()
        {
            soundButton.Finish();
            controllsButton.Finish();
            backButton.Finish();

            soundButton.OnPressed -= OnPressSound;
            controllsButton.OnPressed -= OnPressControlls;
            backButton.OnPressed -= OnPressBack;
            window.KeyPressed -= OnPressBackKey;
            window.Closed -= OnCloseWindow;
        }
    }
}
