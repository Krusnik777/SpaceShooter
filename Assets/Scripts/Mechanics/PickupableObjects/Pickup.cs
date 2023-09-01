using UnityEngine;

namespace SpaceShooter
{
    public abstract class Pickup : MonoBehaviour
    {
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent(out SpaceShip player))
            {
                if (Player.Instance.ActiveShip == player)
                    Destroy(gameObject);
            }
        }
    }
}
