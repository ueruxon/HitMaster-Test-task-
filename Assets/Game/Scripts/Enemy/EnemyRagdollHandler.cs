using System;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class EnemyRagdollHandler : MonoBehaviour
    {
        public event Action Hit;
        
        [SerializeField] private Animator _animator;

        private Rigidbody[] _allRigidbodys;
        private bool isActive = true;

        private void Awake() => 
            InitializeRagdoll();

        private void InitializeRagdoll()
        {
            _allRigidbodys = GetComponentsInChildren<Rigidbody>();

            for (int i = 0; i < _allRigidbodys.Length; i++)
            {
                _allRigidbodys[i].isKinematic = true;

                if (gameObject.name != _allRigidbodys[i].gameObject.name)
                {
                    _allRigidbodys[i].gameObject
                        .AddComponent<EnemyRagdollCollision>()
                        .Init(this);
                }
            }
        }

        public void OnHit()
        {
            if (isActive)
            {
                Hit?.Invoke();
                MakePhysical();
            }
        }

        private void MakePhysical()
        {
            _animator.enabled = false;
            isActive = false;

            for (int i = 0; i < _allRigidbodys.Length; i++)
            {
                _allRigidbodys[i].isKinematic = false;
            }
        }
    }
}