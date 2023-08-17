using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using Color = SFML.Graphics.Color;
using Font = SFML.Graphics.Font;

namespace The_Relic
{
    class Button
    {
        private RenderWindow window;

        private Texture texture;
        private Font font;

        private Sprite background;
        private Text text;

        public event Action OnPressed;


        public Button(RenderWindow window, string textureFilePath, string fontFilePath, Vector2f position, string buttonText)
        {
            this.window = window;

            texture = new Texture(textureFilePath);
            font = new Font(fontFilePath);

            background = new Sprite(texture);
            text = new Text(buttonText, font);

            FloatRect backgroundRect = background.GetLocalBounds();
            FloatRect textRect = text.GetLocalBounds();

            text.Origin = new Vector2f(textRect.Width / 2f, textRect.Height / 2f);

            background.Origin = new Vector2f(backgroundRect.Width / 2f, backgroundRect.Height / 2f);

            SetText(buttonText);
            SetPosition(position);

            window.MouseButtonReleased += OnReleaseMouseButton;
        }

        private void OnReleaseMouseButton(object sender, MouseButtonEventArgs eventArgs)
        {
            FloatRect bounds = background.GetGlobalBounds();

            if (bounds.Contains(eventArgs.X, eventArgs.Y))
            {
                AudioManager.ButtonSound.Play();
                OnPressed?.Invoke();
            }
        }

        private void AboveTheButton()
        {
            FloatRect bounds = background.GetGlobalBounds();

            if (bounds.Contains(Mouse.GetPosition(window).X, Mouse.GetPosition(window).Y))
            {
                background.Color = new Color(150, 150, 150);
                return;
            }

            background.Color = new Color(255, 255, 255);
        }

        public void SetText(string newText)
        {
            text.DisplayedString = newText;

            FloatRect textRect = text.GetLocalBounds();

            text.Origin = new Vector2f(textRect.Width / 2f, textRect.Height / 2f);
        }
        public void SetColor(Color color)
        {
            background.Color = color;
        }
        public void SetScale(Vector2f scale)
        {
            text.Scale = scale;
            background.Scale = scale;
        }
        public void SetPosition(Vector2f position)
        {
            background.Position = position;
            text.Position = background.Position;
        }

        public void FormatText(Color fillColor, Color outlineColor, uint size, bool outline, float outlineThickness)
        {
            text.FillColor = fillColor;
            text.CharacterSize = size;

            if (outline)
            {
                text.OutlineColor = outlineColor;
                text.OutlineThickness = outlineThickness;
            }
            else
            {
                text.OutlineColor = Color.Transparent;
                text.OutlineThickness = 0f;
            }
        }

        public void Update()
        {
            AboveTheButton();
        }
        public void Draw()
        {
            window.Draw(background);
            window.Draw(text);
        }

        public void Finish()
        {
            window.MouseButtonReleased -= OnReleaseMouseButton;
        }
    }
}
