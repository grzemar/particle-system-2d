using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleSystem
{
    public abstract class ParticleGroup
    {
        protected Queue<Particle> freeParticles;
        protected List<Particle> particles;
        protected int maxParticleCount;

        public Texture2D Texture
        {
            get;
            private set;
        }

        public ParticleGroup(int maxParticles)
        {
            freeParticles = new Queue<Particle>();
            particles = new List<Particle>();
            maxParticleCount = maxParticles;
            for (int i = 0; i < maxParticleCount; i++)
            {
                Particle p = new Particle();
                freeParticles.Enqueue(p);
            }
        }

        public void LoadContent(ContentManager content, string fileName)
        {
            Texture = content.Load<Texture2D>(fileName);  
        }

        public virtual void InitializeGroup()
        {
        }

        protected virtual void SetupParameters()
        {
        }

        protected virtual void InitializeParticle(Particle particle, Vector2 location)
        {
        }

        public void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (Particle p in particles)
            {
                p.Update(time);
                if (!(p.Age < p.Lifetime))
                {
                    freeParticles.Enqueue(p);
                    p.Reset();
                }
            }
            particles.RemoveAll( (item) => !(item.Age < item.Lifetime));
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

    }
}
