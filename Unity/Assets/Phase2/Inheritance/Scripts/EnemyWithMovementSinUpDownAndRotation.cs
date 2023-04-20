using UnityEngine;

namespace Kamgam.Inheritance.Phase2
{
    [SelectionBase]
    public class EnemyWithMovementSinUpDownAndRotation : Enemy
    {
        // Movement
        public float MovementSpeed = 1f;
        float _movementProgress;
        Vector3 _startPosition;

        // Rotation
        public float RotationSpeed = 1f;
        float _rotationProgress;
        Quaternion _startRotation;

        public override void Awake()
        {
            base.Awake(); // Don't you ever forget that!
            _startPosition = transform.localPosition;
            _startRotation = transform.localRotation;
        }

        public void Update()
        {
            // Movement
            _movementProgress += (Time.deltaTime * MovementSpeed) % (Mathf.PI * 2f);

            var pos = transform.position;
            pos.y = _startPosition.y + Mathf.Sin(_movementProgress);
            transform.position = pos;

            // Rotation
            _rotationProgress += (Time.deltaTime * RotationSpeed) % (Mathf.PI * 2f);
            transform.localRotation = _startRotation * Quaternion.Euler(0f, Mathf.Sin(_rotationProgress) * 90f, 0f);
        }
    }
}
