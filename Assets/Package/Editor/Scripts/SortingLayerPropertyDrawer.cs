using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HGS.FloatPoint.EditorExtensions
{
  [CustomPropertyDrawer(typeof(SortingLayerAttribute))]
  public class SortingLayerAttributePropertyDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

      var layerNames = SortingLayer.layers
       .Select((item) => item.name)
       .ToArray();

      var index = ArrayUtility.IndexOf(layerNames, SortingLayer.IDToName(property.intValue));

      var selected = EditorGUI.Popup(position, index, layerNames);

      property.intValue = SortingLayer.NameToID(layerNames[selected]);
    }
  }
}