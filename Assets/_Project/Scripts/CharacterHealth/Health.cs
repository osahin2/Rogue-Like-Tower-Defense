using System;

namespace HealthSystem
{
    public class Health : IHealth
    {
        private float _maxHealth;
        public float CurrentHealth { get; private set; }
        public float HealthPercentage => CurrentHealth / _maxHealth;
        public Health(float maxHealth)
        {
            _maxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void Decrease(float amount, Action onReducedZero)
        {
            CurrentHealth -= amount;
            if (CurrentHealth <= 0)
            {
                onReducedZero?.Invoke();
                CurrentHealth = 0;
            }
        }
        public void Increase(float amount)
        {
            CurrentHealth += amount;

            if (CurrentHealth >= _maxHealth)
            {
                CurrentHealth = _maxHealth;
            }
        }

        public void Reset()
        {
            CurrentHealth = _maxHealth;
        }
    }
}