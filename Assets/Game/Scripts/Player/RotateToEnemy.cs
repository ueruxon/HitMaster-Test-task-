using Game.Scripts.Enemy;
using UnityEngine;
using DG.Tweening;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(Movement))]
    public class RotateToEnemy : MonoBehaviour
    {
        [SerializeField] private Radar _radar;
        [SerializeField] private Movement _movement;
        [SerializeField] private Vector3 _offsetFromEnemy;
        [SerializeField] private float _rotationDuration;

        private bool _canRotate = false;

        private void Start()
        {
            _movement.ReachedToWaypoint += OnReachedToWaypoint;
        }

        private void LateUpdate()
        {
            if (_canRotate)
                RotatoToTarget();
        }

        private void OnReachedToWaypoint()
        {
            _canRotate = true;
        }

        private void RotatoToTarget()
        {
            EnemyHolder _nearbyEnemy = _radar.GetClosestEnemy();

            if (_nearbyEnemy == null)
                return;

            transform.DOLookAt(_nearbyEnemy.transform.position + _offsetFromEnemy, _rotationDuration);
        }
    }
}
