using SFML.System;
using SFML.Graphics;


namespace The_Relic
{
    internal class Camera
    {
        readonly RenderWindow window;
        SFML.Graphics.View view;
        Entity target;

        public Camera(RenderWindow window, Entity target)
        {
            this.window = window;
            view = new SFML.Graphics.View(window.GetView());
            this.target = target;
        }

        public void Update(float deltaTime)
        {
            float camPosX = view.Size.X / 2;
            float camPosY = view.Size.Y / 2;

            float tPosX = target.Position.X;
            float tPosY = target.Position.Y;

            float newPosX = Math.Clamp(tPosX, GameState.MinX + camPosX, GameState.MaxX - camPosX);
            float newPosY = Math.Clamp(tPosY, GameState.MinY + camPosY, GameState.MaxY - camPosY);

            view.Center = new Vector2f(newPosX, newPosY);

            window.SetView(view);
        }

        public void SetTarger(Entity target) => this.target = target;
    }
}
