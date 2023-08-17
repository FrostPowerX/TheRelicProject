using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Relic
{
    public struct BulletData
    {
        public float baseDmg;
        public float damage;
        public float baseSpeed;
        public float speed;
        public float bulletCount;
        public float lifeTimeOfBullet;
        public int penetration;

        public string imageFilePath;

        public BulletData()
        {
            baseDmg = 0f;
            damage = baseDmg;
            baseSpeed = 0f;
            speed = baseSpeed;
            bulletCount = 0f;
            lifeTimeOfBullet = 0f;
            penetration = 0;

            imageFilePath = "Assets/Sprites/Bullet.png";
        }

        public BulletData(BulletData copy)
        {
            baseDmg = copy.baseDmg;
            damage = copy.damage;
            baseSpeed = copy.baseSpeed;
            speed = copy.speed;
            bulletCount = copy.bulletCount;
            lifeTimeOfBullet = copy.lifeTimeOfBullet;
            penetration = copy.penetration;

            imageFilePath = copy.imageFilePath;
        }
    }
}
