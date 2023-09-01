using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        public enum PatrolMode
        {
            SingleZone,
            MultipleZones,
            Route
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;
        [SerializeField] private PatrolMode m_patrolMode;

        [SerializeField] private AIPointPatrol m_patrolPoint;

        [SerializeField] private AIRoutePatrol m_patrolRoute;

        [Range(0.0f, 1.0f)] [SerializeField] private float m_navigationLinear;
        [Range(0.0f, 1.0f)] [SerializeField] private float m_navigationAngular;
        [SerializeField] private float m_randomSelectMovePointTime;
        [SerializeField] private float m_findNewTargetTime;
        [SerializeField] private float m_leadPredictionTime;
        [SerializeField] private float m_shootDelay;
        [SerializeField] private float m_shootRange;
        [SerializeField] private float m_evadeRayLength;

        private SpaceShip m_spaceShip;
        private Vector3 m_movePosition;
        private Destructible m_selectedTarget;
        private AIPointPatrol m_prevPatrolPoint;

        private Timer m_randomizeDirectionTimer;
        private Timer m_fireTimer;
        private Timer m_findNewTargetTimer;

        private void Start()
        {
            m_spaceShip = GetComponent<SpaceShip>();

            InitTimers();

            SetPatrolBehaviour(m_patrolPoint);
        }

        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        private void UpdateAI()
        {
            if (TryGetComponent(out Boss boss))
            {
                if (boss != null & boss.InBreakState)
                    return;
            }

            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;

            if (point != null) m_patrolPoint = point;

            if (m_patrolMode == PatrolMode.Route && m_patrolRoute != null)
            {
                m_patrolPoint = m_patrolRoute.RoutePoints[0];
            }
        }

        private void ActionFindNewMovePosition()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_selectedTarget != null)
                {
                    m_movePosition = MakeLead(m_selectedTarget);
                }
                else
                {
                    if (m_patrolPoint != null)
                    {
                        if (m_patrolMode == PatrolMode.SingleZone) GetRandomDirectionInSingleZone();
                        if (m_patrolMode == PatrolMode.MultipleZones) GetRandomDirectionInMultipleZones();
                    }
                    else
                    {
                        if (AIPointPatrol.AllPatrolPoints != null)
                            m_patrolPoint = FindNearestPatrolPoint(); 
                    }

                    if (m_patrolRoute != null && m_patrolMode == PatrolMode.Route) GetDirectionAlongTheRoute();
                }

            }
        }

        private void GetRandomDirectionInSingleZone()
        {
            bool isInsidePatrolZone = (m_patrolPoint.transform.position - transform.position).sqrMagnitude < m_patrolPoint.Radius * m_patrolPoint.Radius;

            if (isInsidePatrolZone)
            {
                if (m_randomizeDirectionTimer.IsFinished)
                {
                    Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_patrolPoint.Radius + m_patrolPoint.transform.position;

                    m_movePosition = newPoint;

                    m_randomizeDirectionTimer.Start(m_randomSelectMovePointTime);
                }
            }
            else
            {
                m_movePosition = m_patrolPoint.transform.position;
            }
        }

        private void GetRandomDirectionInMultipleZones()
        {
            GetRandomDirectionInSingleZone();

            foreach (var point in AIPointPatrol.AllPatrolPoints)
            {
                if (point == m_patrolPoint) continue;

                bool isInsideAnotherPatrolZone = (point.transform.position - transform.position).sqrMagnitude < point.Radius * point.Radius;
                if (isInsideAnotherPatrolZone && point != m_prevPatrolPoint)
                {
                    m_prevPatrolPoint = m_patrolPoint;
                    m_patrolPoint = point;
                }
            }
        }

        private void GetDirectionAlongTheRoute()
        {
            bool isInsidePatrolZone = (m_patrolPoint.transform.position - transform.position).sqrMagnitude < m_patrolPoint.Radius * m_patrolPoint.Radius;

            if (isInsidePatrolZone)
            {
                m_patrolPoint = m_patrolRoute.SetNextDestination(m_patrolPoint);
            }
            else
            {
                m_movePosition = m_patrolPoint.transform.position;
            }
        }

        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_evadeRayLength) == true)
            {
                m_movePosition = transform.position + transform.right * 100.0f;
            }
        }

        private void ActionControlShip()
        {
            m_spaceShip.ThrustControl = m_navigationLinear;

            m_spaceShip.TorqueControl = ComputeAlignTorqueNormalized(m_movePosition, m_spaceShip.transform) * m_navigationAngular;

        }

        private const float MAX_ANGLE = 45.0f;

        private static float ComputeAlignTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }

        private void ActionFindNewAttackTarget()
        {
            if (m_findNewTargetTimer.IsFinished)
            {
                m_selectedTarget = FindNearestDestructibleTarget();

                m_findNewTargetTimer.Start(m_findNewTargetTime);
            }
        }

        private void ActionFire()
        {
            if (m_selectedTarget != null)
            {
                float dist = Vector2.Distance(m_spaceShip.transform.position, m_selectedTarget.transform.position);

                if (dist < m_shootRange)
                {
                    if (m_fireTimer.IsFinished)
                    {
                        m_spaceShip.Fire(TurretMode.Primary);

                        m_fireTimer.Start(m_shootDelay);
                    }
                }
            }
        }

        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = float.MaxValue;

            Destructible potentialTarget = null;

            foreach(var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == m_spaceShip) continue;

                if (v.TeamId == Destructible.TeamIdNeutral) continue;

                if (v.TeamId == m_spaceShip.TeamId) continue;

                float dist = Vector2.Distance(m_spaceShip.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }

            return potentialTarget;
        }

        private Vector3 MakeLead(Destructible target)
        {
            if (target.gameObject.TryGetComponent(out SpaceShip targetShip))
            {
                return  targetShip.transform.position + (Vector3) (targetShip.MovingSpeed * m_leadPredictionTime);
            }

            return target.transform.position;
        }

        private AIPointPatrol FindNearestPatrolPoint()
        {
            AIPointPatrol potentialPoint = null;

            float maxDist = float.MaxValue;

            foreach (var point in AIPointPatrol.AllPatrolPoints)
            {
                float dist = Vector2.Distance(m_spaceShip.transform.position, point.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialPoint = point;
                }
            }

            return potentialPoint;
        }

        #region Timers

        private void InitTimers()
        {
            m_randomizeDirectionTimer = new Timer(m_randomSelectMovePointTime);
            m_fireTimer = new Timer(m_shootDelay);
            m_findNewTargetTimer = new Timer(m_findNewTargetTime);
    }

        private void UpdateTimers()
        {
            m_randomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_fireTimer.RemoveTime(Time.deltaTime);
            m_findNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        #endregion
    }
}
