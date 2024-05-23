using System;
using UnityEngine;

namespace SpaceInvaders
{
    public unsafe class Player : MonoBehaviour
    {
        [SerializeField] private float playerSpeed;
    
        private const string AxesHorizontal = "Horizontal";
    
        private Vector2 _inputDirection;
        private Movement _movement;

        private void Start()
        {
            fixed (Vector2* pInputDirection = &_inputDirection)
            fixed (float* pPlayerSpeed = &playerSpeed)
            {
                _movement = new Movement(transform, pInputDirection, pPlayerSpeed);
            }
        }

        private void Update()
        {
            var playerMove = MovePlayer().IsOk;
        
            if (!playerMove)
            {
                Start();
            }
        }
    
        private Result<bool, Exception> MovePlayer()
        {
            if (_movement is null)
            {
                return new Exception("_movement is null");
            }

            var playerInput = new Vector2(
                x: Input.GetAxisRaw(AxesHorizontal),
                y: 0);
            _inputDirection = playerInput;

            // Verify Borders
            var isOnLimitMap = !(transform.position.x <= -GlobalValues.LimitMaxX && playerInput.x < 0 ||    // player < X
                                 transform.position.x >= GlobalValues.LimitMinX && playerInput.x > 0);      // player > X

            if (isOnLimitMap)
            {
                // Move the player
                _movement.Move();
            }
        
            return true;
        }
    }
}
