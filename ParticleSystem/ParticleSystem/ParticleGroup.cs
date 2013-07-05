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
    //TODO
    public abstract class ParticleGroup
    {

        protected virtual void SetupParameters()
        {
        }

        protected virtual void InitializeAtom(Particle particle, Vector2 location)
        {
        }

        public void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}
