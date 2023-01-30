using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace HGS.FloatPoint
{
  [CreateAssetMenu(menuName = "HGS/FloatPoint/Asset", fileName = "FloatPointAsset")]
  public class FloatPointAsset : ScriptableObject
  {
    [Header("Animation")]
    [SerializeField, Range(0f, 1f)] float fadeStart = 0.5f;
    [SerializeField] float animationDuration = 1;
    [SerializeField] float offsetY = 1;
    [SerializeField] float offsetX = 2;
    [SerializeField] bool billboard = true;

    public float FadeStart => fadeStart;
    public float AnimationDuration => animationDuration;
    public float OffsetY => offsetY;
    public float OffsetX => offsetX;
    public bool Billboard => billboard;

    [Header("TextMesh")]
    [SerializeField] Color color;
    [SerializeField] TextAlignmentOptions alignment;
    [SerializeField, SortingLayerAttribute] int sortingLayer;
    [SerializeField] int sortingOrder;
    [SerializeField] TMP_FontAsset fontAsset;
    [SerializeField] TMP_SpriteAsset spriteAsset;
    [SerializeField] int size = 12;

    public TextAlignmentOptions Alignment => alignment;
    public TMP_FontAsset FontAsset => fontAsset;
    public TMP_SpriteAsset SpriteAsset => spriteAsset;
    public int SortingOrder => sortingOrder;
    public int SortingLayerID => sortingLayer;

    Queue<FloatPointPlayer> _players = new Queue<FloatPointPlayer>();

    private static GameObject _root = null;

    GameObject Root
    {
      get
      {
        if (_root == null) _root = new GameObject("FloatPoint");
        return _root;
      }
    }

    private FloatPointPlayer ReuseOrSpawn()
    {
      if (_players.Count > 0)
      {
        return _players.Dequeue();
      }

      var playerGoHash = $"FloatPointPlayer - {System.Guid.NewGuid().ToString()}";
      var playerGo = new GameObject(playerGoHash);
      playerGo.transform.SetParent(Root.transform);

      var player = playerGo.AddComponent<FloatPointPlayer>();
      player.FloatPointAsset = this;

      return player;
    }

    /// <summary>
    /// Enqueue player to reuse in shared pool
    /// </summary>
    public void AddPlayerToReuse(FloatPointPlayer player)
    {
      _players.Enqueue(player);
    }

    public virtual void Show(string text, Transform target, Color color, int size)
    {
      var player = ReuseOrSpawn();
      player.Show(text, target, color, size);
    }

    public virtual void Show(string text, Transform target)
    {
      Show(text, target, color, size);
    }

    public virtual void Show(string text, Vector3 position, Color color, int size)
    {
      var player = ReuseOrSpawn();
      player.Show(text, position, color, size);
    }

    public virtual void Show(string text, Vector3 position)
    {
      Show(text, position, color, size);
    }
  }
}