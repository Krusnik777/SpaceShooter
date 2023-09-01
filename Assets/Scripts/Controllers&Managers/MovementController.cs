using UnityEngine;

namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            VirtualJoystick
        }

        [SerializeField] private SpaceShip m_targetShip;
        public void SetTargetShip(SpaceShip ship) => m_targetShip = ship;
        [SerializeField] private VirtualJoystick m_virtualJoystick;

        [SerializeField] private ControlMode m_controlMode;

        [SerializeField] private PointerClickHold m_mobileFirePrimary;
        [SerializeField] private PointerClickHold m_mobileFireSecondary;

        private void Start()
        {
            if (Application.isMobilePlatform)
            {
                m_controlMode = ControlMode.VirtualJoystick;
                m_virtualJoystick.gameObject.SetActive(true);
                m_mobileFirePrimary.gameObject.SetActive(true);
                m_mobileFireSecondary.gameObject.SetActive(true);
            }
            else
            {
                m_controlMode = ControlMode.Keyboard;
                m_virtualJoystick.gameObject.SetActive(false);
                m_mobileFirePrimary.gameObject.SetActive(false);
                m_mobileFireSecondary.gameObject.SetActive(false);
            }
            #if UNITY_EDITOR
            UpdateControlMode();
            #endif

        }

        private void Update()
        {
            if (m_targetShip == null) return;

            #if UNITY_EDITOR
            UpdateControlMode();
            #endif

            if (m_controlMode == ControlMode.Keyboard)
                ControlKeyboard();

            if (m_controlMode == ControlMode.VirtualJoystick)
                ControlVirtualJoystick();
        }

        private void ControlVirtualJoystick()
        {
            var dir = m_virtualJoystick.Value;

            m_targetShip.ThrustControl = dir.y;
            m_targetShip.TorqueControl = -dir.x;

            if (m_mobileFirePrimary.IsHold == true)
                m_targetShip.Fire(TurretMode.Primary);

            if (m_mobileFireSecondary.IsHold == true)
                m_targetShip.Fire(TurretMode.Secondary);

            /*
            // OBSOLETE
            Vector3 dir = m_virtualJoystick.Value;

            var dot = Vector2.Dot(dir, m_targetShip.transform.up);
            var dot2 = Vector2.Dot(dir, m_targetShip.transform.right);

            m_targetShip.ThrustControl = Mathf.Max(0, dot);
            m_targetShip.TorqueControl = -dot2;
            */

        }

        private void ControlKeyboard()
        {
            float thrust = 0;
            float torque = 0;

            if (Input.GetKey(KeyCode.UpArrow))
                thrust = 1.0f;

            if (Input.GetKey(KeyCode.DownArrow))
                thrust = -1.0f;

            if (Input.GetKey(KeyCode.LeftArrow))
                torque = 1.0f;

            if (Input.GetKey(KeyCode.RightArrow))
                torque = -1.0f;

            if (Input.GetKey(KeyCode.Space))
                m_targetShip.Fire(TurretMode.Primary);

            if (Input.GetKey(KeyCode.X))
                m_targetShip.Fire(TurretMode.Secondary);

            m_targetShip.ThrustControl = thrust;
            m_targetShip.TorqueControl = torque;
        }

        #if UNITY_EDITOR

        private void UpdateControlMode()
        {
            if (m_controlMode == ControlMode.Keyboard)
            {
                m_virtualJoystick.gameObject.SetActive(false);
                m_mobileFirePrimary.gameObject.SetActive(false);
                m_mobileFireSecondary.gameObject.SetActive(false);
            }
            else
            {
                m_virtualJoystick.gameObject.SetActive(true);
                m_mobileFirePrimary.gameObject.SetActive(true);
                m_mobileFireSecondary.gameObject.SetActive(true);
            }
        }
        #endif
    }
}
