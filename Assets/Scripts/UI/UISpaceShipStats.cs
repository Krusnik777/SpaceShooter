using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UISpaceShipStats : MonoBehaviour
    {
        [SerializeField] private Text m_healthCount;
        [SerializeField] private Text m_energyTextCount;
        [SerializeField] private Text m_ammoTextCount;
        [SerializeField] private Text m_livesCount;

        private SpaceShip activeShip;

        private void Update()
        {
            GetActiveShipStats();
        }

        private void GetActiveShipStats()
        {
            if (Player.Instance == null || Player.Instance.ActiveShip == null) return;

            activeShip = Player.Instance.ActiveShip;
            m_energyTextCount.text = ((int)activeShip.PrimaryEnergy).ToString();
            m_ammoTextCount.text = activeShip.SecondaryAmmo.ToString();
            m_healthCount.text = activeShip.HitPoints.ToString();
            m_livesCount.text = Player.Instance.Lives.ToString();
        }
    }
}
