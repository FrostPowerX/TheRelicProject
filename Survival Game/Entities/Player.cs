using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using Color = SFML.Graphics.Color;

namespace The_Relic
{
    class Player : AnimatedEntity
    {
        CharacterData data;
        UpgradeData upgrade;

        List<Bullet> bullets = new List<Bullet>();

        public SlidingBar healthBar;

        float acumTime;

        bool isAlive;
        bool readyFire;

        public CharacterData Data { get => data; private set => data = value; }
        public bool IsAlive { get => isAlive; private set => isAlive = value; }

        public event Action OnDeath;
        public event Action OnWin;

        public Player(string imageFilePath, Vector2i frameSize, Vector2f position, CharacterData playerData) : base(imageFilePath, frameSize)
        {
            CollisionsHandler.AddEntity(this);

            upgrade = new UpgradeData();
            data = playerData;
            Graphic.Position = position;

            healthBar = new SlidingBar();
            healthBar.SetFillingColor(Color.Green);
            healthBar.SetScale(new Vector2f(0.7f, 0.7f));

            IsStatic = true;

            isAlive = true;
            readyFire = true;

            AnimationData idle = new AnimationData()
            {
                frameRate = 7,
                framesCount = 4,
                rowIndex = 1,
                loop = true
            };

            AnimationData walk = new AnimationData()
            {
                frameRate = 7,
                framesCount = 5,
                rowIndex = 0,
                loop = true
            };

            AddAnimation("Idle", idle);
            AddAnimation("Walk", walk);

            SetCurrentAnimation("Idle");
        }

        private void OnBulletDeath(Bullet sender)
        {
            if (bullets.Contains(sender))
                bullets.Remove(sender);
            sender.OnDeath -= OnBulletDeath;
        }
        private void RefreshData()
        {
            data.maxHealth = data.baseHealth + upgrade.health;
            data.speed += data.baseSpeed * upgrade.speed;
            data.intervalATQTime = (upgrade.atqInterval > 0.9f) ? data.baseATQInterval - data.baseATQInterval * 0.9f : data.baseATQInterval - data.baseATQInterval * upgrade.atqInterval;
            Console.WriteLine(data.intervalATQTime);

            data.bulletData.damage = data.bulletData.baseDmg + upgrade.bulletDmg;
            data.bulletData.speed += data.bulletData.baseSpeed * upgrade.bulletSpeed;

            if (data.bulletData.speed > 800f) data.bulletData.speed = 800f;
        }

        void Move(float deltaTime, RenderWindow window) // Movimiento del Personaje
        {
            Vector2f input = new Vector2f(0f, 0f);

            input.X = Input.GetAxis("Horizontal");

            input.Y = Input.GetAxis("Vertical");

            if (input.X > 0f || input.Y > 0f)
            {
                SetCurrentAnimation("Walk");
            }
            else if (input.X < 0f || input.Y < 0f)
            {
                SetCurrentAnimation("Walk");
            }
            else
            {
                SetCurrentAnimation("Idle");
            }

            if (input.X != 0 || input.Y != 0)
            {
                if (AudioManager.FootStepsSound.Status == SoundStatus.Paused || AudioManager.FootStepsSound.Status == SoundStatus.Stopped)
                    AudioManager.FootStepsSound.Play();
            }
            else if (AudioManager.FootStepsSound.Status == SoundStatus.Playing)
                    AudioManager.FootStepsSound.Pause();

            Vector2f normalizedInput = VectorUtility.Normalize(input);
            Vector2f translation;

            translation = normalizedInput * data.speed * deltaTime;

            float newPosX = Math.Clamp(Position.X + translation.X, GameState.MinX + frameSize.X / 2, GameState.MaxX - frameSize.X);
            float newPosY = Math.Clamp(Position.Y + translation.Y, GameState.MinY + frameSize.Y / 2, GameState.MaxY - frameSize.Y);

            Position = new Vector2f(newPosX, newPosY);
        }
        void RotateFacingToMouse(float deltaTime, RenderWindow window) // Rotamos el Sprite Segun la posicion del mouse
        {
            Vector2f windowPos = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
            Vector2i cursorPos = Mouse.GetPosition(window);

            float dx = windowPos.X - cursorPos.X;
            float dy = windowPos.Y - cursorPos.Y;

            float rotation = MathF.Atan2(dx, dy) * -180 / MathF.PI;

            sprite.Rotation = rotation;
        }
        void HealthBar()
        {
            float halfSizeY = collisionSize.Y;
            Vector2f healthPos = new Vector2f(Position.X, Position.Y + halfSizeY);
            float percentOfLife = data.health / data.maxHealth;

            healthBar.SetPercent(percentOfLife);
            healthBar.SetPosition(healthPos);
        }

        public void TakeDamage(Entity other)
        {
            if (other is Enemy enemy)
            {
                AudioManager.DmgRecievedSound.Play();
                data.health = (enemy.Data.damage < data.health) ? data.health - enemy.Data.damage : 0;
                if (data.health <= 0)
                {
                    isAlive = false;
                    OnDeath?.Invoke();
                    DrawManager.RemoveMid(this);
                    CollisionsHandler.RemoveEntity(this);
                }
            }
        }
        public void Consumible(string name, UpgradeData stats)
        {
            if (name == "Relic") OnWin?.Invoke();

            data.health += stats.healthRestore;

            upgrade.health += stats.health;
            upgrade.speed += stats.speed;

            upgrade.damage += stats.damage;
            upgrade.atqInterval += stats.atqInterval;

            upgrade.bulletDmg += stats.bulletDmg;
            upgrade.bulletSpeed += stats.bulletSpeed;

            RefreshData();
        }
        public void Fire(float deltaTime, RenderWindow window)
        {
            if (readyFire == false) return;
            if (Input.GetAxis("Fire1") != 1) return;

            readyFire = false;
            acumTime = 0;

            AudioManager.ShotSound.Play();

            Vector2f windowPos = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
            Vector2i cursorPos = Mouse.GetPosition(window);

            Vector2f direction = VectorUtility.Normalize(((Vector2f)cursorPos) - windowPos);

            for (int i = 0; i < data.bulletData.bulletCount; i++)
            {
                Bullet bullet = new Bullet("Normal Bullet", this, data.bulletData, Position, Rotation);

                bullet.Direction = direction;

                bullet.OnDeath += OnBulletDeath;
                bullets.Add(bullet);
            }

            
        }

        public override void Update (float deltaTime, RenderWindow window)
        {
            base.Update(deltaTime, window);
            acumTime += deltaTime;

            collisionSize = new Vector2f(Graphic.GetGlobalBounds().Width / 2f, Graphic.GetGlobalBounds().Height / 2f);
            data.health = (data.health > data.maxHealth) ? data.maxHealth : data.health;
            if (acumTime >= data.intervalATQTime) readyFire = true;

            RotateFacingToMouse(deltaTime, window);
            Move(deltaTime, window);

            Fire(deltaTime, window);

            HealthBar();

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(deltaTime, window);
            }
        }
        public override void OnCollision(Entity other)
        {
            if (other is Bullet || other is Enemy) TakeDamage(other);
        }

        public void KilledEnemy() => data.enemysKilled++;
    }
}
