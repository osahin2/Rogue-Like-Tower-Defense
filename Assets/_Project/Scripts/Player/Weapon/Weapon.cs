using UnityEngine;

namespace Player.Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Transform _leftHandPosition;
        [SerializeField] private Transform _rightHandPosition;

        public Vector3 LeftHandPosition => _leftHandPosition.position;
        public Vector3 RightHandPosition => _rightHandPosition.position;

        protected WeaponConfig settings;

        public void Init(WeaponConfig settings)
        {
            this.settings = settings;
        }
        public virtual void Fire()
        {

        }

        public virtual void Dispose()
        {
            Destroy(gameObject);
        }
    }
}