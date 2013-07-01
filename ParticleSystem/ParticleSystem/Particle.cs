using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleSystem
{
    public class Particle
    {
        public Vector2 Position
        {
            get;
            set;
        }

        public Vector2 Velocity
        {
            get;
            set;
        }

        public Vector2 Acceleration
        {
            get;
            set;
        }

        public float Lifetime
        {
            get;
            set;
        }

        public float Age
        {
            get;
            set;
        }

        public float SizeScale
        {
            get;
            set;
        }

        public float Angle
        {
            get;
            set;
        }

        public float AngleVelocity
        {
            get;
            set;
        }

        public Particle()
        {
            Reset();
        }

        public void Initialize(Vector2 position, Vector2 velocity, Vector2 acceleration, 
            float lifetime, float sizeScale, float angleVelocity, float startAngle)
        {
            this.Position = position;
            this.Acceleration = acceleration;
            this.Velocity = velocity;
            this.Lifetime = lifetime;
            this.SizeScale = sizeScale;
            this.AngleVelocity = angleVelocity;
            this.Angle = startAngle;

            this.Age = 0.0f;
        }

        public void Update(float time)
        {
            Angle += AngleVelocity * time;
            Velocity += Acceleration * time;
            Position += Velocity * time;

            float x = Position.X;
            float y = Position.Y;
            Age += time;
        }

        public void Reset()
        {
            Position = new Vector2(0.0f,0.0f);
            Velocity = new Vector2(0.0f,0.0f);
            Acceleration = new Vector2(0.0f,0.0f);
            Lifetime = 0.0f;
            Age = 0.0f;
            SizeScale = 0.0f;
            Angle = 0.0f;
            AngleVelocity = 0.0f;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Color color,float layerofDepth)
        {
            Vector2 vector = new Vector2(texture.Width/2,texture.Height/2);
            spriteBatch.Draw(texture, Position, null, color, Angle, vector, SizeScale, SpriteEffects.None, layerofDepth);
        }
    }
}
