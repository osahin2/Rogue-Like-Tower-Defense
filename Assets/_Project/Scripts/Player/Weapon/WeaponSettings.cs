using UnityEngine;

namespace Player.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Weapon Settings")]
    public class WeaponSettings : ScriptableObject
    {
        [SerializeField] private WeaponType _type;
        [SerializeField] private Weapon _weaponPrefab;

        public Weapon WeaponPrefab => _weaponPrefab;
        public WeaponType WeaponType => _type;
    }

}