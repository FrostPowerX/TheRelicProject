using SFML.Graphics;
using SFML.System;

namespace The_Relic
{
    internal class Bullet : Entity
    {
        BulletData data;

        string bulletName;
        float acumTime;

        Player owner;

        public event Action<Bullet> OnDeath;

        public Vector2f Direction { get; set; }
        public Player OwnerName { get => owner; private set => owner = value; }
        public BulletData Data { get => data; private set => data = value; }

        public Bullet(string bulletName, Player owner, BulletData bulletData, Vector2f position, float rotation) : base(bulletData.imageFilePath)
        {
            CollisionsHandler.AddEntity(this);

            this.bulletName = bulletName;
            this.owner = owner;

            data = bulletData;

            Position = position;
            Rotation = rotation;
        }

        private void Move(float deltaTime)
        {
            Translate(Direction * data.speed * deltaTime);
        }

        public override void Update(float deltaTime, RenderWindow window)
        {
            collisionSize = new Vector2f(Graphic.GetGlobalBounds().Width / 2f, Graphic.GetGlobalBounds().Height / 2f);

            acumTime += deltaTime;

            if (acumTime >= Data.lifeTimeOfBullet)
            {
                Finish();
                OnDeath?.Invoke(this);
            }

            Move(deltaTime);
        }

        public void Finish()
        {
            DrawManager.RemoveMid(this);
            CollisionsHandler.RemoveEntity(this);
        }

        public override void OnCollision(Entity other)
        {
            if (other is Enemy enemy)
            {
                enemy.TakeDamage(this);

                Finish();
                OnDeath?.Invoke(this);
            }
        }
    }
}
