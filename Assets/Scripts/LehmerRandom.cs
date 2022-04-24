using System;

namespace Assets.Scripts
{
    internal static class LehmerRandom
    {
        private static int state = (int)DateTime.Now.ToBinary();
        private static int m = 39916801;
        private static int a = m / 1024;

        public static void InitState(int seed)
        {
            state = seed;
        }

        public static float Range()
        {
            state = a * state % m;

            return (float)state / m;
        }
    }
}