using UnityEngine;

namespace SpaceInvaders.Core
{
    public unsafe class Movement
    {
        private readonly Transform _transform;
        private readonly Vector2* _direction;

        private readonly float* _speed;
    
        public Movement(Transform transform, Vector2 *direction, float *speed)
        {
            _transform = transform;
            _direction = direction;
            _speed = speed;
        }
    
        
        public Movement(Transform transform, Vector2 *direction)
        {
            _transform = transform;
            _direction = direction;
            _speed = (float*)1;
        }
        
        /// <summary>
        /// Move the object
        /// </summary>
        public void Move()
        {
            var direction = *_direction * *_speed;
            _transform.Translate(direction);
        }
    }
}
