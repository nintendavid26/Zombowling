using UnityEngine;
using System.Collections;
using System.Linq;

public class DefendTheBase : Level {

    public Base Base;
    public Transform EndCamera;
    public GameObject Explosion, Fire;
    public Vector3 ExplosionPoint;

    public void Start()
    {
        Base = (Base)FindObjectOfType(typeof(Base));
    }

    public void Update()
    {
        if (Base.CurrentHealth <= 0&&!ended)
        {
            StartCoroutine( End());
        }
    }

    public IEnumerator End()
    {
        ended = true;
        Player.main.rbfpc.enabled=false;
        Player.main.enabled = false;
        /*
        Camera.main.transform.parent = null;
        Camera.main.transform.position = EndCamera.position;
        Camera.main.transform.rotation = EndCamera.rotation;
        */
        yield return new WaitForSeconds(5);
        Instantiate(Explosion, ExplosionPoint, Explosion.transform.rotation);
        Instantiate(Fire, ExplosionPoint, Fire.transform.rotation);
        Transform[] children=Base.GetComponentsInChildren<Transform>();

        foreach( Transform t in children)
        {
            if (!t.GetComponent<Rigidbody>())
            {
                t.gameObject.AddComponent<Rigidbody>();
            }
        }
        //Bring up menu
    }

}
