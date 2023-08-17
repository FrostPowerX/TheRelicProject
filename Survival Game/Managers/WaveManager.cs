using SFML.System;
using SFML.Graphics;
using SFML.Window;
using View = SFML.Graphics.View;
using Font = SFML.Graphics.Font;

namespace The_Relic
{
    internal class WaveManager
    {
        List<Enemy> enemyList;
        List<Vector2f> spawnPosList;

        EnemyData data;

        Font font;
        Text waveText;

        Entity target;

        int enemysCount;
        int wave;
        float countBack;
        float waveCountBack;

        public List<Enemy> Enemys { get => enemyList; }

        public WaveManager(RenderWindow window, int enemysCount, EnemyData data, Entity target)
        {
            enemyList = new List<Enemy>();
            spawnPosList = new List<Vector2f>();
            this.target = target;
            this.enemysCount = enemysCount;
            //this.enemysLvl = enemysLvl;
            this.data = data;

            font = new Font("Assets/Fonts/Olivia.ttf");
            waveText = new Text("Initial Wave!", font);
            waveText.Origin = new Vector2f(waveText.GetGlobalBounds().Width / 2, waveText.GetGlobalBounds().Height / 2);

            Vector2f pos1 = new Vector2f();
            spawnPosList.Add(pos1);
            Vector2f pos2 = new Vector2f();
            spawnPosList.Add(pos2);
            Vector2f pos3 = new Vector2f();
            spawnPosList.Add(pos3);
            Vector2f pos4 = new Vector2f();
            spawnPosList.Add(pos4);
        }

        private void OnEnemyDeath(Enemy sender)
        {
            if (enemyList.Contains(sender))
                enemyList.Remove(sender);
            sender.OnDeath -= OnEnemyDeath;

            Console.WriteLine($"Enemigos Restantes {enemyList.Count}");
        }

        private void UpStatsPerWave()
        {
            if (wave % 5 == 0 && wave > 1)
            {
                enemysCount++;
                data.maxHealth += 5;
                data.damage += 0.001f;
                data.health += 5;
            }
            if (wave % 10 == 0 && wave > 1)
            {
                enemysCount += 2;
                data.maxHealth += 20;
                data.damage += 0.005f;
                data.health += 20;
            }
            if (wave % 50 == 0 && wave > 1)
            {
                enemysCount += 5;
                data.maxHealth += 150;
                data.damage += 0.05f;
                data.health += 150;
            }
            if (wave % 100 == 0 && wave > 1)
            {
                enemysCount = 1;
                data.maxHealth += 2000;
                data.damage += 1f;
                data.health += 2000;
            }

            data.damage += 0.0005f;
            data.speed += 0.01f;

            wave++;
        }

        private void WaveController()
        {
            if (enemyList.Count == 0)
            {
                if (waveCountBack <= 0f)
                {
                    countBack = 5f;
                    UpStatsPerWave();
                    GenerateEnemies();
                }
            }
        }

        private void GenerateEnemies()
        {
            Random random = new Random();

            for (int i = 0; i < enemysCount; i++)
            {
                int spawn = random.Next(spawnPosList.Count);

                Enemy enemy = new Enemy("Assets/Sprites/Enemy_1.png", new Vector2i(54, 20), spawnPosList[spawn], data);
                Console.WriteLine($"Enemigo {i} Creado");
                enemyList.Add(enemy);
                enemy.OnDeath += OnEnemyDeath;
            }

            for (int i = 0; i < enemysCount; i++)
            {
                enemyList[i].SetTarget(target);
                Console.WriteLine($"Target Seteado a {target}, en unidad {i}");
            }
        }

        public void Update(float deltaTime, RenderWindow window)
        {
            countBack = (countBack > 0) ? countBack -= deltaTime : 0;
            if (countBack <= 0) waveCountBack = (waveCountBack > 0) ? waveCountBack -= deltaTime : 0;

            if (wave > 1) waveText.DisplayedString = $"Wave {wave}!";

            View view = window.GetView();
            Vector2f center = view.Center;
            Vector2f windowHalfSize = new Vector2f(window.Size.X / 2, window.Size.Y / 2);

            Vector2f waveTextOffSet = new Vector2f(0, -windowHalfSize.Y / 2);

            Random random = new Random();

            spawnPosList[0] = center + new Vector2f(windowHalfSize.X, windowHalfSize.Y);
            spawnPosList[1] = center + new Vector2f(windowHalfSize.X, -windowHalfSize.Y);
            spawnPosList[2] = center + new Vector2f(-windowHalfSize.X, windowHalfSize.Y);
            spawnPosList[3] = center + new Vector2f(-windowHalfSize.X, -windowHalfSize.Y);

            waveText.Position = center + waveTextOffSet;

            WaveController();

            for (int i = 0; i < enemyList.Count; i++)
                enemyList[i].Update(deltaTime, window);
        }

        public void Draw(RenderWindow window)
        {
            for (int i = 0; i < enemyList.Count; i++) if (enemyList[i].Data.health > 0) enemyList[i].healthBar.Draw(window);
            if (countBack > 0)
            {
                waveCountBack = 3f;
                window.Draw(waveText);
            }
        }

        public void Clear()
        {
            enemyList.Clear();
        }
    }
}
