using SFML.System;
using SFML.Graphics;
using Color = SFML.Graphics.Color;
using View = SFML.Graphics.View;
using KeyEventArgs = SFML.Window.KeyEventArgs;

namespace The_Relic
{
    class InGameMenuState : LoopState
    {
        private Texture backgroundTxture;
        private Sprite backgroundSprt;

        private Button resumeButton;
        private Button optionsButton;
        private Button mainMenuButton;
        private Button quitButton;

        public event Action OnResumePressed;
        public event Action OnOptionsPressed;
        public event Action OnMainMenuPressed;
        public event Action OnQuitPressed;

        float firstButtonOffset = 65f;
        float buttonSpacing = 160f / 2;

        public Sprite BackGround { get => backgroundSprt; }

        public InGameMenuState(RenderWindow window) : base(window) { }

        private void OnPressKey(object sender, KeyEventArgs args)
        {
            if (Input.GetAxis("Cancel") == 1) OnResumePressed?.Invoke();
        }
            
        private void OnPressResume() => OnResumePressed?.Invoke();
        private void OnPressOptions() => OnOptionsPressed?.Invoke();
        private void OnPressMainMenu() => OnMainMenuPressed?.Invoke();
        private void OnPressQuit() => OnQuitPressed?.Invoke();

        protected override void Start()
        {
            string buttonImageFilePath = "Assets/Sprites/Button.png";
            string buttonFontFilePath = "Assets/Fonts/Olivia.ttf";

            Cursor.SetIntColor(Color.Cyan);
            Cursor.SetExColor(Color.Blue);
            Cursor.EnableCross(false);

            backgroundTxture = new Texture(window.Size.X, window.Size.Y);
            backgroundTxture.Update(window);

            if (backgroundSprt == null)
            {
                backgroundSprt = new Sprite(backgroundTxture);
                backgroundSprt.Color = new Color(100, 100, 100);

            }

            FloatRect backgroundRect = backgroundSprt.GetLocalBounds();

            backgroundSprt.Origin = new Vector2f(backgroundRect.Width / 2f, backgroundRect.Height / 2);
            backgroundSprt.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);

            Vector2f playButtonPosition = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset);
            Vector2f optionsButtonPosition = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset + buttonSpacing);
            Vector2f creditsButtonPosition = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset + buttonSpacing * 2);
            Vector2f quitButtonPosition = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset + buttonSpacing * 3);

            resumeButton = new Button(window, buttonImageFilePath, buttonFontFilePath, playButtonPosition, "Resume");
            optionsButton = new Button(window, buttonImageFilePath, buttonFontFilePath, optionsButtonPosition, "Options");
            mainMenuButton = new Button(window, buttonImageFilePath, buttonFontFilePath, creditsButtonPosition, "Main Menu");
            quitButton = new Button(window, buttonImageFilePath, buttonFontFilePath, quitButtonPosition, "Quit");

            resumeButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            optionsButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            mainMenuButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            quitButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);

            resumeButton.SetScale(new Vector2f(0.5f, 0.5f));
            optionsButton.SetScale(new Vector2f(0.5f, 0.5f));
            mainMenuButton.SetScale(new Vector2f(0.5f, 0.5f));
            quitButton.SetScale(new Vector2f(0.5f, 0.5f));

            View view = new View(window.GetView());

            view.Center = new Vector2f(window.Size.X / 2f, window.Size.Y / 2f);

            window.SetView(view);

            resumeButton.OnPressed += OnPressResume;
            optionsButton.OnPressed += OnPressOptions;
            mainMenuButton.OnPressed += OnPressMainMenu;
            quitButton.OnPressed += OnPressQuit;
            window.KeyPressed += OnPressKey;
            window.Closed += OnCloseWindow;
        }

        protected override void Update(float deltaTime)
        {
            resumeButton.Update();
            optionsButton.Update();
            mainMenuButton.Update();
            quitButton.Update();

            Cursor.Update(deltaTime, window);
        }

        protected override void Draw()
        {
            if (backgroundSprt != null)
                window.Draw(backgroundSprt);
            resumeButton.Draw();
            optionsButton.Draw();
            mainMenuButton.Draw();
            quitButton.Draw();

            Cursor.Draw(window);
        }

        public void ResetBackGround()
        {
            backgroundSprt = null;
        }

        protected override void Finish()
        {
            resumeButton.Finish();
            optionsButton.Finish();
            mainMenuButton.Finish();
            quitButton.Finish();

            resumeButton.OnPressed -= OnPressResume;
            optionsButton.OnPressed -= OnPressOptions;
            mainMenuButton.OnPressed -= OnPressMainMenu;
            quitButton.OnPressed -= OnPressQuit;
            window.KeyPressed -= OnPressKey;
            window.Closed -= OnCloseWindow;
        }
    }
}
