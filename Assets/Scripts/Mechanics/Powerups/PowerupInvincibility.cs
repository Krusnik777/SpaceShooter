using UnityEngine;

namespace SpaceShooter
{
    public class PowerupInvincibility : Powerup
    {
        [SerializeField] private float m_durationTime;

        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.GetShield(m_durationTime);
        }
    }
}
