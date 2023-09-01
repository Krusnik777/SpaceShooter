using UnityEngine;

namespace SpaceShooter
{
    public class SyncTransform : MonoBehaviour
    {
        [SerializeField] private Transform m_target;

        private void Update()
        {
            transform.position = new Vector3(m_target.position.x, m_target.position.y, transform.position.z);
        }

    }
}
