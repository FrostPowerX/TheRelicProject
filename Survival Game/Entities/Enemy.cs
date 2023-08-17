using SFML.System;
using SFML.Audio;
using SFML.Graphics;
using Color = SFML.Graphics.Color;

namespace The_Relic
{
    internal class Enemy : AnimatedEntity
    {
        EnemyData data;
        private Entity target;

        float acumDmgInterval;
        float timer;

        bool vulnerable;
        bool isAlive;

        public event Action<Enemy> OnDeath;

        public SlidingBar healthBar;
        public EnemyData Data { get => data; private set => data = value; }
        public bool IsAlive { get => isAlive; private set => isAlive = value; }

        public Enemy(string imageFilePath, Vector2i frameSize, Vector2f position, EnemyData data): base(imageFilePath, frameSize)
        {
            CollisionsHandler.AddEntity(this);
            this.data = data;
            Graphic.Position = position;

            healthBar = new SlidingBar();
            healthBar.SetFillingColor(Color.Red);
            healthBar.SetScale(new Vector2f(0.8f, 0.8f));

            vulnerable = true;
            isAlive = true;

            AnimationData idle = new AnimationData()
            {
                frameRate = 7,
                framesCount = 2,
                rowIndex = 2,
                loop = true
            };

            AnimationData walk = new AnimationData()
            {
                frameRate = 7,
                framesCount = 5,
                rowIndex = 1,
                loop = true
            };

            AnimationData explode = new AnimationData()
            {
                frameRate = 7,
                framesCount = 5,
                rowIndex = 0,
                loop = false
            };

            AddAnimation("Idle", idle);
            AddAnimation("Walk", walk);
            AddAnimation("Explode", explode);

            SetCurrentAnimation("Idle");
        }

        private void AnimatedDmgRecieved()
        {
            if (vulnerable)
            {
                sprite.Color = new Color(255, 255, 255);
                return;
            }

            sprite.Color = new Color(255, 100, 100);
        }
        private void RotateFacingToMouse() // Rotamos el Sprite Segun la posicion del mouse
        {
            if (target == null) return;

            float dx = target.Position.X - Position.X;
            float dy = target.Position.Y - Position.Y;

            float rotation = MathF.Atan2(dx, dy) * -180 / MathF.PI;

            sprite.Rotation = rotation;
        }
        private void MoveToTarget(float deltaTime)
        {
            if (target == null) return;

            Vector2f direction = VectorUtility.Normalize(target.Position - Position);

            if (direction.X == 0 && direction.Y == 0) SetCurrentAnimation("Idle");
            else SetCurrentAnimation("Walk");

            Translate(direction * data.speed * deltaTime);
        }
        private void HealthBar()
        {
            float halfSizeY = collisionSize.Y;
            Vector2f healthPos = new Vector2f(Position.X, Position.Y + halfSizeY);
            float percentOfLife = data.health / data.maxHealth;

            healthBar.SetPercent(percentOfLife);
            healthBar.SetPosition(healthPos);
        }
        private void Death(float deltaTime)
        {
            timer += deltaTime;

            CollisionsHandler.RemoveEntity(this);

            if (isAlive)
            {
                CreateConsumible();

                SetCurrentAnimation("Explode");
                AudioManager.ExplodeSound.Play();
            }

            isAlive = false;

            if (timer >= currentFrameTime * 5)
            {
                OnDeath?.Invoke(this);
                DrawManager.RemoveMid(this);
            }
        }

        public void TakeDamage(Entity other)
        {
            if (other is Bullet bullet)
            {
                if (vulnerable)
                {
                    vulnerable = false;
                    acumDmgInterval = 0;

                    AudioManager.DmgRecievedSound.Play();
                    data.health = (bullet.Data.damage <= data.health) ? data.health - bullet.Data.damage : 0;
                }

                if (data.health <= 0)
                {
                    bullet.OwnerName.KilledEnemy();
                }
            }
        }

        private void CreateConsumible()
        {
            Random random = new Random();
            double percentage = (float)random.NextDouble();

            if (percentage <= 0.001f)
            {
                UpgradeData data = new UpgradeData();
                Consumible cons = new Consumible("Relic", "Assets/Sprites/Relic.png", data);
                cons.Position = Position;
                return;
            }

            else if (percentage <= 0.10f)
            {
                int rand = random.Next(0, 5);

                UpgradeData data;
                Consumible cons;


                switch (rand)
                {
                    case 0:
                        data = new UpgradeData
                        {
                            bulletDmg = 15f
                        };
                        cons = new Consumible("DmgUp", "Assets/Sprites/DmgBulletUp.png", data);
                        cons.Position = Position;
                        return;

                    case 1:
                        data = new UpgradeData
                        {
                            bulletSpeed = 0.002f
                        };
                        cons = new Consumible("DmgUp", "Assets/Sprites/BulletVelUp.png", data);
                        cons.Position = Position;
                        return;
                    case 2:
                        data = new UpgradeData
                        {
                            bulletSpeed = 0.001f
                        };
                        cons = new Consumible("DmgUp", "Assets/Sprites/BulletVelUp.png", data);
                        cons.Position = Position;
                        return;

                    case 3:
                        data = new UpgradeData
                        {
                            atqInterval = 0.05f
                        };
                        cons = new Consumible("HealthUp", "Assets/Sprites/ReloadUp.png", data);
                        cons.Position = Position;
                        return;
                    case 4:
                        data = new UpgradeData
                        {
                            atqInterval = 0.02f
                        };
                        cons = new Consumible("HealthUp", "Assets/Sprites/ReloadUp.png", data);
                        cons.Position = Position;
                        return;
                }
            }
            else if (percentage <= 0.20f)
            {
                int rand = random.Next(0, 2);

                UpgradeData data;
                Consumible cons;

                switch(rand)
                {
                    case 0:
                        data = new UpgradeData
                        {
                            health = 25f
                        };
                        cons = new Consumible("HealthUp", "Assets/Sprites/MaxHealthUp.png", data);
                        cons.Position = Position;
                        return;

                    case 1:
                        data = new UpgradeData
                        {
                            healthRestore = 999999999f
                        };
                        cons = new Consumible("FullHealing", "Assets/Sprites/MedKit.png", data);
                        cons.Position = Position;
                        return;
                }
            }
        }
        public void SetTarget(Entity target)
        {
            this.target = target;
        }

        public override void Update(float deltaTime, RenderWindow window)
        {
            base.Update(deltaTime, window);

            if (data.health <= 0) Death(deltaTime);
            if (!isAlive) return;

            collisionSize = new Vector2f(Graphic.GetGlobalBounds().Width / 2f, Graphic.GetGlobalBounds().Height / 2f);
            acumDmgInterval += deltaTime;

            if (acumDmgInterval >= 0.05f) vulnerable = true;

            MoveToTarget(deltaTime);
            RotateFacingToMouse();
            AnimatedDmgRecieved();
            HealthBar();
        }
    }
}
