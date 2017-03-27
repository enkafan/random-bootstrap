using System;

namespace RandomBootstrap.Services
{
    public static class RandomExtensions
    {
        public static bool Bool(this Random random, int probability = 50)
        {
            return random.Next(100) <= probability;
        }

        public static T PickItem<T>(this Random random, T[] items)
        {
            return items[random.Next(0, items.Length)];
        }
    }
}