using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class KeyCard : Pickup
    {
        public UnityEvent KeyCardPicked;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            KeyCardPicked?.Invoke();
        }
    }
}
