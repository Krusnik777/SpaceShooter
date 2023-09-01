using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour
    {
        [SerializeField] private float m_force;
        [SerializeField] private float m_radius;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.attachedRigidbody == null) return;

            Vector2 dir = transform.position - collision.transform.position;

            float dist = dir.magnitude;

            if (dist < m_radius)
            {
                Vector2 force = dir.normalized * m_force * (dist / m_radius);
                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
            }
        }

        #if UNITY_EDITOR

        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_radius;
        }

        #endif
    }
}
