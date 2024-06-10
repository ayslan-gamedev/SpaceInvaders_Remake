using UnityEngine;

namespace SpaceInvaders
{
    public class DynamicColor : MonoBehaviour
    {
        private readonly GameObject[] _objects = new GameObject[2];
        
        // Start is called before the first frame update
        private void Start()
        {
            var s = GetComponent<SpriteRenderer>().sprite;
            
            _objects[0] = new GameObject(gameObject.name + "_green");
            _objects[1] = new GameObject(gameObject.name + "_red");
            
            var gs = _objects[0].AddComponent<SpriteRenderer>();
            gs.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            gs.sprite = s;
            gs.color = Color.green;
            Instantiate(_objects[0], transform.position, transform.rotation, transform);
    
            gs = _objects[1].AddComponent<SpriteRenderer>();
            gs.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            gs.sprite = s;
            gs.color = Color.red;
            Instantiate(_objects[1], transform.position, transform.rotation, transform);
        }

        // Update is called once per frame
        private void Update()
        {
            if (transform.position.y < 0)
            {
                _objects[0].SetActive(true);
                _objects[1].SetActive(false);
            }
            else
            {
                _objects[0].SetActive(false);
                _objects[1].SetActive(true);
            }
        }
    }
}
