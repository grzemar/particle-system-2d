using System;
using Microsoft.Xna.Framework;

namespace ParticleSystem
{
    public static class RandomHelper
    {
        private static Random random = new Random();

        public static float PickValueFromRange(float minValue, float maxValue)
        {
            return minValue + (float)random.NextDouble() * (maxValue - minValue);
        }

        public static int PickIntValueFromRange(int minValue, int maxValue)
        {
            return minValue + random.Next(maxValue - minValue + 1);
        }

        public static Vector2 PickRandomDirection()
        {
            float angle = (float)random.NextDouble() * MathHelper.TwoPi;
            Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            return direction;
        }
    }
}
