using System.Collections.Generic;
using UnityEngine;

namespace HGS.FloatPointSystem
{
    [System.Serializable]
    public class FloatPointEffect
    {
        public string effectId = null;
        public GameObject prefab;
    }

    public class FloatPointSettings : ScriptableObject
    {
        [Header("Prefabs")]
        [SerializeField] GameObject canvasPrefab = null;       
        
        [Header("Presets")]
        [SerializeField] List<FloatPointEffect> effects= null;

        [Header("Animation")]
        [SerializeField] float animationDuration = 1;
        [SerializeField] float offsetY = 1;
        [SerializeField] float offsetX = 2;

        public FloatPointEffect GetEffect(string effectId)
        {
            return effects.Find(effect => effect.effectId == effectId);
        }

        public GameObject CanvasPrefab { get => canvasPrefab; }
        public float AnimationDuration { get => animationDuration; }
        public float OffsetY { get => offsetY; }
        public float OffsetX { get => offsetX; }
    }
}