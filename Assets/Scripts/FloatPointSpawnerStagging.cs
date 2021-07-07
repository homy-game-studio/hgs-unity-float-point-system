using UnityEngine;

namespace HGS.FloatPointSystem
{
    public class FloatPointSpawnerStagging : MonoBehaviour
    {
        [SerializeField] float spawnRate = 0.1f;
        Camera _mainCamera = null;
        float _currentSpawnTime = 0;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            _currentSpawnTime += Time.deltaTime;
            if (_currentSpawnTime >= spawnRate)
            {
                _currentSpawnTime = 0;
                Spawn();
            }
        }

        private void Spawn()
        {
            int value = UnityEngine.Random.Range(-500, 100);

            if (value < -400)
            {
                FloatPointService.Instance.Show(value, FloatPointId.CRITICAL, transform.position);
            }
            else if (value < 0)
            {
                FloatPointService.Instance.Show(value, FloatPointId.DAMAGE, transform.position);
            }
            else
            {
                FloatPointService.Instance.Show(value, FloatPointId.HEAL, transform.position);
            }
        }
    }
}
