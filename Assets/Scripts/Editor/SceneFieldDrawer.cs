using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(property.FindPropertyRelative("sceneName").stringValue);

        EditorGUI.BeginChangeCheck();
        var newScene = EditorGUI.ObjectField(position, oldScene, typeof(SceneAsset), false);

        if (EditorGUI.EndChangeCheck())
        {
            var newPath = AssetDatabase.GetAssetPath(newScene);
            var scenePathProperty = property.FindPropertyRelative("sceneName");
            scenePathProperty.stringValue = newPath;
        }

        EditorGUI.EndProperty();
    }
}
