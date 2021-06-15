using UnityEngine;

namespace HGS.FloatPointSystem
{
    public enum FloatPointId
    {
        DAMAGE,
        HEAL,
        CRITICAL
    }

    public class FloatPointStagging : MonoBehaviour
    {
        Camera m_mainCamera = null;

        private void Start()
        {
            m_mainCamera = Camera.main;    
        }

        private void OnMouseDown()
        {
            int value = Random.Range(-500, 100);

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
