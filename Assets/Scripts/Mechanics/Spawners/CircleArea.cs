using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    public class CircleArea : MonoBehaviour
    {
        [SerializeField] private float m_radius;
        [SerializeField] private float m_saveZoneRadius;
        [SerializeField] private bool m_ignoreSaveZone;
        public float Radius => m_radius;

        private Vector2 saveZonePoint;

        private bool CheckSpaceShipPosition(Vector2 checkPos)
        {
            SpaceShip ship = Player.Instance.ActiveShip;

            if (ship != null)
            {
                saveZonePoint = ship.transform.position;

                if (checkPos.x <= saveZonePoint.x + m_saveZoneRadius
                    && checkPos.x >= saveZonePoint.x - m_saveZoneRadius
                    && checkPos.y <= saveZonePoint.y + m_saveZoneRadius
                    && checkPos.y >= saveZonePoint.y - m_saveZoneRadius)
                {
                    return true;
                }
            }

            return false;
        }

        public Vector2 GetRandomInsideZone()
        {
            var value = (Vector2)transform.position + (Vector2)UnityEngine.Random.insideUnitSphere * m_radius;

            if (!m_ignoreSaveZone)
            {
                while (CheckSpaceShipPosition(value))
                {
                    value = (Vector2)transform.position + (Vector2)UnityEngine.Random.insideUnitSphere * m_radius;
                }
            }

            return value;
        }

        #if UNITY_EDITOR

        private static Color gizmoColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Handles.color = gizmoColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_radius);
        }

        #endif
    }
}
