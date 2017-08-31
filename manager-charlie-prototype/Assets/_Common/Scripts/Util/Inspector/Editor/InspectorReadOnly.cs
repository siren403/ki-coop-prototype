using UnityEngine;
using UnityEditor;

namespace Util.Inspector
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class InspectorReadOnly : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.EndDisabledGroup();
        }

    }
}