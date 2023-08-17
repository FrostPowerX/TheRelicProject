using SFML.System;
using SFML.Graphics;

namespace The_Relic
{
    abstract class LoopState
    {
        protected RenderWindow window;
        private bool isRunning;

        static Time deltaTime;
        static Clock clock = new Clock();

        protected bool isPaused;

        public LoopState(RenderWindow window)
        {
            this.window = window;
        }

        protected virtual void OnCloseWindow(object sender, EventArgs e)
        {
            isRunning = false;
            window.Close();
        }

        protected abstract void Start();
        protected abstract void Update(float deltaTime);
        protected abstract void Draw();
        protected abstract void Finish();

        public void Play()
        {
            isRunning = true;
            isPaused = false;

            Start();

            while (isRunning)
            {
                deltaTime = clock.Restart();

                window.DispatchEvents();

                if (!isPaused)
                {                 
                    Update(deltaTime.AsSeconds());

                    window.Clear();
                    Draw();
                    window.Display();
                }
            }

            Finish();
        }

        public void Pause()
        {
            if (isPaused)
            {
                Console.WriteLine("Ya esta pausado.");
                return;
            }

            isPaused = true;
        }
        public void Resume()
        {
            if (!isPaused)
            {
                Console.WriteLine("No esta Pausado.");
                return;
            }

            isPaused = false;
        }
        public void Stop()
        {
            if (!isRunning)
            {
                Console.WriteLine("Cannot stop a state that is not running.");
                return;
            }

            isRunning = false;

            Finish();
        }
    }
}
