using System;
using System.Collections.Generic;
using SFML.System;

namespace The_Relic
{
    static class CollisionsHandler
    {
        private static List<Entity> entities = new List<Entity>();

        private static void GetMassRatios(Entity entityA, Entity entityB, out float ratioA, out float ratioB)
        {
            if (!entityA.IsStatic && !entityB.IsStatic)
            {
                float massSum = entityA.Mass + entityB.Mass;

                ratioA = entityB.Mass / massSum;
                ratioB = entityA.Mass / massSum;
            }
            else
            {
                ratioA = (entityA.IsStatic) ? 0f : 1f;
                ratioB = (entityB.IsStatic) ? 0f : 1f;
            }
        }

        private static void SolveCollision(Entity entityA, Entity entityB)
        {
            if (entityA is Bullet || entityB is Bullet) return;
            if (entityA is Consumible || entityB is Consumible) return;

            Vector2f separationA;
            Vector2f separationB;

            float widthAGB = entityA.CollisionSize.X;
            float heightAGB = entityA.CollisionSize.Y;

            float widthBGB = entityB.CollisionSize.X;
            float heightBGB = entityB.CollisionSize.Y;

            Vector2f entityACenteredPos = entityA.Position;
            Vector2f entityBCenteredPos = entityB.Position;

            Vector2f diff = entityBCenteredPos - entityACenteredPos;

            float minHorDistance = widthAGB + widthBGB;
            float minVerDistance = heightAGB + heightBGB;

            float horPenetration = minHorDistance - Math.Abs(diff.X);
            float verPenetration = minVerDistance - Math.Abs(diff.Y);

            bool isPositiveDiff;
            float displacementA;
            float displacementB;
            float displacementASign;
            float displacementBSign;
            float massRatioA;
            float massRatioB;

            GetMassRatios(entityA, entityB, out massRatioA, out massRatioB);

            if (horPenetration > verPenetration) // Colisionan verticalmente (una para arriba y otra para abajo)
            {
                isPositiveDiff = (diff.Y > 0f);
                displacementASign = (!isPositiveDiff) ? 1f : -1f;
                displacementBSign = (isPositiveDiff) ? 1f : -1f;

                displacementA = verPenetration * massRatioA * displacementASign;
                displacementB = verPenetration * massRatioB * displacementBSign;

                separationA = new Vector2f(0f, displacementA);
                separationB = new Vector2f(0f, displacementB);
            }
            else // Colisionan horizontalmente (una para la izquierda y otra para la derecha)
            {
                isPositiveDiff = (diff.X > 0f);
                displacementASign = (!isPositiveDiff) ? 1f : -1f;
                displacementBSign = (isPositiveDiff) ? 1f : -1f;

                displacementA = horPenetration * massRatioA * displacementASign;
                displacementB = horPenetration * massRatioB * displacementBSign;

                separationA = new Vector2f(displacementA, 0f);
                separationB = new Vector2f(displacementB, 0f);
            }

            entityA.Translate(separationA);
            entityB.Translate(separationB);
        }

        public static void AddEntity(Entity entity)
        {
            if (!entities.Contains(entity))
                entities.Add(entity);
        }

        public static void RemoveEntity(Entity entity)
        {
            if (entities.Contains(entity))
                entities.Remove(entity);
        }

        public static void Clear()
        {
            entities.Clear();
        }

        public static void Update()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                for (int j = i + 1; j < entities.Count; j++)
                {
                    if (entities[i].IsColliding(entities[j])) 
                    {
                            SolveCollision(entities[i], entities[j]);
                            entities[i].OnCollision(entities[j]);
                            entities[j].OnCollision(entities[i]);
                    }
                }
            }
        }
    }
}

