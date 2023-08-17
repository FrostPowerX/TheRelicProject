using System;

namespace The_Relic
{
    public struct EnemyData
    {
        public float health;
        public float maxHealth;
        public float speed;

        public float damage;
        public float intervalATQTime;

        public EnemyData()
        {
            maxHealth = 0f;
            health = maxHealth;
            speed = 0f;

            damage = 0f;
            intervalATQTime = 0f;
        }

        public EnemyData(EnemyData copy)
        { 
            health = copy.health;
            maxHealth = copy.maxHealth;
            speed = copy.speed;

            damage = copy.damage;
            intervalATQTime = copy.intervalATQTime;
        }
    }
}
