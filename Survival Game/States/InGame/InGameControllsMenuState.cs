using SFML.System;
using SFML.Window;
using SFML.Graphics;
using Color = SFML.Graphics.Color;
using View = SFML.Graphics.View;
using KeyEventArgs = SFML.Window.KeyEventArgs;
using Font = SFML.Graphics.Font;

namespace The_Relic
{
    internal class InGameControllsMenuState : LoopState
    {
        private Texture backgroundTxture;
        private Sprite backgroundSprt;

        Button upButton;
        Button downButton;
        Button leftButton;
        Button rightButton;
        Button cancelButton;
        Button fire1Button;
        Button backButton;

        Font font;
        Text upTxt;
        Text downTxt;
        Text leftTxt;
        Text rightTxt;
        Text cancelTxt;
        Text fire1Txt;

        float firstButtonOffsetY = 64f;
        float firstButtonOffsetX = 64f;
        float firstListOffset;

        int buttonPress = 0;

        public event Action OnBackPressed;

        public InGameControllsMenuState(RenderWindow window) : base(window) { }

        private void OnPressBack() => OnBackPressed?.Invoke();
        private void OnPressBackKey(object sender, KeyEventArgs args)
        {
            if (Input.GetAxis("Cancel") == 1) OnBackPressed?.Invoke();
        }
        private void GetKeyPressed(object sender, KeyEventArgs args)
        {
            switch(buttonPress)
            {
                case 1:
                    Input.SetNegKey("Vertical", args.Code);
                    upButton.SetText($"{args.Code}");
                    break;

                case 2:
                    Input.SetPosKey("Vertical", args.Code);
                    downButton.SetText($"{args.Code}");
                    break;

                case 3:
                    Input.SetNegKey("Horizontal", args.Code);
                    leftButton.SetText($"{args.Code}");
                    break;

                case 4:
                    Input.SetPosKey("Horizontal", args.Code);
                    rightButton.SetText($"{args.Code}");
                    break;

                case 5:
                    Input.SetPosKey("Cancel", args.Code);
                    cancelButton.SetText($"{args.Code}");
                    break;

                case 6:
                    Input.SetPosKey("Fire1", args.Code);
                    fire1Button.SetText($"{args.Code}");

                    window.MouseButtonPressed -= GetMouseButtonPressed;
                    break;
            }

            window.KeyPressed += OnPressBackKey;
            window.KeyPressed -= GetKeyPressed;
            Resume();
        }
        private void GetMouseButtonPressed(object sender, MouseButtonEventArgs args)
        {
            if (buttonPress == 6)
            {
                Input.SetPosButton("Fire1", args.Button);
                fire1Button.SetText($"{args.Button}");

                window.KeyPressed += OnPressBackKey;
                window.KeyPressed -= GetKeyPressed;
                window.MouseButtonPressed -= GetMouseButtonPressed;
                Resume();
            }
            
        }

        private void OnPressButtonUp()
        {
            Pause();
            buttonPress = 1;
            window.KeyPressed -= OnPressBackKey;
            window.KeyPressed += GetKeyPressed;
        }
        private void OnPressButtonDown()
        {
            Pause();
            buttonPress = 2;
            window.KeyPressed -= OnPressBackKey;
            window.KeyPressed += GetKeyPressed;

        }
        private void OnPressButtonLeft()
        {
            Pause();
            buttonPress = 3;
            window.KeyPressed -= OnPressBackKey;
            window.KeyPressed += GetKeyPressed;
        }
        private void OnPressButtonRight()
        {
            Pause();
            buttonPress = 4;
            window.KeyPressed -= OnPressBackKey;
            window.KeyPressed += GetKeyPressed;
        }
        private void OnPressButtonCancel()
        {
            Pause();
            buttonPress = 5;
            window.KeyPressed -= OnPressBackKey;
            window.KeyPressed += GetKeyPressed;
        }
        private void OnPressButtonFire1()
        {
            Pause();
            buttonPress = 6;
            Mouse.SetPosition(new Vector2i(), window);
            window.KeyPressed -= OnPressBackKey;
            window.KeyPressed += GetKeyPressed;
            window.MouseButtonPressed += GetMouseButtonPressed;
        }

