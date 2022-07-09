using System;

namespace Diablone.DamageSystem
{
    public interface IHasHealth
    {
        public int Health { get; }
        public event Action Died;
    }
}