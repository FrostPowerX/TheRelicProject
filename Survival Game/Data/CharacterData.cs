using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Relic
{
    public struct CharacterData
    {
        public BulletData bulletData;

        public string name;

        public float baseHealth;
        public float health;
        public float maxHealth;

        public float baseSpeed;
        public float speed;
        public float speedSprint;

        public float damage;
        public float baseATQInterval;
        public float intervalATQTime;

        public int enemysKilled;
        public CharacterData()
        {
            bulletData = new BulletData();

            name = "Player";

            baseHealth = 0f;
            maxHealth = baseHealth;
            health = baseHealth;

            baseSpeed = 0f;
            speed = baseSpeed;
            speedSprint = speed * 0f;

            damage = 0f;
            baseATQInterval = 0f;
            intervalATQTime = baseATQInterval;

            enemysKilled = 0;
        }

        public CharacterData(CharacterData copy)
        {
            bulletData = copy.bulletData;
            name = copy.name;

            baseHealth = copy.baseHealth;
            health = copy.health;
            maxHealth = copy.maxHealth;

            baseSpeed = copy.baseSpeed;
            speed = copy.speed;
            speedSprint = copy.speedSprint;

            damage = copy.damage;
            baseATQInterval = copy.baseATQInterval;
            intervalATQTime = copy.intervalATQTime;

            enemysKilled = copy.enemysKilled;
        }
    }
}
