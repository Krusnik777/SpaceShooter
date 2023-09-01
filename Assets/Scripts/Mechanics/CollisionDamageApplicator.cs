using UnityEngine;

namespace SpaceShooter
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";

        [SerializeField] private float m_velocityDamageModifier;
        [SerializeField] private int m_damageConstant;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag) return;

            var destructible = transform.root.GetComponent<Destructible>();

            if (destructible != null)
            {
                destructible.ApplyDamage(this, m_damageConstant + (int)(m_velocityDamageModifier * collision.relativeVelocity.magnitude));
            }
        }
    }
}
