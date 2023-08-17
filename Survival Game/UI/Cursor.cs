using SFML.System;
using SFML.Graphics;
using SFML.Window;
using Color = SFML.Graphics.Color;
using View = SFML.Graphics.View;

namespace The_Relic
{
    static class Cursor
    {
        static private Texture texture;
        static private Sprite spriteInterior;
        static private Sprite spriteExterior;

        static private Texture textureCross;
        static private Sprite crossHire;

        static bool reinbow = false;
        static bool cross = false;

        static public void Init()
        {
            texture = new Texture("Assets/Sprites/Cursor.png");
            textureCross = new Texture("Assets/Sprites/CrossHire.png");

            spriteInterior = new Sprite(texture);
            spriteExterior = new Sprite(texture);
            crossHire = new Sprite(textureCross);

            crossHire.Origin = new Vector2f(crossHire.Texture.Size.X / 2, crossHire.Texture.Size.Y / 2);

            spriteExterior.TextureRect = new IntRect
            {
                Top = 0,
                Left = 0,
                Width = 22,
                Height = 29
            };

            spriteInterior.TextureRect = new IntRect
            {
                Top = 0,
                Left = 22,
                Width = 22,
                Height = 29
            };
        }

        static private void Rainbow()
        {
            Random random = new Random();

            byte colorR1 = (byte)random.Next(0, 255);
            byte colorG1 = (byte)random.Next(0, 255);
            byte colorB1 = (byte)random.Next(0, 255);

            SetCrossColor(new Color(colorR1, colorG1, colorB1));

            SetIntColor(new Color(colorR1, colorG1, colorB1));

            byte colorR2 = (byte)random.Next(0, 255);
            byte colorG2 = (byte)random.Next(0, 255);
            byte colorB2 = (byte)random.Next(0, 255);

            SetExColor(new Color(colorR2, colorG2, colorB2));
        }

        static public void SetIntColor(Color color) => spriteInterior.Color = color;
        static public void SetExColor(Color color) => spriteExterior.Color = color;
        static public void SetCrossColor(Color color) => crossHire.Color = color;
        static public void Rainbow(bool choise) => reinbow = choise;
        static public void EnableCross(bool choise) => cross = choise;

        static public void Update(float deltaTime, RenderWindow window)
        {
            View view = window.GetView();
            Vector2f center = view.Center;
            Vector2f windowHalfSize = new Vector2f(view.Size.X / 2, view.Size.Y / 2);
            Vector2f scoreOffset = new Vector2f(-windowHalfSize.X + Mouse.GetPosition(window).X, -windowHalfSize.Y + Mouse.GetPosition(window).Y);

            if (cross)
            {
                crossHire.Position = center + scoreOffset;
            }

            spriteInterior.Position = center + scoreOffset;
            spriteExterior.Position = center + scoreOffset;

            if (reinbow) Rainbow();
        }
        static public void Draw(RenderWindow window)
        {
            if (cross)
            {
                window.Draw(crossHire);
                return;
            }

            window.Draw(spriteInterior);
            window.Draw(spriteExterior);
        }
    }
}
