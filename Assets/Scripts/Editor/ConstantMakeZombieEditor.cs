using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ConstantZombieMaking))]
[ExecuteInEditMode]
public class ConstantMakeZombieEditor : Editor
{
    Zombie obj;
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        Event e = Event.current;
        ConstantZombieMaking myBlock = (ConstantZombieMaking)target;


    }
}
