using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class Base : MonoBehaviour {

    public int MaxHealth;
    public int CurrentHealth;
    public int DamageRange;
    public GameObject[] Cannons;
    public ThrowableObject Projectile;
    public float FireRange;
    public float FireRate;
    public float FirePower;
    public Image HealthBar;
    public Text HealthText;

    public void Start()
    {
        InvokeRepeating("CheckDamage", 0, 1);
        StartCoroutine(AttackZombies());
    }

    public void Update()
    {
        if (CurrentHealth <= 0)
        {
            GetDestroyed();
        }
    }

    public void GetDestroyed()
    {

    }

    public void CheckDamage()
    {
        if (Zombie.AllZombies == null) { return; }
        List<Zombie> Nearby = Zombie.AllZombies.FindAll(x => Vector3.Distance(x.transform.position, transform.position) <= DamageRange);
        foreach (Zombie Z in Nearby)
        {
            CurrentHealth -= Z.attack;
        }
       // Debug.Log((float)((float)MaxHealth / (float)CurrentHealth));
        HealthBar.fillAmount = (float)((float)CurrentHealth / (float)MaxHealth);
        HealthText.text = "Base " + CurrentHealth + "/" + MaxHealth;
    }

    public IEnumerator AttackZombies()
    {
        yield return new WaitForSeconds(1);
        while( true){
            if (Zombie.AllZombies.Count == 0) { yield return null; }
            foreach (GameObject Cannon in Cannons)
            {
                Zombie Closest = ClosestZombie(Cannon);
                if (Cannon.transform.position.DistanceIgnoreHeight(Closest.transform.position) < FireRange)
                {
                    Cannon.transform.LookAt(Closest.transform);
                    ThrowableObject projectile = Instantiate(Projectile, Cannon.transform.position, Cannon.transform.rotation) as ThrowableObject;
                    if (projectile.rb == null) { projectile.rb = projectile.GetComponent<Rigidbody>(); }
                    projectile.rb.AddRelativeForce(Vector3.forward * FirePower);
                    Destroy(projectile.gameObject, 15);
                }
                yield return new WaitForSeconds(FireRate);
            }
        }

    }

    public Zombie ClosestZombie(GameObject go)
    {
        if (Zombie.AllZombies.Count == 0) { return null; }
        Zombie Closest=Zombie.AllZombies[0];
        float distance = go.transform.position.DistanceIgnoreHeight(Closest.transform.position);
        foreach (Zombie Z in Zombie.AllZombies)
        {
            if (go.transform.position.DistanceIgnoreHeight(Z.transform.position)<distance)
            {
                Closest = Z;
            }
        }
        return Closest;
    }


}
