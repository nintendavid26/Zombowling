﻿using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour
{

    public Camera miniMapCamera;

    public float viewSize = 10f;
    public Vector2 positionOnScreen = new Vector2(0f, 0f);
    public Vector2 size = new Vector2(200f, 150f);
    Texture2D miniMapBorder;
    Rect miniMapRectangle;
    Rect miniMapGUIBorder;
    public bool transparent;

    // Use this for initialization
    void Start()
    {
        miniMapRectangle = new Rect(positionOnScreen.x, (Screen.height - positionOnScreen.y) - size.y, size.x, size.y);
        miniMapGUIBorder = new Rect(positionOnScreen.x - 5, positionOnScreen.y - 5, size.x + 10, size.y + 10);
        GameObject miniCam = new GameObject("MiniMapCamera", typeof(Camera));
        miniMapCamera = miniCam.GetComponent<Camera>();
        SetupMinimapCamera();


       // GameObject characterIcon = GameObject.Instantiate(Resources.Load("CharacterIcon") as GameObject) as GameObject;
       // characterIcon.transform.parent = this.transform;

        miniMapBorder = Resources.Load("MinimapBorder") as Texture2D;
    }

    private void SetupMinimapCamera()
    {
        miniMapCamera.transform.parent = this.gameObject.transform;
        miniMapCamera.transform.position = new Vector3(miniMapCamera.transform.parent.position.x, 20f,miniMapCamera.transform.parent.position.z);
        miniMapCamera.transform.Rotate(Vector3.right, 90f);
        miniMapCamera.transform.Rotate(Vector3.forward, 90f);
        miniMapCamera.orthographic = true;
        miniMapCamera.orthographicSize = viewSize;

       // int layerMask = 0;
       // layerMask |= 1 << LayerMask.NameToLayer("MiniMap");
      //  layerMask |= 1 << LayerMask.NameToLayer("Layer1");

      //  miniMapCamera.cullingMask = layerMask;

        //Convert to viewport coordinates (i.e. 0,0 bottom left, 1,1 top right)
        miniMapCamera.rect = new Rect(miniMapRectangle.x / Screen.width, miniMapRectangle.y / Screen.height,
                                      miniMapRectangle.width / Screen.width, miniMapRectangle.height / Screen.height);

    }

    // Update is called once per frame
    void Update()
    {
        //We're simply putting this here so we can see it change live in the demo.
        //Ideally it would go in the setup method above.
        if (transparent)
            miniMapCamera.clearFlags = CameraClearFlags.Depth;
        else
            miniMapCamera.clearFlags = CameraClearFlags.Skybox;
    }

   // void OnGUI()
  //  {
   //     GUI.DrawTexture(miniMapGUIBorder, miniMapBorder);
   // }
}
