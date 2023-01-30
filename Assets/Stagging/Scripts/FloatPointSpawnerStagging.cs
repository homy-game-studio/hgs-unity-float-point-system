using UnityEngine;

namespace HGS.FloatPoint
{
  public class FloatPointSpawnerStagging : MonoBehaviour
  {
    [SerializeField] float degressBySecond = 45;
    [SerializeField] float radius = 2f;
    [SerializeField] bool canFollow = false;
    [SerializeField] Color criticalColor;
    [SerializeField] Color healColor;
    [SerializeField] Color damageColor;
    [SerializeField] FloatPointAsset floatPoint;
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

      transform.RotateAround(Vector3.zero, Vector3.up * radius, degressBySecond * Time.deltaTime);
    }

    private void Show(int sprite, int value, Color color, int size)
    {
      var valueString = $"<sprite={sprite} tint=1> {Mathf.Abs(value)}";

      if (canFollow)
      {
        floatPoint.Show(valueString, transform, color, size);
      }
      else
      {
        floatPoint.Show(valueString, transform.position, color, size);
      }
    }

    private void Spawn()
    {
      var value = UnityEngine.Random.Range(-200, 100);

      if (value < -100)
      {
        Show(1, value, criticalColor, 4);
      }
      else if (value < 0)
      {
        Show(0, value, damageColor, 3);
      }
      else
      {
        Show(2, value, Color.green, 3);
      }
    }
  }
}
