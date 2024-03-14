using System;

namespace HealthSystem
{
    public interface IHealth
    {
        public float HealthPercentage { get; }
        void Decrease(float amount, Action onReducedZero);
        void Increase(float amount);
        void Reset();
    }
}