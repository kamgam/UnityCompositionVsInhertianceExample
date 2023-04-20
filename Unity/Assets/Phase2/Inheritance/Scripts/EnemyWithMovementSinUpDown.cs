using UnityEngine;

namespace Kamgam.Inheritance.Phase2
{
    [SelectionBase]
    public class EnemyWithMovementSinUpDown : Enemy
    {
        public float Speed = 1f;

        float _progress;
        Vector3 _startPosition;

        public override void Awake()
        {
            base.Awake(); // Don't you ever forget that!
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
