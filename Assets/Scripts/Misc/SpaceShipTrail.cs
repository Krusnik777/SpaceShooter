using UnityEngine;

namespace SpaceShooter
{
    public class SpaceShipTrail : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_spaceShip;
        [SerializeField] private TrailRenderer m_forwardEngine;
        [SerializeField] private TrailRenderer m_leftBackwardEngine;
        [SerializeField] private TrailRenderer m_rightBackwardEngine;
        [SerializeField] private TrailRenderer m_leftRotationEngine;
        [SerializeField] private TrailRenderer m_rightRotationEngine;
        [SerializeField] private float m_disappearTime;

        private float timer;

        private void Start()
        {
            SetAllEnginesEmitting(false);
        }

        private void SetAllEnginesEmitting(bool condition)
        {
            m_forwardEngine.emitting = condition;
            m_leftBackwardEngine.emitting = condition;
            m_rightBackwardEngine.emitting = condition;
            m_leftRotationEngine.emitting = condition;
            m_rightRotationEngine.emitting = condition;
        }

        private void SetBackwardEnginesEmitting(bool condition)
        {
            m_leftBackwardEngine.emitting = condition;
            m_rightBackwardEngine.emitting = condition;
        }

        private void SetRotationEnginesEmitting(bool condition)
        {
            m_leftRotationEngine.emitting = condition;
            m_rightRotationEngine.emitting = condition;
        }

        private void Update()
        {
            if (m_spaceShip.ThrustControl == 0 && m_spaceShip.TorqueControl == 0)
            {
                timer += Time.deltaTime;
                if (timer >= m_disappearTime) SetAllEnginesEmitting(false);
            }
            else
            {
                timer = 0;
            }

            if (m_spaceShip.ThrustControl > 0)
            {
                m_forwardEngine.emitting = true;
                SetBackwardEnginesEmitting(false);
            }

            if (m_spaceShip.ThrustControl < 0)
            {
                m_forwardEngine.emitting = false;
                SetBackwardEnginesEmitting(true);
            }

            if (m_spaceShip.TorqueControl != 0)
            {
                SetRotationEnginesEmitting(true);
            }
            else
            {
                SetRotationEnginesEmitting(false);
            }
        }

    }
}
