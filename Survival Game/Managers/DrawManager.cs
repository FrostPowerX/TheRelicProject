using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace The_Relic
{
    static class DrawManager
    {
        static List<Entity> drawEntFirst = new List<Entity>();
        static List<Entity> drawEntMiddle = new List<Entity>();
        static List<Entity> drawEntFinal = new List<Entity>();

        public static void Draw(RenderWindow window)
        {
            for (int i = 0; i < drawEntFirst.Count; i++)
            {
                window.Draw(drawEntFirst[i].Graphic);
            }
            for (int i = 0; i < drawEntMiddle.Count; i++)
            {
                window.Draw(drawEntMiddle[i].Graphic);
            }
            for (int i = 0; i < drawEntFinal.Count; i++)
            {
                window.Draw(drawEntFinal[i].Graphic);
            }
        }

        public static void  Clear()
        {
            drawEntFirst.Clear();
            drawEntMiddle.Clear();
            drawEntFinal.Clear();
        }

        public static void AddFirst(Entity entity)
        {
            if (!drawEntFirst.Contains(entity))
                drawEntFirst.Add(entity);
        }

        public static void AddMid(Entity entity)
        {
            if (!drawEntMiddle.Contains(entity))
                drawEntMiddle.Add(entity);
        }

        public static void AddFinal(Entity entity)
        {
            if (!drawEntFinal.Contains(entity))
                drawEntFinal.Add(entity);
        }

        public static void RemoveFirst(Entity entity)
        {
            if (drawEntFirst.Contains(entity))
                drawEntFirst.Remove(entity);
        }

        public static void RemoveMid(Entity entity)
        {
            if (drawEntMiddle.Contains(entity))
                drawEntMiddle.Remove(entity);
        }

        public static void RemoveFinal(Entity entity)
        {
            if (drawEntFinal.Contains(entity))
                drawEntFinal.Remove(entity);
        }
    }
}
