using UnityEngine;

namespace Kamgam.Inheritance.Phase2
{
    [SelectionBase]
    public class EnemyWithRotation : Enemy
    {
        public float Speed = 1f;

        float _progress;
        Quaternion _startRotation;

        public override void Awake()
        {
            base.Awake(); // Don't you ever forget that!
            _startRotation = transform.localRotation;
        }

        public void Update()
        {
            _progress += (Time.deltaTime * Speed) % (Mathf.PI * 2f);
            transform.localRotation = _startRotation * Quaternion.Euler(0f, Mathf.Sin(_progress) * 90f, 0f);
        }
    }
}
