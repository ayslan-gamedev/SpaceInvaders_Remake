using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Enemy
{
    public class InvaderManager : MonoBehaviour
    {
        [SerializeField] private InvadersSpritesUI[] invadersSprites;
        
        public int lines, rows;

        public float initialYPosition, initialXPosition;
        public float distanceY, distanceX;

        private List<List<Invader>> _invaders;
        
        [Serializable]
        private struct InvadersSpritesUI
        { 
            public Sprite[] frames;
        }
     
        private void Start()
        {
            GenerateInvaders();
            StartCoroutine(SetInvadersInitialPosition()); 
        }
        
        /// <summary>
        /// Generate all invaders
        /// </summary>
        private void GenerateInvaders()
        {
            var id = 0;
            _invaders = new List<List<Invader>>();
            
            for (var line = 0; line < lines; line++)
            {
                _invaders.Add(new List<Invader>());
                for (var row = 0; row < rows; row++)
                {
                    var o = Instantiate(
                        new GameObject($"temp {id}"),
                        new Vector2(10000, 10000),
                        transform.rotation,
                        transform);
                    
                    // delete original
                    o.name = $"invader {id}";
                    Destroy(GameObject.Find($"temp {id}"));
                    
                    var oSprite = o.AddComponent<SpriteRenderer>();
                    oSprite.sprite = invadersSprites[line].frames[0];
                    
                    var oInvader = o.AddComponent<Invader>();
                    oInvader.sprites = invadersSprites[line].frames;
                    
                    _invaders[line].Add(oInvader);
                    id++;
                }
            }
        }
        
        /// <summary>
        /// Set all Invaders to your initial position
        /// </summary>
        /// <returns></returns>
        private IEnumerator SetInvadersInitialPosition()
        {
            var y = initialYPosition;
            for (var line = 0; line < lines; line++)
            {
                var x = initialXPosition;
                for (var row = 0; row < rows; row++)
                {
                    _invaders[line][row].transform.position = new Vector3(x, y);
                    x += distanceX;
                    yield return new WaitForSeconds(0.015f);
                }
                y-=distanceY;
            }
        }
        
        // ReSharper disable once UnusedMember.Local
        private IEnumerator MoveInvaders()
        {
            // foreach (var invader in _invaders)
            // {
            //     invader.MoveInvader(Vector2.right);
            //     yield return new WaitForSeconds(0.5f);
            // }

            for (var line = 0; line < lines + 1; line++)
            {
                for (var cell = rows; cell >= 0; cell--)
                {
                    _invaders[cell][line].MoveInvader(Vector2.right);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }

        // /// <summary>
        // /// Kill Alien
        // /// </summary>
        // /// <param name="invaderId"></param>
        // public void KillInvader(int invaderId)
        // {
        //     foreach (var currentInvader 
        //              in _invaders.ToList().Where(currentInvader => currentInvader.InvaderID == invaderId))
        //     {
        //         Destroy(currentInvader.gameObject);
        //         _invaders.Remove(currentInvader);
        //     }
        // }
    }
}
