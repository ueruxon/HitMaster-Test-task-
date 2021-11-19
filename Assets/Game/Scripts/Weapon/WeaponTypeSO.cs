using UnityEngine;

namespace Game.Scripts.Weapon
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon")]
    public class WeaponTypeSO : ScriptableObject
    {
        [Range(0.1f, 1f)]
        public float Delay = 0.5f;
        [Range(2f, 5f)]
        public float ReloadTime = 2f;
        public GameObject GunPrefab;
    }
}
