using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleSystem.SampleEffects
{
    public class DefaultFlashParticleGroup : ParticleGroup
    {
        private int currentDirection = -1;

        private int minNumParticles;
        private int maxNumParticles;

        private float minLifetime;
        private float maxLifetime;

        private float minVelocity;
        private float maxVelocity;

        private float minSize;
        private float maxSize;

        private float effectSpeed = 0.0f;
        private float effectLifeTime = 0.0f;
        private float effectSize = 0.0f;

        public DefaultFlashParticleGroup(int maxParticles) : base(maxParticles)
        {
            SetupParameters();
        }

        public override void InitializeGroup(Vector2 location)
        {
            currentDirection = -1;
            int numberParticles = 
                RandomHelper.PickIntValueFromRange(minNumParticles, maxNumParticles);
            effectSpeed = RandomHelper.PickValueFromRange(minVelocity, maxVelocity);
            effectLifeTime = RandomHelper.PickValueFromRange(minLifetime, maxLifetime);
            effectSize = RandomHelper.PickValueFromRange(minSize, maxSize);
            for (int i = 0; i < numberParticles && freeParticles.Count > 0; i++)
            {
                Particle p = freeParticles.Dequeue();
                InitializeParticle(p, location);
                particles.Add(p);
            }
        }

        protected override void SetupParameters()
        {
            minVelocity = 4.0f;
            maxVelocity = 6.0f;

            minLifetime = 2.0f;
            maxLifetime = 3.0f;

            minSize = 0.3f;
            maxSize = 0.5f;

            minNumParticles = maxParticleCount / 6;

            maxNumParticles = maxParticleCount / 3;
        }

        protected Vector2 PickFlashDirection()
        {
            Vector2 flashDirection;
            if (currentDirection == -1)
            {
                flashDirection = new Vector2(0.0f,0.0f);
                currentDirection++;
                return flashDirection;
            }
            if (currentDirection % 2 == 0)
            {
                flashDirection = new Vector2(1.0f,0.0f);
                if (currentDirection % 4 == 2)
                {
                    flashDirection.X = -1.0f;
                }
            }
            else
            {
                flashDirection = new Vector2(0.0f,1.0f);
                if (currentDirection % 4 == 3)
                {
                    flashDirection.Y = -1.0f;
                }
            }
            currentDirection++;
            return flashDirection;
        }

        protected override void InitializeParticle(Particle particle, Vector2 location)
        {
            Vector2 direction = PickFlashDirection();
            float speed = effectSpeed;
            speed = speed + speed/2.0f * (float)((currentDirection - 1) / 4);
            float lifeTime = effectLifeTime;
            if (currentDirection == 0) lifeTime += 0.2f;
            float size = effectSize;
            particle.Initialize
                (position : location, 
                velocity : speed * direction, 
                acceleration : new Vector2(0.0f,0.0f), 
                lifetime : lifeTime, 
                sizeScale : size, 
                angleVelocity : 0.0f, 
                startAngle : 0.0f);
     
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.Additive);
            Vector2 textureSize = new Vector2();
            textureSize.X = Texture.Width / 2;
            textureSize.Y = Texture.Height / 2;
            foreach (Particle p in particles)
            {
                if (p.Age < p.Lifetime)
                {
                    float percentTime = p.Age / p.Lifetime;
                    float fade = 1.0f - percentTime * percentTime * percentTime * percentTime;
                    Color color = Color.White * fade;
                    p.Draw(spriteBatch, Texture, color, 0.0f);
                }
            }
            spriteBatch.End();
        
        }
    }
    
}
