using UnityEngine;

namespace SpaceShooter
{
    public class PowerupSpeedBoost : Powerup
    {
        [SerializeField] private float m_durationTime;
        [SerializeField] private float m_speedModifier;

        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.GetSpeedBoost(m_durationTime, m_speedModifier);
        }
    }
}
