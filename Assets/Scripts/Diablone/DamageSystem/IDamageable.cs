using System;

namespace Diablone.DamageSystem
{
    public interface IDamageable
    {
        public event Action<int> DamageTaken;
        public void TakeDamage(int damage);
    }
}