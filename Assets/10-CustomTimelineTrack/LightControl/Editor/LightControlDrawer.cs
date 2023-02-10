using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

[CustomPropertyDrawer(typeof(LightControlBehaviour))]
public class LightControlDrawer : PropertyDrawer
{
    public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
    {
        int fieldCount = 6;
        return fieldCount * EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty colorProp = property.FindPropertyRelative("color");
        SerializedProperty intensityProp = property.FindPropertyRelative("intensity");
        SerializedProperty bounceIntensityProp = property.FindPropertyRelative("bounceIntensity");
        SerializedProperty rangeProp = property.FindPropertyRelative("range");
        SerializedProperty innerSpotAngleProp = property.FindPropertyRelative("innerSpotAngle");
        SerializedProperty spotAngleProp = property.FindPropertyRelative("spotAngle");

        Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(singleFieldRect, colorProp);
        
        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, intensityProp);
        
        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, bounceIntensityProp);
        
        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect, rangeProp);
        
        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect,innerSpotAngleProp);
        
        singleFieldRect.y += EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(singleFieldRect,spotAngleProp);
    }
}
