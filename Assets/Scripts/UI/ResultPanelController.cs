using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_kills;
        [SerializeField] private Text m_score;
        [SerializeField] private Text m_time;

        [SerializeField] private Text m_result;

        [SerializeField] private Text m_buttonNextText;

        private bool m_success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResults, bool state)
        {
            gameObject.SetActive(true);

            m_success = state;

            m_result.text = m_success ? "Win" : "Lose";
            m_buttonNextText.text = m_success ? "Next" : "Restart";

            m_kills.text = "Kills: " + levelResults.NumKills.ToString();
            m_score.text = "Score: " + levelResults.Score.ToString();
            m_time.text = "Time: " + levelResults.Time.ToString();

            Time.timeScale = 0f;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1f;

            if (m_success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel();
            }
        }
    }
}
