using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using Color = SFML.Graphics.Color;

namespace The_Relic
{
    internal class SlidingBar
    {
        Texture healthBar;
        Texture fillingTexture;

        Sprite backBar;
        Sprite fillingBar;
        Sprite frontBar;

        int filling;

        public SlidingBar()
        {
            healthBar = new Texture("Assets/Sprites/HealthBar.png");
            fillingTexture = new Texture("Assets/Sprites/HealthBarFilling.png");

            backBar = new Sprite(healthBar);
            frontBar = new Sprite(healthBar);
            fillingBar = new Sprite(fillingTexture);

            backBar.TextureRect = new IntRect 
            {
                Top = 0,
                Left = 0,
                Width = 64,
                Height = 10
            };
            frontBar.TextureRect = new IntRect
            {
                Top = 0,
                Left = 64,
                Width = 64,
                Height = 10
            };

            FloatRect backbarHalf = backBar.GetGlobalBounds();
            FloatRect frontbarHalf = frontBar.GetGlobalBounds();
            FloatRect fillingbarHalf = frontBar.GetGlobalBounds();

            backBar.Origin = new Vector2f(backbarHalf.Width / 2, backbarHalf.Height / 2);
            frontBar.Origin = new Vector2f(frontbarHalf.Width / 2, frontbarHalf.Height / 2);

            fillingBar.Origin = new Vector2f(fillingbarHalf.Width / 2, fillingbarHalf.Height / 2);

            fillingBar.Texture.Repeated = true;

            filling = (int)backBar.Texture.Size.X / 2;
        }

        public void SetFillingColor(Color color) => fillingBar.Color = color;
        public void SetPosition(Vector2f position)
        {
            backBar.Position = position;
            fillingBar.Position = position;
            frontBar.Position = position;
        }
        public void SetPercent(float value)
        {
            int Height = fillingBar.TextureRect.Height;

            value = Math.Clamp(value, 0, 1);

            int percentOfHealth = (int)(value * filling);

            fillingBar.TextureRect = new IntRect(0,0, percentOfHealth, Height);
        }
        public void SetScale(Vector2f scale)
        {
            backBar.Scale = scale;
            fillingBar.Scale = scale;
            frontBar.Scale = scale;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(backBar);
            window.Draw(fillingBar);
            window.Draw(frontBar);
        }
    }
}
