using System.Collections;
using Game.Scripts.Weapon;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class Shooting : MonoBehaviour
    {
        private readonly int GunShoot = Animator.StringToHash("Shoot");

        [Header("References")] 
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private Radar _radar;
        [SerializeField] private Movement _movement;
        [SerializeField] private InputHandler _input;
        [SerializeField] private Transform _gunPoint;
        [SerializeField] private Animator _animator;
        [Space(2)] 
        [SerializeField] private float _shootDelay = .6f;
        [SerializeField] private LayerMask _enemyLayer;
        
        private bool _canShoot;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _input.Touched += OnTouched;
            _movement.ReachedToWaypoint += OnReachedToWaypoint;
        }

        private void OnReachedToWaypoint()
        {
            _radar.FindEnemies();
            _canShoot = true;
        }

        private void OnTouched()
        {
            if (_canShoot) 
                Shoot();
        }

        private void Shoot()
        {
            RaycastHit hit;

            Vector3 touchPosition = _input.GetTouchOnScreen();
            Ray ray = _camera.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out hit, 50f, _enemyLayer)) 
                StartCoroutine(ShotRoutine(hit.point));
        }

        private IEnumerator ShotRoutine(Vector3 hitPoint)
        {
            _canShoot = false;
            Shot(hitPoint);
            
            yield return new WaitForSeconds(_shootDelay);
            _canShoot = true;
        }

        private void Shot(Vector3 hitPoint)
        {
            Bullet bullet = _bulletPool.GetBullet(_gunPoint.position);

            bullet.Initialize(_bulletPool);
            bullet.MoveToTarget(hitPoint);

            _animator.SetTrigger(GunShoot);
        }

        private void OnDisable()
        {
            _input.Touched -= OnTouched;
        }
    }
}