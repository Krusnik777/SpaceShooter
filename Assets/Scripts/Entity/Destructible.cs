using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Destructible object on scene. Has HitPoints
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties

        /// <summary>
        /// Object ignores damage or not
        /// </summary>
        [SerializeField] protected bool m_indestrutible;
        public bool IsIndestructible => m_indestrutible;

        /// <summary>
        /// Start value for HitPoints
        /// </summary>
        [SerializeField] protected int m_hitPoints;
        public int MaxHitPoints => m_hitPoints;

        /// <summary>
        /// Current value of HitPoints
        /// </summary>
        private int m_currentHitPoins;
        public int HitPoints => m_currentHitPoins;

        /// <summary>
        /// Event for object destruction
        /// </summary>
        [SerializeField] protected UnityEvent m_eventOnDeath;
        public UnityEvent EventOnDeath => m_eventOnDeath;

        [SerializeField] private SpriteFillBar m_spriteFillBar;

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_teamId;
        public int TeamId => m_teamId;

        #endregion

        #region UnityEvents

        protected virtual void Start()
        {
            m_currentHitPoins = m_hitPoints;
            if (m_spriteFillBar != null)
            {
                m_spriteFillBar.SetFillAmountStep(m_hitPoints);
                m_spriteFillBar.gameObject.SetActive(false);
            }
        }

        #endregion

        #region PublicAPI
        /// <summary>
        /// Applying Damage to object
        /// </summary>
        /// <param name="damage">Damage to object</param>
        public void ApplyDamage(object sender, int damage)
        {
            if (m_indestrutible) return;

            if (TryGetComponent(out Boss boss))
            {
                boss.ApplyBreakDamage(sender, damage);

                if (boss.InBreakState) damage = (int) (damage * boss.DamageBooster);
            }

            m_currentHitPoins -= damage;

            if (m_spriteFillBar != null)
            {
                m_spriteFillBar.gameObject.SetActive(true);

                m_spriteFillBar.EmptyBar(damage);
            }

            if (m_currentHitPoins <= 0)
            {
                if (Player.Instance.ActiveShip != null && m_teamId != Player.Instance.ActiveShip.TeamId)
                {
                    if (sender is Projectile projectile)
                    {
                        if (projectile.ByPlayer)
                        {
                            Player.Instance.AddKill();
                            CheckSpecificKillsCondition();
                        }
                    }

                    if (sender is ImpactEffect impactEffect)
                    {
                        if (impactEffect.ByPlayer)
                        {
                            Player.Instance.AddKill();
                            CheckSpecificKillsCondition();
                        }
                    }
                }
                OnDeath();
            }
        }

        #endregion

        /// <summary>
        /// Overdetermined event for object destruction, when HitPoints equal or less than 0
        /// </summary>
        protected virtual void OnDeath()
        {
            m_eventOnDeath?.Invoke();

            if (LevelController.Instance.GetComponentInChildren<LevelConditionNumberKills>() != null 
                && LevelController.Instance.GetComponentInChildren<LevelConditionNumberKills>().OnlyByPlayer == false) 
                CheckSpecificKillsCondition();

            if (LevelController.Instance.GetComponentInChildren<LevelConditionsTargetKills>()!= null)
            {
                var targetKillsCondition = LevelController.Instance.GetComponentInChildren<LevelConditionsTargetKills>();
                foreach (var v in targetKillsCondition.Targets)
                {
                    if (v == this)
                    {
                        targetKillsCondition.RemoveTarget();
                    }
                }
            }

            Destroy(gameObject);
        }

        private void CheckSpecificKillsCondition()
        {
            var specificKillsCondition = LevelController.Instance.GetComponentInChildren<LevelConditionNumberKills>();
            if (specificKillsCondition != null && specificKillsCondition.TargetSpecificTeam)
            {
                if (m_teamId == specificKillsCondition.TargetedTeamId)
                {
                    specificKillsCondition.AddTargetKills();
                }
            }
        }

        private static HashSet<Destructible> m_allDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_allDestructibles;

        protected virtual void OnEnable()
        {
            if (m_allDestructibles == null)
                m_allDestructibles = new HashSet<Destructible>();

            m_allDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_allDestructibles.Remove(this);
        }

        public void SetTeam(int teamId)
        {
            m_teamId = teamId;
        }

        #region Score
        [SerializeField] private int m_scoreValue;
        public int ScoreValue => m_scoreValue;

        #endregion
    }
}
