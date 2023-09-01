using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UIMissionTask : MonoBehaviour
    {
        [SerializeField] private Text m_taskText;

        [SerializeField] private LevelConditionNumberKills m_levelConditionNumberKills;
        [SerializeField] private LevelConditionScore m_levelConditionScore;
        [SerializeField] private LevelConditionsTargetKills m_levelConditionTargetKills;
        [SerializeField] private LevelConditionTime m_levelConditionTime;

        private void Update()
        {
            GetCurrentMission();
        }

        private void GetCurrentMission()
        {
            if (m_levelConditionNumberKills != null)
            {
                m_taskText.text = "Defeat the enemies: " + (m_levelConditionNumberKills.TargetSpecificTeam ? m_levelConditionNumberKills.KilledTargets.ToString() : Player.Instance.NumKills.ToString()) + "/" + m_levelConditionNumberKills.NumKills.ToString(); 
            }

            if (m_levelConditionScore != null)
            {
                m_taskText.text = "Get score: " + Player.Instance.Score.ToString() + "/" + m_levelConditionScore.TargetScore.ToString();
            }

            if (m_levelConditionTargetKills != null)
            {
                m_taskText.text = "Defeat the targets: " + m_levelConditionTargetKills.NumTargets.ToString();
            }

            if (m_levelConditionTime != null)
            {
                m_taskText.text = "Finish the mission within time: " + ((int) LevelController.Instance.LevelTime).ToString() + "/" + ((int) m_levelConditionTime.LimitTime).ToString();
            }
        }


    }
}
