using System;

namespace The_Relic
{
    public struct UpgradeData
    {
        public float healthRestore;

        public float health;
        public float speed;

        public float damage;
        public float atqInterval;

        public float bulletDmg;
        public float bulletSpeed;
        public UpgradeData()
        {
            healthRestore = 0;

            health = 0;
            speed = 0;

            damage = 0;
            atqInterval = 0;

            bulletDmg = 0;
            bulletSpeed = 0;
        }
    }
}
