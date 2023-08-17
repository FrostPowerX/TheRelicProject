using SFML.System;
using SFML.Graphics;

namespace The_Relic
{
    class Entity
    {
        private Texture texture;
        protected Sprite sprite;

        protected Vector2f collisionSize;

        public Vector2f Position { get => sprite.Position; set => sprite.Position = value; }
        public Vector2f Scale { get => sprite.Scale; set => sprite.Scale = value; }
        public Vector2f CollisionSize { get => collisionSize; }
        public float Rotation { get => sprite.Rotation; set => sprite.Rotation = value; }

        public Sprite Graphic { get => sprite; set => sprite = value; }
        public Texture Texture { get => texture; set => texture = value; }

        public bool IsStatic { get; set; }
        public float Mass { get; set; }

        public Entity(string imageFilePath)
        {
            texture = new Texture(imageFilePath);
            sprite = new Sprite(texture);

            collisionSize = new Vector2f(Graphic.GetGlobalBounds().Width / 2f, Graphic.GetGlobalBounds().Height / 2f);
            IsStatic = false;
            Mass = 20;

            sprite.Origin = new Vector2f(Graphic.Texture.Size.X / 2, Graphic.Texture.Size.Y / 2);
            DrawManager.AddMid(this);
        }

        public Entity(Texture texture)
        {
            this.texture = texture;
            sprite = new Sprite(texture);

            collisionSize = new Vector2f(Graphic.GetGlobalBounds().Width / 2f, Graphic.GetGlobalBounds().Height / 2f);
            IsStatic = false;
            Mass = 20;

            sprite.Origin = new Vector2f(Graphic.Texture.Size.X / 2, Graphic.Texture.Size.Y / 2);
            DrawManager.AddFirst(this);
        }

        public void Translate (Vector2f translation) => Position += translation;
        public void Rotate (float rotation) => Rotation += rotation;

        public bool IsColliding(Entity other)
        {
            FloatRect thisRect = Graphic.GetGlobalBounds();
            FloatRect otherRect = other.Graphic.GetGlobalBounds();

            return thisRect.Intersects(otherRect);
        }

        public virtual void Update (float deltaTime, RenderWindow window) { }
        public virtual void OnCollision(Entity other) { }
    }
}