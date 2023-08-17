using SFML.System;
using SFML.Graphics;
using Color = SFML.Graphics.Color;
using Font = SFML.Graphics.Font;
using View = SFML.Graphics.View;
using KeyEventArgs = SFML.Window.KeyEventArgs;

namespace The_Relic
{
    class MainMenuState : LoopState
    {
        private Texture backgroundTxture;
        private Sprite backgroundSprt;

        private Font titleFont;
        private Text titleText;

        private Button playButton;
        private Button optionsButton;
        private Button creditsButton;
        private Button quitButton;

        public event Action OnPlayPressed;
        public event Action OnOptionsPressed;
        public event Action OnCreditsPressed;
        public event Action OnQuitPressed;

        float firstButtonOffset = 65f;
        float buttonSpacing = 160f / 2;

        public MainMenuState(RenderWindow window) : base(window) { }

        private void OnPressKey(object sender, KeyEventArgs args)
        {
            if (Input.GetAxis("Cancel") == 1) OnQuitPressed?.Invoke();
        }
            
        private void OnPressPlay() => OnPlayPressed?.Invoke();
        private void OnPressOptions() => OnOptionsPressed?.Invoke();
        private void OnPressCredits() => OnCreditsPressed?.Invoke();
        private void OnPressQuit() => OnQuitPressed?.Invoke();

        protected override void Start()
        {
            string fontFilePath = "Assets/Fonts/Anguishop.ttf";
            string backGroundFilePath = "Assets/Sprites/BackGround.png";
            string buttonImageFilePath = "Assets/Sprites/Button.png";
            string buttonFontFilePath = "Assets/Fonts/Olivia.ttf";

            Cursor.SetIntColor(Color.Cyan);
            Cursor.SetExColor(Color.Blue);
            Cursor.EnableCross(false);

            backgroundTxture = new Texture(backGroundFilePath);
            backgroundSprt = new Sprite(backgroundTxture);

            titleFont = new Font(fontFilePath);
            titleText = new Text("The Relic", titleFont);

            titleText.CharacterSize = 100;
            titleText.FillColor = Color.Blue;
            titleText.OutlineColor = Color.White;
            titleText.OutlineThickness = 2f;

            FloatRect titleRect = titleText.GetLocalBounds();
            FloatRect backgroundRect = backgroundSprt.GetLocalBounds();

            titleText.Origin = new Vector2f(titleRect.Width / 2f, titleRect.Height / 2f);
            titleText.Position = new Vector2f(window.Size.X / 2f, window.Size.Y / 6f);

            backgroundSprt.Origin = new Vector2f(backgroundRect.Width / 2f, backgroundRect.Height / 2);
            backgroundSprt.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);

            Vector2f playButtonPosition = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset);
            Vector2f optionsButtonPosition = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset + buttonSpacing);
            Vector2f creditsButtonPosition = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset + buttonSpacing * 2);
            Vector2f quitButtonPosition = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset + buttonSpacing * 3);

            playButton = new Button(window, buttonImageFilePath, buttonFontFilePath, playButtonPosition, "Play");
            optionsButton = new Button(window, buttonImageFilePath, buttonFontFilePath, optionsButtonPosition, "Options");
            creditsButton = new Button(window, buttonImageFilePath, buttonFontFilePath, creditsButtonPosition, "Credits");
            quitButton = new Button(window, buttonImageFilePath, buttonFontFilePath, quitButtonPosition, "Quit");

            playButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            optionsButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            creditsButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            quitButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);

            playButton.SetScale(new Vector2f(0.5f, 0.5f));
            optionsButton.SetScale(new Vector2f(0.5f, 0.5f));
            creditsButton.SetScale(new Vector2f(0.5f, 0.5f));
            quitButton.SetScale(new Vector2f(0.5f, 0.5f));

            View view = new View(window.GetView());

            view.Center = new Vector2f(window.Size.X / 2f, window.Size.Y / 2f);

            window.SetView(view);

            playButton.OnPressed += OnPressPlay;
            optionsButton.OnPressed += OnPressOptions;
            creditsButton.OnPressed += OnPressCredits;
            quitButton.OnPressed += OnPressQuit;
            window.KeyPressed += OnPressKey;
            window.Closed += OnCloseWindow;
        }

        protected override void Update(float deltaTime)
        {
            Cursor.Update(deltaTime, window);

            playButton.Update();
            optionsButton.Update();
            creditsButton.Update();
            quitButton.Update();
        }

        protected override void Draw()
        {
            window.Draw(backgroundSprt);
            window.Draw(titleText);
            playButton.Draw();
            optionsButton.Draw();
            creditsButton.Draw();
            quitButton.Draw();

            DrawManager.Draw(window);

            Cursor.Draw(window);
        }

        protected override void Finish()
        {
            DrawManager.Clear();
            CollisionsHandler.Clear();

            playButton.Finish();
            optionsButton.Finish();
            creditsButton.Finish();
            quitButton.Finish();

            playButton.OnPressed -= OnPressPlay;
            optionsButton.OnPressed -= OnPressOptions;
            creditsButton.OnPressed -= OnPressCredits;
            quitButton.OnPressed -= OnPressQuit;
            window.KeyPressed -= OnPressKey;
            window.Closed -= OnCloseWindow;
        }
    }
}
