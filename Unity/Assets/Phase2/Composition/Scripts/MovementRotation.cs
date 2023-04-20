using UnityEngine;

namespace Assets.Phase2.Composition.Scripts
{
    public class MovementRotation : MonoBehaviour
    {
        public float Speed = 1f;

        float _progress;
        Quaternion _startRotation;

        public void Awake()
        {
            _startRotation = transform.localRotation;
        }

        public void Update()
        {
            _progress += (Time.deltaTime * Speed) % (Mathf.PI * 2f);
            transform.localRotation = _startRotation * Quaternion.Euler(0f, Mathf.Sin(_progress) * 90f, 0f);
        }
    }
}