using UnityEngine;

namespace SpaceInvaders
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DynamicColor : MonoBehaviour
    {
        private readonly SpriteRenderer[] _objects = new SpriteRenderer[2];
        private SpriteRenderer _renderer;
        private SpriteRenderer _spriteRenderer;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _renderer = _spriteRenderer;
            _renderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            
            for (var i = 0; i < _objects.Length; i++)
            {
                var o = Instantiate(new GameObject(gameObject.name + i + "_x"), 
                                                                         transform.position, transform.rotation, 
                                                                         transform);
                o.name = gameObject.name + "color" + i;
                Destroy(GameObject.Find(gameObject.name + i + "_x"));

                var r = o.AddComponent<SpriteRenderer>();
                r.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

                _objects[i] = r;
            }
        }

        private void LateUpdate()
        {
            foreach (var t in _objects)
            {
                t.sprite = _renderer.sprite;
            }

            if (transform.position.y < 0)
            {
                _objects[0].color = Color.green;
                _objects[1].color = Color.clear;
            }
            else
            {                
                _objects[0].color = Color.clear;
                _objects[1].color = Color.red;
            }
        }
    }
}
