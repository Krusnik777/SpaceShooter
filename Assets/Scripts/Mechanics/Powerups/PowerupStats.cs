using UnityEngine;

namespace SpaceShooter
{
    public class PowerupStats : Powerup
    {
        public enum EffectType
        {
            AddAmmo,
            AddEnergy
        }

        [SerializeField] private EffectType m_effectType;
        [SerializeField] private float m_value;
        protected override void OnPickedUp(SpaceShip ship)
        {
            if (m_effectType == EffectType.AddEnergy)
                ship.AddEnergy((int)m_value);
            if (m_effectType == EffectType.AddAmmo)
                ship.AddAmmo((int)m_value);
        }
    }
}
