using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceInvaders.Enemy
{
    public class InvaderManager : MonoBehaviour
    {
        [SerializeField] private InvadersSpritesUI[] invadersSprites;
        
        public int lines, rows;

        public float initialYPosition, initialXPosition;
        public float distanceY, distanceX;

        private List<Invader> _invaders;
        
        [Serializable]
        private struct InvadersSpritesUI
        { 
            public Sprite[] frames;
        }
     
        private void Start()
        {
            GenerateInvaders();
        }
        
        private void GenerateInvaders()
        {
            var count = 0;
            var y = initialYPosition;
            
            for (var line = 0; line < lines; line++)
            {
                var x = initialXPosition;
                for (var cell = 0; cell < rows; cell++)
                {
                    var o = Instantiate(
                        new GameObject(), 
                        transform.position, 
                        transform.rotation, 
                        transform);
                    o.name = $"invader[{count}]";
                    o.transform.Translate(new Vector2(x, y));
                    
                    var oSprite = o.AddComponent<SpriteRenderer>();
                    oSprite.sprite = invadersSprites[line].frames[0];
                    
                    var oInvader = o.AddComponent<Invader>();
                    oInvader.sprites = invadersSprites[line].frames;
                    
                    _invaders.Add(oInvader);
                    x += distanceX;
                    count++;
                }
                y+=distanceY;
            }
        }

        // ReSharper disable once UnusedMember.Local
        private void MoveInvaders()
        {
            foreach (var invader in _invaders)
            {
                invader.MoveInvader(Vector2.right);
            }
        }
        
        /// <summary>
        /// Kill Alien
        /// </summary>
        /// <param name="invader"></param>
        public void KillInvader(Invader invader)
        {
            foreach (var currentInvader 
                     in _invaders.ToList().Where(currentInvader => currentInvader.InvaderID == invader.InvaderID))
            {
                _invaders.Remove(currentInvader);
            }
        }
    }
}
