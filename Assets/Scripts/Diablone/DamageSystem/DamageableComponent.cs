using System;
using UnityEngine;

namespace Diablone.DamageSystem
{
    public class DamageableComponent : MonoBehaviour, IDamageable, IHasHealth
    {
        [SerializeField] private int _maxHealth = 50;
        private int _currentHealth;
        
        public int Health 
        { 
            get => _currentHealth;
            private set
            {
                if (value > _maxHealth)
                {
                    _currentHealth = _maxHealth;
                }
                else if (value <= 0)
                {
                    _currentHealth = 0;
                    Died?.Invoke();
                }
            }
        }

        public event Action Died;
        public event Action<int> DamageTaken;
        
        public void TakeDamage(int damage)
        {
            DamageTaken?.Invoke(damage);
            Health -= damage;
        }
    }
}