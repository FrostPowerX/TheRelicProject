using SFML.System;
using SFML.Graphics;
using Color = SFML.Graphics.Color;
using View = SFML.Graphics.View;
using KeyEventArgs = SFML.Window.KeyEventArgs;

namespace The_Relic
{
    internal class SoundMenuState : LoopState
    {
        private Texture backgroundTxture;
        private Sprite backgroundSprt;

        Button upMusicButton;
        Button downMusicButton;
        Button upGFXButton;
        Button downGFXButton;
        Button backButton;

        Display musicVolDysplay;
        Display gfxVolDysplay;

        Vector2f upMusicButtonPos;
        Vector2f downMusicButtonPos;

        Vector2f upGFXButtonPos;
        Vector2f downGFXButtonPos;


        float firstButtonOffset = 64f;
        float buttonSeparation = 32f;
        float buttonSpacing = 160f / 2;
        float musicDyspHalfSizeX;
        float gfxDyspHalfSizeX;

        public event Action OnBackPressed;

        public SoundMenuState(RenderWindow window) : base(window) { }

        private void OnPressBackKey(object sender, KeyEventArgs args)
        {
            if (Input.GetAxis("Cancel") == 1) OnBackPressed?.Invoke();
        }
        private void OnUpPressMusicButton() => AudioManager.AddMusicVol(10f);
        private void OnDownPressMusicButton() => AudioManager.AddMusicVol(-10f);
        private void OnUpPressGFXButton() => AudioManager.AddGFXVol(10f);
        private void OnDownPressGFXButton() => AudioManager.AddGFXVol(-10f);
        private void OnPressBack() => OnBackPressed?.Invoke();

        protected override void Start()
        {
            musicVolDysplay = new Display(window, new Vector2f(), AudioManager.MusicVol.ToString());
            gfxVolDysplay = new Display(window, new Vector2f(), AudioManager.GFXVol.ToString());

            musicVolDysplay.SetScale(new Vector2f(0.6f, 0.5f));
            gfxVolDysplay.SetScale(new Vector2f(0.6f, 0.5f));

            musicDyspHalfSizeX = musicVolDysplay.Background.GetGlobalBounds().Width / 2;
            gfxDyspHalfSizeX = gfxVolDysplay.Background.GetGlobalBounds().Width / 2;

            string upButtonsFilePath = "Assets/Sprites/ButtonPositive.png";
            string downButtonsFilePath = "Assets/Sprites/ButtonNegative.png";

            string buttonImageFilePath = "Assets/Sprites/Button.png";
            string buttonFontFilePath = "Assets/Fonts/Olivia.ttf";

            string backGroundFilePath = "Assets/Sprites/BackGround.png";

            backgroundTxture = new Texture(backGroundFilePath);
            backgroundSprt = new Sprite(backgroundTxture);

            FloatRect backgroundRect = backgroundSprt.GetLocalBounds();

            backgroundSprt.Origin = new Vector2f(backgroundRect.Width / 2f, backgroundRect.Height / 2);
            backgroundSprt.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);

            Vector2f musicDyspPos = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f);
            Vector2f gfxDyspPos = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset);

            musicVolDysplay.SetPosition(musicDyspPos);
            gfxVolDysplay.SetPosition(gfxDyspPos);

            downMusicButtonPos = new Vector2f(window.Size.X / 2f -musicDyspHalfSizeX - buttonSeparation, window.Size.Y / 4f);
            upMusicButtonPos = new Vector2f(window.Size.X / 2f + musicDyspHalfSizeX + buttonSeparation, window.Size.Y / 4f);

            downGFXButtonPos = new Vector2f(window.Size.X / 2f -gfxDyspHalfSizeX - buttonSeparation, window.Size.Y / 4f + firstButtonOffset);
            upGFXButtonPos = new Vector2f(window.Size.X / 2f + gfxDyspHalfSizeX + buttonSeparation, window.Size.Y / 4f + firstButtonOffset);

            Vector2f backButtonPosition = new Vector2f(window.Size.X / 2f, window.Size.Y / 4f + firstButtonOffset + buttonSpacing * 2);

            upMusicButton = new Button(window, upButtonsFilePath, buttonFontFilePath, upMusicButtonPos, "");
            downMusicButton = new Button(window, downButtonsFilePath, buttonFontFilePath, downMusicButtonPos, "");
            upGFXButton = new Button(window, upButtonsFilePath, buttonFontFilePath, upGFXButtonPos, "");
            downGFXButton = new Button(window, downButtonsFilePath, buttonFontFilePath, downGFXButtonPos, "");
            backButton = new Button(window, buttonImageFilePath, buttonFontFilePath, backButtonPosition, "Back");

            musicVolDysplay.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            gfxVolDysplay.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            backButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);

            backButton.SetScale(new Vector2f(0.5f, 0.5f));

            View view = new View(window.GetView());

            view.Center = new Vector2f(window.Size.X / 2f, window.Size.Y / 2f);

            window.SetView(view);

            upMusicButton.OnPressed += OnUpPressMusicButton;
            downMusicButton.OnPressed += OnDownPressMusicButton;
            upGFXButton.OnPressed += OnUpPressGFXButton;
            downGFXButton.OnPressed += OnDownPressGFXButton;

            backButton.OnPressed += OnPressBack;
            window.KeyPressed += OnPressBackKey;
            window.Closed += OnCloseWindow;
        }

        protected override void Update(float deltaTime)
        {
            Cursor.Update(deltaTime, window);

            musicVolDysplay.SetText("Music Vol:   " + AudioManager.MusicVol.ToString() + "%");
            gfxVolDysplay.SetText("GFX Vol:   " + AudioManager.GFXVol.ToString() + "%");

            upMusicButton.Update();
            downMusicButton.Update();
            upGFXButton.Update();
            downGFXButton.Update();
            backButton.Update();
        }

        protected override void Draw()
        {
            window.Draw(backgroundSprt);
            upMusicButton.Draw();
            downMusicButton.Draw();
            upGFXButton.Draw();
            downGFXButton.Draw();

            musicVolDysplay.Draw();
            gfxVolDysplay.Draw();

            backButton.Draw();

            Cursor.Draw(window);
        }

        protected override void Finish()
        {
            upMusicButton.Finish();
            downMusicButton.Finish();
            upGFXButton.Finish();
            downGFXButton.Finish();

            backButton.Finish();

            upMusicButton.OnPressed -= OnUpPressMusicButton;
            downMusicButton.OnPressed -= OnDownPressMusicButton;
            upGFXButton.OnPressed -= OnUpPressGFXButton;
            downGFXButton.OnPressed -= OnDownPressGFXButton;

            backButton.OnPressed -= OnPressBack;
            window.KeyPressed -= OnPressBackKey;
            window.Closed -= OnCloseWindow;
        }
    }
}
