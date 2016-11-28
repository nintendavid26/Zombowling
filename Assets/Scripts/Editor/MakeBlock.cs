using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Block))]
[CanEditMultipleObjects]
[ExecuteInEditMode]
public class MakeBlock : Editor
{
    List<GameObject> Blocks;
    public static int amount;
    GameObject prevBlock;
    bool LastActionWasCreate = false;
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        Event e = Event.current;
        Block myBlock = (Block)target;

        //int amountToMake = EditorGUILayout.IntField("Amount Of Blocks:", amount);
        Rect r = EditorGUILayout.BeginHorizontal();
        string key = "";
        /*switch (e.type)
        {
            case EventType.keyDown:
                {
                    if (Event.current.keyCode == (KeyCode.A))
                    {
                        GameObject newBlock = Instantiate(myBlock.BaseBlock.gameObject, new Vector3(myBlock.gameObject.transform.position.x - myBlock.gameObject.transform.localScale.x, myBlock.gameObject.transform.position.y, myBlock.gameObject.transform.position.z), Quaternion.identity) as GameObject;
                        Selection.activeGameObject = newBlock;
                        newBlock.name = myBlock.name;
                        LastActionWasCreate = true;
                    }

                    else if (Event.current.keyCode == (KeyCode.W))
                    {
                        key = "w";
                    }
                    else if (Event.current.keyCode == (KeyCode.S))
                    {
                        key = "s";
                    }
                    else if (Event.current.keyCode == (KeyCode.D))
                    {
                        key = "d";
                    }
                    break;
                }
        }*/
        if (GUILayout.Button("<"))
        {
            if (myBlock.currentMode == Block.Mode.Create)
            {
                MakeMyBlock("W", myBlock, myBlock.amount,myBlock.height);
            }
            else if (myBlock.currentMode == Block.Mode.Move)
            {
                MoveMyBlock("W", myBlock, myBlock.amount);
            }
            // Blocks.Add(newBlock);

        }
        if (GUILayout.Button("V"))
        {
            if (myBlock.currentMode == Block.Mode.Create)
            {
                MakeMyBlock("S", myBlock, myBlock.amount,myBlock.height);
            }
            else if (myBlock.currentMode == Block.Mode.Move)
            {
                MoveMyBlock("S", myBlock, myBlock.amount);
            }
            //  Blocks.Add(newBlock);
        }
        if (GUILayout.Button("^"))
        {
            if (myBlock.currentMode == Block.Mode.Create)
            {
                MakeMyBlock("N", myBlock, myBlock.amount,myBlock.height);
            }
            else if (myBlock.currentMode == Block.Mode.Move)
            {
                MoveMyBlock("N", myBlock, myBlock.amount);
            }
        }
        if (GUILayout.Button(">"))
        {
            if (myBlock.currentMode == Block.Mode.Create)
            {
                MakeMyBlock("E", myBlock, myBlock.amount,myBlock.height);
            }
            else if (myBlock.currentMode == Block.Mode.Move)
            {
                MoveMyBlock("E", myBlock, myBlock.amount);
            }
        }
        if (GUILayout.Button("U"))
        {
            if (myBlock.currentMode == Block.Mode.Create)
            {
                MakeMyBlock("U", myBlock, myBlock.amount, myBlock.height);
            }
            else if (myBlock.currentMode == Block.Mode.Move)
            {
                MoveMyBlock("U", myBlock, myBlock.amount);
            }
        }
        EditorGUILayout.EndHorizontal();