        protected override void Start()
        {
            string buttonImageFilePath = "Assets/Sprites/Button.png";
            string buttonFontFilePath = "Assets/Fonts/Olivia.ttf";
            string buttonKeyImgFilePath = "Assets/Sprites/ButtonKey.png";
            string backGroundFilePath = "Assets/Sprites/BackGround.png";

            font = new Font("Assets/Fonts/Anguishop.ttf");

            firstListOffset = window.Size.X / 4;

            if (backgroundTxture == null && backgroundSprt == null)
            {
                backgroundTxture = new Texture(backGroundFilePath);
                backgroundSprt = new Sprite(backgroundTxture);
            }

            FloatRect backgroundRect = backgroundSprt.GetLocalBounds();

            backgroundSprt.Origin = new Vector2f(backgroundRect.Width / 2f, backgroundRect.Height / 2);
            backgroundSprt.Position = new Vector2f(window.Size.X / 2, window.Size.Y / 2);

            upTxt = new Text("Up: ", font);
            downTxt = new Text("Down: ", font);
            leftTxt = new Text("Left: ", font);
            rightTxt = new Text("Right: ", font);
            cancelTxt = new Text("Back / Cancel: ", font);
            fire1Txt = new Text("Shoot: ", font);

            FloatRect upTxtSize = upTxt.GetGlobalBounds();
            FloatRect downTxtSize = downTxt.GetGlobalBounds();
            FloatRect leftTxtSize = leftTxt.GetGlobalBounds();
            FloatRect rightTxtSize = rightTxt.GetGlobalBounds();
            FloatRect cancelTxtSize = cancelTxt.GetGlobalBounds();
            FloatRect fire1TxtSize = fire1Txt.GetGlobalBounds();

            Vector2f upTextPos = new Vector2f(firstListOffset - firstButtonOffsetX - upTxtSize.Width, window.Size.Y / 6f);
            Vector2f downTextPos = new Vector2f(firstListOffset - firstButtonOffsetX - downTxtSize.Width, window.Size.Y / 6f + firstButtonOffsetY);
            Vector2f leftTextPos = new Vector2f(firstListOffset - firstButtonOffsetX - leftTxtSize.Width, window.Size.Y / 6f + firstButtonOffsetY * 2);
            Vector2f rightTextPos = new Vector2f(firstListOffset - firstButtonOffsetX - rightTxtSize.Width, window.Size.Y / 6f + firstButtonOffsetY * 3);
            Vector2f cancelTextPos = new Vector2f(firstListOffset * 2 - firstButtonOffsetX - upTxtSize.Width, window.Size.Y / 6f);
            Vector2f fire1TextPos = new Vector2f(firstListOffset * 2 - firstButtonOffsetX - fire1TxtSize.Width, window.Size.Y / 6f + firstButtonOffsetY);

            upTxt.Origin = new Vector2f(upTxtSize.Width / 2, upTxtSize.Height / 2);
            upTxt.Position = upTextPos;
            downTxt.Origin = new Vector2f(downTxtSize.Width / 2, downTxtSize.Height / 2);
            downTxt.Position = downTextPos;
            leftTxt.Origin = new Vector2f(leftTxtSize.Width / 2, leftTxtSize.Height / 2);
            leftTxt.Position = leftTextPos;
            rightTxt.Origin = new Vector2f(rightTxtSize.Width / 2, rightTxtSize.Height / 2);
            rightTxt.Position = rightTextPos;
            cancelTxt.Origin = new Vector2f(cancelTxtSize.Width / 2, cancelTxtSize.Height / 2);
            cancelTxt.Position = cancelTextPos;
            fire1Txt.Origin = new Vector2f(fire1TxtSize.Width / 2, fire1TxtSize.Height / 2);
            fire1Txt.Position = fire1TextPos;

            Vector2f upButtonPos = new Vector2f(firstListOffset, window.Size.Y / 6f);
            Vector2f downButtonPos = new Vector2f(firstListOffset, window.Size.Y / 6f + firstButtonOffsetY);
            Vector2f leftButtonPos = new Vector2f(firstListOffset, window.Size.Y / 6f + firstButtonOffsetY * 2);
            Vector2f rightButtonPos = new Vector2f(firstListOffset, window.Size.Y / 6f + firstButtonOffsetY * 3);
            Vector2f cancelButtonPos = new Vector2f(firstListOffset * 2, window.Size.Y / 6f);
            Vector2f fire1ButtonPos = new Vector2f(firstListOffset * 2, window.Size.Y / 6f + firstButtonOffsetY);
            Vector2f backButtonPosition = new Vector2f(window.Size.X / 10, window.Size.Y / 10);

            upButton = new Button(window, buttonKeyImgFilePath, buttonFontFilePath, upButtonPos, $"{Input.GetNegKey("Vertical")}");
            downButton = new Button(window, buttonKeyImgFilePath, buttonFontFilePath, downButtonPos, $"{Input.GetPosKey("Vertical")}");
            leftButton = new Button(window, buttonKeyImgFilePath, buttonFontFilePath, leftButtonPos, $"{Input.GetNegKey("Horizontal")}");
            rightButton = new Button(window, buttonKeyImgFilePath, buttonFontFilePath, rightButtonPos, $"{Input.GetPosKey("Horizontal")}");
            cancelButton = new Button(window, buttonKeyImgFilePath, buttonFontFilePath, cancelButtonPos, $"{Input.GetPosKey("Cancel")}");
            fire1Button = new Button(window, buttonKeyImgFilePath, buttonFontFilePath, fire1ButtonPos, $"{Input.GetPosKey("Fire1")}");
            backButton = new Button(window, buttonImageFilePath, buttonFontFilePath, backButtonPosition, "Back");

            upButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            downButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            leftButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            rightButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            cancelButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            fire1Button.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);
            backButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);

