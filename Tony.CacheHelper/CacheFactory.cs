namespace Tony.CacheHelper
{
   public class CacheFactory
    {
        public static ICache Cache()
        {
            return new Cache();
        }
    }
}
