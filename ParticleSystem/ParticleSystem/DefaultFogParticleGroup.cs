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
    public class DefaultFogParticleGroup: ParticleGroup
    {
        private int minNumParticles;
        private int maxNumParticles;

        private float minLifetime;
        private float maxLifetime;

        private float minVelocity;
        private float maxVelocity;

        private float minSize;
        private float maxSize;

        public DefaultFogParticleGroup(int maxParticles) : base(maxParticles)
        {
            SetupParameters();
        }

        public override void InitializeGroup(Vector2 location)
        {
            int numberParticles = 
                RandomHelper.PickIntValueFromRange(minNumParticles, maxNumParticles);
            for (int i = 0; i < numberParticles && freeParticles.Count > 0; i++)
            {
                Particle p = freeParticles.Dequeue();
                InitializeParticle(p, location);
                particles.Add(p);
            }
        }

        protected override void SetupParameters()
        {
            minVelocity = 6.0f;
            maxVelocity = 10.0f;

            minLifetime = 5.0f;
            maxLifetime = 6.0f;

            minSize = 1.2f;
            maxSize = 1.8f;

            minNumParticles = maxParticleCount / 6;

            maxNumParticles = maxParticleCount / 3;
        }

        protected override void InitializeParticle(Particle particle, Vector2 location)
        {
            Vector2 direction = RandomHelper.PickRandomDirection();
            float speed = RandomHelper.PickValueFromRange(minVelocity, maxVelocity);
            float lifeTime = RandomHelper.PickValueFromRange(minLifetime, maxLifetime);
            float size = RandomHelper.PickValueFromRange(minSize, maxSize);
            float startAngle = RandomHelper.PickValueFromRange(-MathHelper.Pi, MathHelper.Pi);
            particle.Initialize
                (position : location, 
                velocity : speed * direction, 
                acceleration : new Vector2(0.0f,0.0f), 
                lifetime : lifeTime, 
                sizeScale : size, 
                angleVelocity : 0.0f, 
                startAngle : startAngle);
     
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend);
            Vector2 textureSize = new Vector2();
            textureSize.X = Texture.Width / 2;
            textureSize.Y = Texture.Height / 2;
            foreach (Particle p in particles)
            {
                if (p.Age < p.Lifetime)
                {
                    float percentTime = p.Age / p.Lifetime;
                    float fade = 3.0f * percentTime * (1.0f - percentTime);
                    Color color = Color.Gray * fade;
                    p.Draw(spriteBatch, Texture, color, 0.0f);
                }
            }
            spriteBatch.End();
        
        }
    }
}
