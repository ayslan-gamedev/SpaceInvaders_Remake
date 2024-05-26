using System;
using SpaceInvaders.Core;
using SpaceInvaders.Utilities;
using UnityEngine;

namespace SpaceInvaders.Enemy
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public unsafe class Invader : MonoBehaviour
    {
        public readonly int InvaderID = GlobalValues.InvadersNewID;
        
        public Sprite[] sprites;
        private int _currentSprite;

        private Vector2 _direction;
        private float _speed;
        
        private SpriteRenderer _spriteRenderer;
        private Movement _movement;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            GetComponent<CircleCollider2D>().radius = 0.25f;
        }

        private void Start()
        {
            fixed (Vector2* pInputDirection = &_direction)
            fixed (float* pPlayerSpeed = &_speed)
            {
                _movement = new Movement(transform, pInputDirection, pPlayerSpeed);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Move Invader to direction
        /// </summary>
        /// <param name="direction">direction to move</param>
        public void MoveInvader(Vector2 direction)
        {
            while (true)
            {
                _direction = direction;

                var invaderMove = Move().IsOk;

                if (invaderMove) return;
                Start();
            }
        }
        
        /// <summary>
        /// Move the object
        /// </summary>
        /// <returns> error case missing movement, or true case successful</returns>
        private Result<bool, Exception> Move()
        {
            if (_movement is null)
            {
                return new Exception("_movement is null");
            }
            
            _currentSprite = _currentSprite == sprites.Length ? _currentSprite++ : 0;
            _spriteRenderer.sprite = sprites[_currentSprite];
            _movement.Move();            
            
            return true;
        }
    }
}
