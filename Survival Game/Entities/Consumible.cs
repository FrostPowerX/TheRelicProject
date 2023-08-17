using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

namespace The_Relic
{
    internal class Consumible : Entity
    {
        string name;
        UpgradeData data;

        public Consumible(string name, string imageFilePath, UpgradeData data) : base (imageFilePath)
        {
            this.name = name;
            this.data = data;

            CollisionsHandler.AddEntity(this);
            DrawManager.AddMid(this);
        }

        public Consumible(Consumible copy) : base(copy.Texture)
        {
            name = copy.name;
            data = copy.data;

            CollisionsHandler.AddEntity(this);
            DrawManager.AddMid(this);
        }

        public override void OnCollision(Entity other)
        {
            if (other is Player target)
            {
                target.Consumible(name, data);
                CollisionsHandler.RemoveEntity(this);
                DrawManager.RemoveMid(this);
            }
        }
    }
}
