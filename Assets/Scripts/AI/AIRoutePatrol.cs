using UnityEngine;

namespace SpaceShooter
{
    public class AIRoutePatrol : MonoBehaviour
    {
        [SerializeField] private AIPointPatrol[] m_routePoints;
        public AIPointPatrol[] RoutePoints => m_routePoints;

        public AIPointPatrol SetNextDestination(AIPointPatrol currentPoint)
        {
            for (int i = 0; i < m_routePoints.Length; i++)
            {
                if (m_routePoints[i] == currentPoint)
                {
                    if (i+1 < m_routePoints.Length)
                    {
                        return m_routePoints[i + 1];
                    }
                }
            }

            return m_routePoints[0];
        }
    }
}
