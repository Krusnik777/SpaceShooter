using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleArea))]
    public class PowerupSpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop,
            Special
        }

        [SerializeField] private Powerup[] m_powerupPrefabs;
        [SerializeField] private CircleArea m_area;
        [SerializeField] private SpawnMode m_spawnMode;
        [SerializeField] private int m_numSpawns;
        [SerializeField] private float m_respawnTime;
        [SerializeField] private bool m_spawnAtStart;

        private float timer;

        private bool commandSpawn;

        private void Start()
        {
            if (m_spawnMode == SpawnMode.Start || (m_spawnMode == SpawnMode.Loop && m_spawnAtStart))
            {
                SpawnPowerups();
            }

            if (m_spawnMode == SpawnMode.Special)
            {
                for (int i = 0; i < m_numSpawns; i++)
                {
                    SpawnPowerup();
                }
            }

            timer = m_respawnTime;
        }

        private void Update()
        {
            if (m_spawnMode == SpawnMode.Loop)
            {
                timer -= Time.deltaTime;

                if (timer < 0)
                {
                    SpawnPowerups();

                    timer = m_respawnTime;
                }   
            }

            if (m_spawnMode == SpawnMode.Special && commandSpawn == true)
            {
                timer -= Time.deltaTime;

                if (timer < 0)
                {
                    SpawnPowerup();

                    commandSpawn = false;

                    timer = m_respawnTime;
                }
            }
        }

        private void SpawnPowerups()
        {
            for (int i = 0; i < m_numSpawns; i++)
            {
                int index = Random.Range(0, m_powerupPrefabs.Length);

                GameObject powerup = Instantiate(m_powerupPrefabs[index].gameObject);

                powerup.transform.position = m_area.GetRandomInsideZone();
            }
        }

        private void SpawnPowerup()
        {
            int index = Random.Range(0, m_powerupPrefabs.Length);

            GameObject powerup = Instantiate(m_powerupPrefabs[index].gameObject);

            powerup.transform.position = m_area.GetRandomInsideZone();

            powerup.GetComponent<Powerup>().MarkTheSpawner(this);

        }

        public void OnPowerupPicked()
        {
            timer = m_respawnTime;

            commandSpawn = true;
        }

    }
}
