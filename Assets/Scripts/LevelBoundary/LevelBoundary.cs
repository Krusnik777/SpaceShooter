using UnityEngine;

namespace SpaceShooter
{
    public class LevelBoundary : SingletonBase<LevelBoundary>
    {
        [SerializeField] private float m_radius;
        public float Radius => m_radius;

        public enum Mode
        {
            Limit,
            Teleport
        }

        [SerializeField] private Mode m_limitMode;
        public Mode LimitMode => m_limitMode;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_radius);
        }
#endif
    }
}
