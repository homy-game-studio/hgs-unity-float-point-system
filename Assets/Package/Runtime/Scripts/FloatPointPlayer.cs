using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HGS.FloatPoint
{
  public class FloatPointPlayer : MonoBehaviour
  {
    private Transform _target = null;
    private TextMeshPro _textMesh = null;
    private FloatPointAsset _floatPointAsset;
    private Vector3 _offset = Vector3.zero;
    private Vector3 _targetPosition = Vector3.zero;
    private bool _isPlaying;
    private float _timer = 0;
    private Color _startColor;
    private Transform _cameraTransform;

    TextMeshPro TextMesh
    {
      get
      {
        if (!_textMesh)
        {
          _textMesh = gameObject.AddComponent<TextMeshPro>();

          var contentSizeFiter = gameObject.AddComponent<ContentSizeFitter>();
          contentSizeFiter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
          contentSizeFiter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

          LayoutRebuilder.MarkLayoutForRebuild((RectTransform)transform);
        }
        return _textMesh;
      }
    }

    Transform CameraTransform
    {
      get
      {
        if (!_cameraTransform) _cameraTransform = Camera.main.transform;
        return _cameraTransform;
      }
    }

    Vector3 TargetPosition => _target != null ? _target.position : _targetPosition;

    public FloatPointAsset FloatPointAsset
    {
      get => _floatPointAsset;
      set
      {
        _floatPointAsset = value;
        TextMesh.alignment = _floatPointAsset.Alignment;
        TextMesh.spriteAsset = _floatPointAsset.SpriteAsset;
        TextMesh.font = _floatPointAsset.FontAsset;
        TextMesh.sortingOrder = _floatPointAsset.SortingOrder;
        TextMesh.sortingLayerID = _floatPointAsset.SortingLayerID;
      }
    }

    public void Show(string text, Transform target, Color color, int size)
    {
      _target = target;
      Animate(text, color, size);
    }

    public void Show(string text, Vector3 position, Color color, int size)
    {
      _target = null;
      _targetPosition = position;
      Animate(text, color, size);
    }

    private void Animate(string text, Color color, int size)
    {
      TextMesh.text = text;
      TextMesh.color = color;
      TextMesh.fontSize = size;

      gameObject.SetActive(true);
      Play();
    }

    void Update()
    {
      if (!_isPlaying) return;

      UpdatePosition();
      UpdateFading();

      _timer += Time.deltaTime;
      if (_timer >= FloatPointAsset.AnimationDuration) Stop();
    }

    private void UpdateFading()
    {
      var startFadeTime = FloatPointAsset.FadeStart * FloatPointAsset.AnimationDuration;
      var fadeDuration = FloatPointAsset.AnimationDuration - startFadeTime;
      var color = TextMesh.color;

      color.a = 0;
      var fadeLerp = Mathf.Max((_timer - startFadeTime) / fadeDuration, 0);

      TextMesh.color = Color.Lerp(_startColor, color, fadeLerp);
    }

    private void UpdatePosition()
    {
      var startPosition = TargetPosition;
      var endPosition = TargetPosition + _offset;
      var position = Vector3.Lerp(startPosition, endPosition, _timer / FloatPointAsset.AnimationDuration);

      transform.position = position;

      if (FloatPointAsset.Billboard) transform.rotation = CameraTransform.rotation;
    }

    private void Play()
    {
      _isPlaying = true;
      _timer = 0;
      _startColor = TextMesh.color;
      _textMesh.color = Color.clear;

      var duration = FloatPointAsset.AnimationDuration;
      var offsetY = FloatPointAsset.OffsetY;
      var offsetX = FloatPointAsset.OffsetX;

      _offset.x = UnityEngine.Random.Range(-offsetX, offsetX);
      _offset.y = offsetY;
    }

    private void Stop()
    {
      _isPlaying = false;
      gameObject.SetActive(false);
      FloatPointAsset.AddPlayerToReuse(this);
    }
  }
}