        Rect r2 = EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Undo"))
        {
            /* if (LastActionWasCreate)
             {
                 Selection.activeGameObject = Blocks[Blocks.Capacity - 1].gameObject;
                 Destroy(Blocks[Blocks.Capacity - 1].gameObject);
                 Blocks.RemoveAt(Blocks.Capacity - 1);
             }
             else
             {

             }
             */
        }
        if (GUILayout.Button("Redo"))
        {

        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Delete"))
        {
            //  Blocks.Remove(myBlock.gameObject);
            DestroyImmediate(myBlock.gameObject);
            Selection.activeGameObject = prevBlock;
        }
        if (GUILayout.Button("Add Spawn Point on Top"))
        {
            if (ZombieMaker.maker == null) { ZombieMaker.maker = GameObject.Find("Zombie Maker").GetComponent<ZombieMaker>(); }
            ZombieMaker.maker.spawnPoints.Add(new Vector3(myBlock.transform.position.x, myBlock.transform.position.y + 5, myBlock.transform.position.z));
        }
        //public Texture tex;
        // Texture tex;
        //myBlock.gameObject.GetComponent<Renderer>().material.mainTexture= EditorGUILayout.ObjectField("Label:", target.tex, typeof(Texture), true); ;
    }

    public void MakeMyBlock(string direction, Block selectedBlock, int howMany,int Height)
    {
        GameObject Parent = GameObject.Find("Block Container");
        for (int i = 0; i < howMany; i++)
        {
            GameObject newBlock;
            prevBlock = selectedBlock.gameObject;
            Transform t = selectedBlock.gameObject.transform;
            float height =t.position.y;
            for (int j = 0; j < Height; j++)
            {
               
                switch (direction)
                {
                 case "W":
                     newBlock = Instantiate(selectedBlock.BaseBlock.gameObject,
                         new Vector3(t.position.x - t.localScale.x, j*t.localScale.y, t.position.z), Quaternion.identity) as GameObject;
                     break;
                 case "N":
                     newBlock = Instantiate(selectedBlock.BaseBlock.gameObject, new Vector3(t.position.x,j*t.localScale.y,t.position.z  + t.localScale.z), Quaternion.identity) as GameObject;
                     break;
                 case "S":
                     newBlock = Instantiate(selectedBlock.BaseBlock.gameObject, new Vector3(t.position.x, j*t.localScale.y,t.position.z  -    t.localScale.z), Quaternion.identity) as GameObject;
                     break;
                 case "E":
                     newBlock = Instantiate(selectedBlock.BaseBlock.gameObject, new Vector3(t.position.x + t.localScale.x, j*t.localScale.y,  t.position.z), Quaternion.identity) as GameObject;
                     break;
                 case "U":
                     newBlock = Instantiate(selectedBlock.BaseBlock.gameObject, new Vector3(t.position.x, t.position.y + t.localScale.y,  t.position.z),    Quaternion.identity) as GameObject;
                     break;
                 default:
                     newBlock = Instantiate(selectedBlock.BaseBlock.gameObject, new Vector3(t.position.x + t.localScale.x, t.position.y,  t.position.z),    Quaternion.identity) as GameObject;
                     break;
                }

                newBlock.transform.parent = Parent.transform;
                Selection.activeGameObject = newBlock;
                newBlock.name = selectedBlock.name;
                LastActionWasCreate = true;
                selectedBlock = newBlock.GetComponent<Block>();
                selectedBlock.amount = 1;
                selectedBlock.tag = "Block";
            }
        }
    }
    public void MoveMyBlock(string direction, Block selectedBlock, int howMany)
    {
        switch (direction)
        {
            case "E": selectedBlock.transform.position = new Vector3(selectedBlock.transform.position.x + howMany, selectedBlock.transform.position.y, selectedBlock.transform.position.z); break;
            case "N": selectedBlock.transform.position = new Vector3(selectedBlock.transform.position.x, selectedBlock.transform.position.y + howMany, selectedBlock.transform.position.z); break;
            case "S": selectedBlock.transform.position = new Vector3(selectedBlock.transform.position.x, selectedBlock.transform.position.y, selectedBlock.transform.position.z - howMany); break;
            case "W": selectedBlock.transform.position = new Vector3(selectedBlock.transform.position.x - howMany, selectedBlock.transform.position.y, selectedBlock.transform.position.z); break;
            case "U": selectedBlock.transform.position = new Vector3(selectedBlock.transform.position.x, selectedBlock.transform.position.y + howMany, selectedBlock.transform.position.z); break;
        }
    }
    /* public void makeSquare(int x, int y, Block selectedBlock) {
         string dir="E";
         for () {
             MakeMyBlock(dir,selectedBlock,x);
         }*/
}