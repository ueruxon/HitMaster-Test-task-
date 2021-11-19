using System;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class EnemyHolder : MonoBehaviour
    {
        [SerializeField] private EnemyRagdollHandler _ragdollHandler;

        private bool _isDead;

        private void Start() => 
            _ragdollHandler.Hit += OnHit;

        public bool IsDead() => _isDead; 

        private void OnHit() => 
            _isDead = true;

        private void OnDisable()
        {
            _ragdollHandler.Hit -= OnHit;
        }
    }
}