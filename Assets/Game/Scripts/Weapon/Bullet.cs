using System;
using UnityEngine;

namespace Game.Scripts.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime = 2f;

        private BulletPool _bulletPool;
        private Rigidbody _rb;
        private float _lifeTimer;

        public void Initialize(BulletPool bulletPool) => 
            _bulletPool = bulletPool;

        private void Awake() => 
            _rb = GetComponent<Rigidbody>();
        
        private void Update() {
            _lifeTimer += Time.deltaTime;

            if (_lifeTimer >= _lifeTime) 
                ReturnToPool();
        }
        
        public void MoveToTarget(Vector3 targetPoint)
        {
            Vector3 direction = (targetPoint - transform.position).normalized;
            
            _rb.velocity = direction * _speed;
            _rb.rotation = Quaternion.LookRotation(direction);
        }

        private void ReturnToPool()
        {
            _rb.velocity = Vector3.zero;
            _lifeTimer = 0f;
            transform.position = _bulletPool.transform.position;
            
            _bulletPool.ReturnToPool(this);
        }
    }
}
