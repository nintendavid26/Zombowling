using UnityEngine;
using System.Collections;

public class SportsZombie : Zombie {
    public float ThrowRange;
    public float ThrowPower;
    public GameObject Hand;
    public ThrowableObject heldThrowableObject;

    // Use this for initialization
    void Update()
    {
        if (!dead)
        {

            float step = speed * Time.deltaTime;
            if (Target == null) { Target = AllPlayers.allPlayers.PlayerList[0].gameObject; }
            if (Target.tag == "Meat Head") { transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step * 2); }
            else { transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, step); }
            if (Vector3.Distance(transform.position,Target.transform.position)<ThrowRange&&Target.tag=="Player") { transform.LookAt(Target.transform.position);  heldThrowableObject.Thrown(this); }
        }
    }



    public void checkClosestTarget()
    {
        if (Target.tag != "Meat Head") {
            GameObject[] gos;
            GameObject closestPlayer = null;
            GameObject closestBall = null;
            gos = GameObject.FindGameObjectsWithTag("Player");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                    closestPlayer = go;
                }
            }
            gos = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                    closestBall = go;
                }
            }
            if (closestPlayer!=null) {
                if (Vector3.Distance(transform.position, closestBall.transform.position) < Vector3.Distance(transform.position, closestPlayer.transform.position)) { Target = closestBall; }
                else { Target = closestPlayer; }
            }
        }
    }

}
