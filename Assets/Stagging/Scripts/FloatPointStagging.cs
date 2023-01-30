using UnityEngine;

namespace HGS.FloatPoint
{
  public class FloatPointStagging : MonoBehaviour
  {
    [SerializeField] FloatPointAsset floatPoint;
    Camera _mainCamera = null;

    private void Start()
    {
      _mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
      var value = UnityEngine.Random.Range(-500, 100);
      var valueString = value.ToString();

      if (value < -400)
      {
        floatPoint.Show(valueString, transform, Color.red, 2);
      }
      else if (value < 0)
      {
        floatPoint.Show(valueString, transform, new Color(0.5f, 0.5f, 0f, 1f), 1);
      }
      else
      {
        floatPoint.Show(valueString, transform, Color.green, 1);
      }
    }
  }
}
