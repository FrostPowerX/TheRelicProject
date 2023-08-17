using SFML.System;
using SFML.Graphics;
using Font = SFML.Graphics.Font;
using KeyEventArgs = SFML.Window.KeyEventArgs;
using Color = SFML.Graphics.Color;
using View = SFML.Graphics.View;

namespace The_Relic
{
    internal class InfoState : LoopState
    {
        Texture texture;
        Sprite PowerUpsSprite;

        Button okButton;

        public event Action OnOkPressed;

        public InfoState(RenderWindow window) : base(window) { }

        protected override void Start()
        {
            string powerUpsFilePath = "Assets/Sprites/PowerUpsInfo.png";
            string buttonImageFilePath = "Assets/Sprites/Button.png";
            string buttonFontFilePath = "Assets/Fonts/Olivia.ttf";

            Vector2f okPos = new Vector2f(window.Size.X / 1.2f, window.Size.Y / 1.2f);

            texture = new Texture(powerUpsFilePath);
            PowerUpsSprite = new Sprite(texture);

            okButton = new Button(window, buttonImageFilePath, buttonFontFilePath, okPos, "Ok");

            okButton.SetScale(new Vector2f(0.5f, 0.5f));

            Vector2f halfPowerUps = new Vector2f(PowerUpsSprite.GetGlobalBounds().Width / 2f, PowerUpsSprite.GetGlobalBounds().Height / 2f);

            PowerUpsSprite.Origin = halfPowerUps;
            PowerUpsSprite.Position = new Vector2f(window.Size.X / 2f, window.Size.Y / 2f);

            okButton.FormatText(fillColor: Color.Cyan, outlineColor: Color.Black, size: 30, outline: true, outlineThickness: 2f);

            View view = new View(window.GetView());

            view.Center = new Vector2f (window.Size.X / 2, window.Size.Y / 2);

            window.SetView(view);

            okButton.OnPressed += OnOkPressed;
            window.Closed += OnCloseWindow;
        }

        protected override void Update(float deltaTime)
        {
            Cursor.Update(deltaTime, window);

            okButton.Update();
        }

        protected override void Draw()
        {
            window.Draw(PowerUpsSprite);
            okButton.Draw();

            Cursor.Draw(window);
        }

        protected override void Finish()
        {
            okButton.OnPressed -= OnOkPressed;
            window.Closed -= OnCloseWindow;
        }
    }
}
