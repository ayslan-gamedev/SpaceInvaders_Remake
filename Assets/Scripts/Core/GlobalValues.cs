namespace SpaceInvaders.Core
{
    public static class GlobalValues
    {
        public const float LimitMaxY = 10;
        public const float LimitMinY = -5;
    
        public const float LimitMaxX = 7;
        public const float LimitMinX = -7;

        private static int _invaders;

        public static int InvadersNewID
        {
            get
            {
                _invaders++;
                return _invaders - 1;
            }
        }
    }
}