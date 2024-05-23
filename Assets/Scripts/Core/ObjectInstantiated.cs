using UnityEngine;

namespace SpaceInvaders.Core
{
    public class ObjectInstantiated : MonoBehaviour
    {    
        [SerializeField] private float moveSpeed;

        [SerializeField] private Vector2 direction = Vector2.up;

        private Movement _movement;
    
        private unsafe void Start()
        {
            fixed (Vector2* vector2 = &direction)
            fixed (float* speed = &moveSpeed)
            {
                _movement = new Movement(transform, vector2, speed);
            }
        }

        private void Update()
        {
            var objectIsInsideGameArea = transform.position is 
            { 
                x: > GlobalValues.LimitMinX and < GlobalValues.LimitMaxY, 
                y: > GlobalValues.LimitMinY and < GlobalValues.LimitMaxY 
            };
        
            if(!objectIsInsideGameArea)
            {
                Destroy(gameObject);
            }
        
            _movement.Move();
        }
    }
}
