using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Elevator))]
[CanEditMultipleObjects]
[ExecuteInEditMode]
public class CheckPoints : Editor {

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        Event e = Event.current;
        Elevator myBlock = (Elevator)target;

        //int amountToMake = EditorGUILayout.IntField("Amount Of Blocks:", amount);
        // Rect r = EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Checkpoint"))
        {
            myBlock.AddPoint(myBlock.transform.position);
            // Blocks.Add(newBlock);

        }
    }


    }
