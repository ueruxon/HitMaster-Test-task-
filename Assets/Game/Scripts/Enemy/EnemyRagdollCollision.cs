using Game.Scripts.Weapon;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class EnemyRagdollCollision : MonoBehaviour
    {
        private EnemyRagdollHandler _ragdollHandler;

        public void Init(EnemyRagdollHandler ragdollHandler) => 
            _ragdollHandler = ragdollHandler;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Bullet bullet)) {
                _ragdollHandler.OnHit();
            }
        }
    }
}
