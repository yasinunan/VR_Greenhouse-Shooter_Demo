using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class BugData
{
    

    public string bugName;
    [Tooltip("Harfmul is true, Beneficial is false.")]
    public bool isHarmful;
    public int score;
    public Sprite sprite;
    public GameObject prefab;
    public int movementSpeed;
    public int rotationSpeed;
    public MovementType movementType;
    
}

    public enum MovementType{
        Fly,
        Jump

}

//#if UNITY_EDITOR

//[CustomEditor(typeof(BugData))]
//public class EnemyStatsEditor : Editor
//{
//    // The various categories the editor will display the variables in 
    
//    public enum DisplayCategory
//    {
//        Basic, Combat, Magic
//    }

//    // The enum field that will determine what variables to display in the Inspector
//    public MovementType categoryToDisplay;

//    // The function that makes the custom editor work
//    public override void OnInspectorGUI()
//    {
//        // Display the enum popup in the inspector
//        categoryToDisplay = (MovementType)EditorGUILayout.EnumPopup("Display", categoryToDisplay);

//        // Create a space to separate this enum popup from other variables 
//        EditorGUILayout.Space();

//        // Switch statement to handle what happens for each category
//        switch (categoryToDisplay)
//        {
//            case MovementType.Fly:
//                DisplayFlyInfo();
//                break;

//            case MovementType.Jump:
//                DisplayJumpInfo();
//                break;


//        }
//        serializedObject.ApplyModifiedProperties();
//    }

//    // When the categoryToDisplay enum is at "Basic"
//    void DisplayFlyInfo()
//    {
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("movementSpeed"));
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationSpeed"));
//    }

//    // When the categoryToDisplay enum is at "Combat"
//    void DisplayJumpInfo()
//    {
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("attack"));
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("attackRange"));
//        EditorGUILayout.PropertyField(serializedObject.FindProperty("attackSpeed"));
//    }

   
   
//}
//#endif