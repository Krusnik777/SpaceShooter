using UnityEngine;

namespace SpaceShooter
{
    public class TriggerOnDestroy : Trigger
    {
        private void OnDestroy()
        {
            OnTrigger();
        }
    }
}
