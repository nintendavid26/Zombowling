using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Block : MonoBehaviour {
    public Block BaseBlock;
   // public Texture tex;
    public int amount=1;
    public ZombieMaker bZMaker;
    public int height = 1;
    public enum Mode {Move, Create, Stretch };
    public Mode currentMode;
    public GameObject Parent;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
