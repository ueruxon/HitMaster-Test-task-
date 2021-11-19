using System;
using System.Collections;
using UnityEngine;
using Game.Scripts.Enemy;

namespace Game.Scripts.Player
{
    public class Radar : MonoBehaviour
    {
        public event Action PlaceCleaned;

        [SerializeField] private float _radarDelay = 0.3f;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _radius = 10f;

        private Collider[] _enemies = new Collider[5];
        private EnemyHolder _closestEnemy;
        private int _enemiesOnPlace;
        private bool _activate;

        public void FindEnemies()
        {
            _activate = true;
            StartCoroutine(FindEnemyRoutine());
        }

        public EnemyHolder GetClosestEnemy() => _closestEnemy;

        private IEnumerator FindEnemyRoutine()
        {
            while (_activate)
            {
                GetEnemyInfo();
                
                if (_enemiesOnPlace == 0)
                {
                    PlaceCleaned?.Invoke();
                    _activate = false;
                }
                
                yield return new WaitForSeconds(_radarDelay);
            }
        }

        private void GetEnemyInfo()
        {
            int allEnemyOnPlace = GetEnemyCount();
            int aliveEnemyOnPlace = 0;

            EnemyHolder nearbyEnemy = null;

            foreach (Collider enemyCollider in _enemies)
            {
                if (enemyCollider != null && enemyCollider.TryGetComponent(out EnemyHolder enemy))
                {
                    if (enemy.IsDead() == false)
                    {
                        aliveEnemyOnPlace++;
                        
                        if (nearbyEnemy == null)
                        {
                            nearbyEnemy = enemy;
                        }
                        else
                        {
                            float distanceBetweenEnemy = (enemy.transform.position - transform.position).sqrMagnitude;
                            float distanceBetweenNearbyEnemy =
                                (nearbyEnemy.transform.position - transform.position).sqrMagnitude;

                            if (distanceBetweenEnemy < distanceBetweenNearbyEnemy)
                                nearbyEnemy = enemy;
                        }
                    }
                }
            }

            _enemiesOnPlace = aliveEnemyOnPlace;
            _closestEnemy = nearbyEnemy;
        }

        private int GetEnemyCount() => 
            Physics.OverlapSphereNonAlloc(transform.position, _radius, _enemies, _enemyLayer);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}