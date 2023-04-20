using UnityEngine;

namespace Assets.Phase2.Composition.Scripts
{
    public class MovementSinUpDown : MonoBehaviour
    {
        public float Speed = 1f;

        float _progress;
        Vector3 _startPosition;

        public void Awake()
        {
            _startPosition = transform.localPosition;
        }

        public void Update()
        {
            _progress += (Time.deltaTime * Speed) % (Mathf.PI * 2f);

            var pos = transform.position;
            pos.y = _startPosition.y + Mathf.Sin(_progress);
            transform.position = pos;
        }
    }
}