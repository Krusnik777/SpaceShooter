using UnityEngine;

namespace SpaceShooter
{
    public class MainMenuController : SingletonBase<MainMenuController>
    {
        [SerializeField] private SpaceShip m_defaultSpaceShip;
        [SerializeField] private GameObject m_episodeSelection;
        [SerializeField] private GameObject m_shipSelection;
        [SerializeField] private GameObject m_statistics;

        private void Start()
        {
            LevelSequenceController.PlayerShip = m_defaultSpaceShip;
        }

        public void OnButtonStartNew()
        {
            m_episodeSelection.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnSelectShip()
        {
            m_shipSelection.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnButtonStatistics()
        {
            m_statistics.SetActive(true);
        }

        public void OnButtonExit()
        {
            Application.Quit();
        }
    }
}
