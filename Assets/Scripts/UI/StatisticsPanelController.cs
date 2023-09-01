using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class StatisticsPanelController : SingletonBase<StatisticsPanelController>
    {
        [SerializeField] private Text m_kills;
        [SerializeField] private Text m_score;
        [SerializeField] private Text m_time;

        protected override void Awake()
        {
            base.Awake();

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            m_kills.text = "Kills: " + OverallStatistics.Instance.AllKills.ToString();
            m_score.text = "Score: " + OverallStatistics.Instance.AllScore.ToString();
            m_time.text = "Time: " + OverallStatistics.Instance.AllTime.ToString();
        }

        public void OnButtonBack()
        {
            gameObject.SetActive(false);
        }

        public void OnButtonReset()
        {
            OverallStatistics.Instance.Reset();

            gameObject.SetActive(false);
        }



    }
}
