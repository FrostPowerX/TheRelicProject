using SFML.System;
using SFML.Graphics;
using Font = SFML.Graphics.Font;
using Color = SFML.Graphics.Color;
using View = SFML.Graphics.View;

namespace The_Relic
{
    class HUD
    {
        private readonly RenderWindow window;
        private readonly Player player;
        private Font font;
        private Text livesText;
        private Text scoreText;

        private const string HealthLabel = "Health: ";
        private const string KillsLabel = "Enemys Killed: ";
        private const float MarginSize = 25f;


        public HUD (RenderWindow window, Player player, string fontFilePath)
        {
            this.window = window;
            this.player = player;
            
            font = new Font(fontFilePath);

            livesText = new Text(HealthLabel + player.Data.health.ToString(), font);
            scoreText = new Text(KillsLabel + player.Data.enemysKilled.ToString(), font);

            livesText.FillColor = Color.White;
            livesText.OutlineColor = Color.Red;
            livesText.OutlineThickness = 3f;
            
            scoreText.FillColor = Color.White;
            scoreText.OutlineColor = Color.Blue;
            scoreText.OutlineThickness = 3f;
        }

        public void Update ()
        {
            View view = window.GetView();
            float scoreWidth = scoreText.GetGlobalBounds().Width;
            Vector2f center = view.Center;
            Vector2f windowHalfSize = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
            Vector2f livesOffset = new Vector2f(-windowHalfSize.X + MarginSize, -windowHalfSize.Y + MarginSize);
            Vector2f scoreOffset = new Vector2f(windowHalfSize.X - MarginSize - scoreWidth, -windowHalfSize.Y + MarginSize);

            livesText.Position = center + livesOffset;
            scoreText.Position = center + scoreOffset;

            livesText.DisplayedString = $"{HealthLabel}{player.Data.health: #,#.#} / {player.Data.maxHealth}";
            scoreText.DisplayedString = KillsLabel + player.Data.enemysKilled.ToString();
        }
        public void Draw ()
        {
            window.Draw(livesText);
            window.Draw(scoreText);
        }
    }
}
