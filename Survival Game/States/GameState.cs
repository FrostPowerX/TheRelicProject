using SFML.System;
using SFML.Audio;
using SFML.Window;
using SFML.Graphics;
using KeyEventArgs = SFML.Window.KeyEventArgs;
using Color = SFML.Graphics.Color;

namespace The_Relic
{
    class GameState : LoopState
    {
        private Player player;
        private Entity ground;

        private HUD hud;
        private Camera camera;
        private WaveManager waveManager;

        public const int MaxX = 200000;
        public const int MinX = 0;
        public const int MaxY = 200000;
        public const int MinY = 0;

        public bool firstStart = true;

        public event Action OnWin;
        public event Action OnDeath;
        public event Action GameMenu;

        public event Action PowerUps;

        public GameState (RenderWindow window) : base (window) { }

        private void OnPressKey(object sender, KeyEventArgs keyEventArgs)
        {
            if (Input.GetAxis("Cancel") == 1)
            {
                if (!isPaused)
                    GameMenu?.Invoke();
            }
        }

        private void OnPlayerDeath() => OnDeath?.Invoke();
        private void OnPLayerWin()=> OnWin?.Invoke();

        protected override void Start()
        {
            Vector2f playerPosition = new Vector2f(MaxX / 2f, MaxY / 2f);
            Vector2i playerSize = new Vector2i(44, 27);

            EnemyData enemyData = new EnemyData
            {
                damage = 0.05f,
                speed = 125f,
                health = 25f,
                maxHealth = 25f
            };

            Cursor.EnableCross(true);
            Cursor.Rainbow(false);
            Cursor.SetCrossColor(new Color(100,255,200));

            AudioManager.InGameMusic.Play();

            player = new Player("Assets/Sprites/Player_Sheet.png",
                playerSize,
                playerPosition,
                new CharacterData
                {
                    baseHealth = 100f,
                    health = 100,
                    maxHealth = 100,
                    baseATQInterval = 1f,
                    intervalATQTime = 1f,
                    speed = 130,
                    bulletData = new BulletData {baseDmg = 65f, damage = 65f, baseSpeed = 300f, speed = 300f, bulletCount = 1, lifeTimeOfBullet = 1.2f}
                });

            ground = new Entity(TextureManager.texturesIDs[0]);
            
            ground.Graphic.TextureRect = new IntRect(0, 0, MaxX, MaxY);

            player.Scale = new Vector2f(0.7f, 0.7f);

            hud = new HUD(window, player, "Assets/Fonts/Olivia.ttf");

            camera = new Camera(window, player);

            waveManager = new WaveManager(window, 2, enemyData, player);

            window.KeyPressed += OnPressKey;
            window.Closed += OnCloseWindow;
            player.OnDeath += OnPlayerDeath;
            player.OnWin += OnPLayerWin;

            if (firstStart == true)
            {
                PowerUps?.Invoke();
                firstStart = false;
            }
        }

        protected override void Update (float deltaTime)
        {
            Cursor.Update(deltaTime, window);

            player.Update(deltaTime, window);

            camera.Update(deltaTime);
            hud.Update();

            waveManager.Update(deltaTime, window);

            CollisionsHandler.Update();
        }

        protected override void Draw ()
        {
            DrawManager.Draw(window);
            player.healthBar.Draw(window);
            waveManager.Draw(window);
            hud.Draw();
            Cursor.Draw(window);
        }

        protected override void Finish ()
        {
            waveManager.Clear();
            DrawManager.Clear();
            CollisionsHandler.Clear();

            AudioManager.InGameMusic.Stop();
            AudioManager.MainMenuMusic.Play();

            window.KeyPressed -= OnPressKey;
            window.Closed -= OnCloseWindow;
            player.OnDeath -= OnPlayerDeath;
            player.OnWin -= OnPlayerDeath;
        }
    }
}
