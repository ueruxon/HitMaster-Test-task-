using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Weapon
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _bulletAmount = 20;

        private List<Bullet> _bulletPool = new List<Bullet>();

        private void Start() => Fill();

        public Bullet GetBullet(Vector3 position)
        {
            Bullet bullet = TryGetBulletInPool();
            
            bullet.gameObject.SetActive(true);
            bullet.transform.position = position;
            return bullet;
        }

        public void ReturnToPool(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            bullet.transform.SetParent(transform);
        }

        private Bullet TryGetBulletInPool()
        {
            Bullet bullet = null;
            
            for (int i = 0; i < _bulletPool.Count; i++)
            {
                if (_bulletPool[i].gameObject.activeInHierarchy == false)
                {
                    bullet = _bulletPool[i];
                    break;
                }
            }

            if (bullet == null)
            {
                bullet = MakeBullet();
                _bulletPool.Add(bullet);
                
                return bullet;
            }
            else
                return bullet;
        }

        private void Fill()
        {
            for (int i = 0; i < _bulletAmount; i++)
                _bulletPool.Add(MakeBullet());
        }

        private Bullet MakeBullet()
        {
            Bullet bullet = Instantiate(_bulletPrefab, transform);
            bullet.transform.SetParent(transform);
            bullet.gameObject.SetActive(false);
            return bullet;
        }
    }
}