using UnityEditor;
using UnityEngine;

namespace HGS.FloatPointSystem
{
    public class CustomContextMenu
    {
        private static void CreteFolder(string folderPath, string folderName)
        {
            if (!AssetDatabase.IsValidFolder($"{folderPath}/{folderName}")) AssetDatabase.CreateFolder(folderPath, folderName);
        }

        [MenuItem("HGS/Float Point Settings", false, 0)]
        static void Init()
        {
            FloatPointSettings current = AssetDatabase.LoadAssetAtPath<FloatPointSettings>("Assets/HGSGenerated/Resources/Settings/FloatPointSettings.asset");
            if (current == null)
            {
                // Inicializa os diretórios
                CreteFolder("Assets", "HGSGenerated");
                CreteFolder("Assets/HGSGenerated", "Resources");
                CreteFolder("Assets/HGSGenerated/Resources", "Settings");

                current = ScriptableObject.CreateInstance<FloatPointSettings>();
                AssetDatabase.CreateAsset(current, "Assets/HGSGenerated/Resources/Settings/FloatPointSettings.asset");
                AssetDatabase.SaveAssets();
            }

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = current;
        }
    }
}
