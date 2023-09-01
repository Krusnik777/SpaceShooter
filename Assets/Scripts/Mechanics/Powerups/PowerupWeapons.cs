using UnityEngine;

namespace SpaceShooter
{
    public class PowerupWeapons : Powerup
    {
        [SerializeField] private TurretProperties m_properties;

        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.AssignWeapon(m_properties);
        }
    }
}
