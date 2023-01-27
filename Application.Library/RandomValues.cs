namespace Application.Library
{
    public static class RandomValues
    {
        public static int RandomNumber(int min, int max)
        {
            var rand = new Random();
            return rand.Next(min, max);
        }
    }
}
