using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName ="Zombie/HeadData")]
public class StandardZombieHeadAssets : ScriptableObject {

    public Texture[] tex;
    public Material[] mat;
    public Renderer[] ren;
    public GameObject Explosion;
}
