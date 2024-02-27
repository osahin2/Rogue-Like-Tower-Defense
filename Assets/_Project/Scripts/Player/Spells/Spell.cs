using System;
using UnityEngine;

namespace Player.Spells
{
    public abstract class Spell : MonoBehaviour, ISpell
    {

        private SpellConfig _config;
        public SpellType Type => _config.SpellType;

        private float _cooldownTimer;

        public void Construct(SpellConfig config)
        {
            _config = config;
        }

        public void Use()
        {
            if (CheckCooldown())
            {
                ResetCooldown();
                OnUse();
            }
        }
        private void ResetCooldown()
        {
            _cooldownTimer = 0;
        }
        private bool CheckCooldown()
        {
            _cooldownTimer += Time.deltaTime;
            return _cooldownTimer >= _config.Cooldown;
        }
        protected abstract void OnUse();

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}