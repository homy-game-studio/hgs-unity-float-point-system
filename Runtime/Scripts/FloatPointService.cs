using UnityEngine;
using HGS.SharedUtils;
using DG.Tweening;
using System;
using TMPro;

namespace HGS.FloatPointSystem
{
    public class FloatPointService : SingletonBase<FloatPointService>
    {
        FloatPointSettings m_settings = null;
        Transform m_currentCanvasTransform = null;

        protected FloatPointSettings Settings {
            get {
                if (m_settings == null) m_settings = Resources.Load<FloatPointSettings>("Settings/FloatPointSettings");
                return m_settings;
            }
        }

        private Transform SpawnCanvas(Vector3 position)
        {
            if (m_currentCanvasTransform == null)
            {
                GameObject canvasPrefab = Settings.CanvasPrefab;
                GameObject go = PooledObject.Instance.InstantiatePooled(
                    canvasPrefab,
                    position,
                    Quaternion.identity
                    );

                m_currentCanvasTransform = go.transform;
            }
            return m_currentCanvasTransform;
        }

        private void SpawnTextAndAnimate(string message, FloatPointEffect effect, Vector3 position)
        {
            Transform canvasT = SpawnCanvas(position);

            // Spawna o objeto reutilizando da pool
            GameObject go = PooledObject.Instance.InstantiatePooled(effect.prefab, position, Quaternion.identity, canvasT);

            RectTransform rect = go.GetComponent<RectTransform>();
            TMP_Text textComponent = go.GetComponent<TMP_Text>();

            // Adiciona a mensagem
            textComponent.text = message;

            rect.localScale = Vector3.one;
            Color color = textComponent.color;
            color.a = 1;
            textComponent.color = color;

            float animationDuration = Settings.AnimationDuration;
            float animationOffsetY = Settings.OffsetY;
            float animationOffsetX = Settings.OffsetX;

            float animationX = UnityEngine.Random.Range(-animationOffsetX, animationOffsetX);

            Vector2 startPosition = rect.anchoredPosition;
            startPosition.x += animationX;
            startPosition.y += animationOffsetY;

            Vector2 endPosition = rect.anchoredPosition;
            endPosition.x = startPosition.x;
            endPosition.y += animationOffsetY * 0.7f;

            Sequence fadeSequence = DOTween.Sequence()
                .Append(rect.DOAnchorPos(startPosition, animationDuration * 0.6f))
                .Append(rect.DOScale(Vector2.zero, animationDuration * 0.4f))
                .Join(rect.DOAnchorPos(endPosition, animationDuration * 0.4f))
                .Join(textComponent.DOFade(0, animationDuration * 0.4f))
                .OnComplete(() =>
                {
                    go.SetActive(false);
                });
        }

        // <summary>
        /// Cria um prefab de FloatPoint 
        /// </summary>
        /// <param name="message">Mensagem que será passada para o FloatPoint</param>
        /// <param name="color">Cor que será passada para o FloatPoint</param>
        public virtual void Show(string message, string effectId, Vector3 position)
        {
            Vector3 convertedPosition = Camera.main.WorldToScreenPoint(position);
            FloatPointEffect effect = Settings.GetEffect(effectId);
            SpawnTextAndAnimate(message, effect, convertedPosition);
        }

        // <summary>
        /// Cria um prefab de FloatPoint 
        /// </summary>
        /// <param name="message">Mensagem que será passada para o FloatPoint</param>
        /// <param name="color">Cor que será passada para o FloatPoint</param>
        public virtual void Show<T>(string message, T effectId, Vector3 position) where T : Enum
        {
            Show(message, effectId.ToString(), position);
        }

        // <summary>
        /// Cria um prefab de FloatPoint 
        /// </summary>
        /// <param name="message">Mensagem que será passada para o FloatPoint</param>
        /// <param name="color">Cor que será passada para o FloatPoint</param>
        public virtual void Show<T>(float value, T effectId, Vector3 position) where T : Enum
        {
            Show(value.ToString(), effectId.ToString(), position);
        }
    }
}