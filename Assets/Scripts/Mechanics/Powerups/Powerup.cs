using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Powerup : MonoBehaviour
    {

        private bool bySpawner;
        private PowerupSpawner m_spawner;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent(out SpaceShip ship))
            {
                if (Player.Instance.ActiveShip == ship)
                {
                    OnPickedUp(ship);

                    if (bySpawner)
                    {
                        m_spawner.OnPowerupPicked();
                    }

                    Destroy(gameObject);
                }
            }
        }

        public void MarkTheSpawner(PowerupSpawner sender)
        {
            bySpawner = true;

            m_spawner = sender;
        }

        protected abstract void OnPickedUp(SpaceShip ship);
    }
}
