using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ScoreStats : MonoBehaviour
    {
        [SerializeField] private Text m_scoreText;

        private int m_lastScore;

        private void Update()
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            if (Player.Instance != null)
            {
                int currentScore = Player.Instance.Score;

                if (currentScore != m_lastScore)
                {
                    m_lastScore = currentScore;

                    m_scoreText.text = m_lastScore.ToString();
                }
            }
        }
    }
}