            backButton.SetScale(new Vector2f(0.5f, 0.5f));

            View view = new View(window.GetView());

            view.Center = new Vector2f(window.Size.X / 2f, window.Size.Y / 2f);

            window.SetView(view);

            upButton.OnPressed += OnPressButtonUp;
            downButton.OnPressed += OnPressButtonDown;
            leftButton.OnPressed += OnPressButtonLeft;
            rightButton.OnPressed += OnPressButtonRight;
            cancelButton.OnPressed += OnPressButtonCancel;
            fire1Button.OnPressed += OnPressButtonFire1;
            backButton.OnPressed += OnPressBack;
            window.KeyPressed += OnPressBackKey;
            window.Closed += OnCloseWindow;
        }

        protected override void Update(float deltaTime)
        {
            Cursor.Update(deltaTime, window);
            upButton.Update();
            downButton.Update();
            backButton.Update();
            leftButton.Update();
            rightButton.Update();
            cancelButton.Update();
            fire1Button.Update();
        }

        protected override void Draw()
        {
            window.Draw(backgroundSprt);
            upButton.Draw();
            downButton.Draw();
            leftButton.Draw();
            rightButton.Draw();
            cancelButton.Draw();
            fire1Button.Draw();
            backButton.Draw();
            window.Draw(upTxt);
            window.Draw(downTxt);
            window.Draw(leftTxt);
            window.Draw(rightTxt);
            window.Draw(cancelTxt);
            window.Draw(fire1Txt);

            Cursor.Draw(window);
        }

        public void SetBackGround(Sprite backGround)
        {
            backgroundSprt = backGround;
        }

        protected override void Finish()
        {
            upButton.Finish();
            downButton.Finish();
            backButton.Finish();

            upButton.OnPressed -= OnPressButtonUp;
            downButton.OnPressed -= OnPressButtonDown;
            leftButton.OnPressed -= OnPressButtonLeft;
            rightButton.OnPressed -= OnPressButtonRight;
            cancelButton.OnPressed -= OnPressButtonCancel;
            fire1Button.OnPressed -= OnPressButtonFire1;
            backButton.OnPressed -= OnPressBack;
            window.KeyPressed -= OnPressBackKey;
            window.Closed -= OnCloseWindow;
        }
    }
}
