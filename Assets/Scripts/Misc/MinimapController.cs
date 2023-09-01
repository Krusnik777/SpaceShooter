using UnityEngine;

namespace SpaceShooter
{
    public class MinimapController : MonoBehaviour
    {
        [SerializeField] private Transform m_minimapCamera;
        [SerializeField] private Transform m_target;

        private void LateUpdate()
        {
            if (m_target == null || m_minimapCamera == null) return;

            Vector3 newPosition = new Vector3(m_target.position.x, m_target.position.y, m_minimapCamera.position.z);
            m_minimapCamera.position = newPosition;
        }

        public void SetTarget(Transform newTarget)
        {
            m_target = newTarget;
        }

    }
}
