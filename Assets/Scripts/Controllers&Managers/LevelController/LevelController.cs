using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public interface ILevelCondition
    {
        bool IsCompleted { get; }
    }

    public class LevelController : SingletonBase<LevelController>
    {
        [SerializeField] private int m_referenceTime;
        public int ReferenceTime => m_referenceTime;

        [SerializeField] private int m_bonusScore;
        public int BonusScore => m_bonusScore;

        [SerializeField] private UnityEvent m_eventLevelCompleted;

        private ILevelCondition[] m_conditions;

        private bool m_isLevelCompleted;
        private float m_levelTime;
        public float LevelTime => m_levelTime;

        private void Start()
        {
            m_conditions = GetComponentsInChildren<ILevelCondition>();
        }

        private void Update()
        {
            if (!m_isLevelCompleted)
            {
                m_levelTime += Time.deltaTime;

                CheckLevelConditions();
            }
        }

        private void CheckLevelConditions()
        {
            if (m_conditions == null || m_conditions.Length == 0) return;

            int numCompleted = 0;

            foreach (var v in m_conditions)
            {
                if (v.IsCompleted)
                    numCompleted++;
            }

            if (numCompleted == m_conditions.Length)
            {
                m_isLevelCompleted = true;
                m_eventLevelCompleted?.Invoke();

                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }

    }
}
