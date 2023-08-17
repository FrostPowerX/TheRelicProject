using System;
using SFML.System;
using SFML.Graphics;
using Font = SFML.Graphics.Font;
using Color = SFML.Graphics.Color;

namespace The_Relic
{
    internal class Display
    {
        RenderWindow window;

        Texture texture;
        Font font;

        Sprite background;
        Text text;

        public Sprite Background { get => background; }

        public Display(RenderWindow window, Vector2f position, string buttonText)
        {
            this.window = window;

            texture = new Texture("Assets/Sprites/InfoPanel.png");
            font = new Font("Assets/Fonts/Olivia.ttf");

            background = new Sprite(texture);
            text = new Text(buttonText, font);

            FloatRect backgroundRect = background.GetLocalBounds();

            background.Origin = new Vector2f(backgroundRect.Width / 2f, backgroundRect.Height / 2f);

            SetText(buttonText);
            SetPosition(position);
        }

        public void SetText(string newText)
        {
            text.DisplayedString = newText;

            FloatRect textRect = text.GetLocalBounds();

            text.Origin = new Vector2f(textRect.Width / 2f, textRect.Height / 2f);
        }
        public void SetPosition(Vector2f position)
        {
            text.Position = position;
            background.Position = position;
        }
        public void SetScale(Vector2f scale)
        {
            text.Scale = scale;
            background.Scale = scale;
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

        public void Draw()
        {
            window.Draw(background);
            window.Draw(text);
        }
    }
}
