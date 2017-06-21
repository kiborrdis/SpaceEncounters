using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

    public float power = 10.0f;
    public float rateOfFire = 10.0f;

    private float beforeNextShot;
	// Use this for initialization
	void Start () {
        beforeNextShot = rateOfFire;
	}

    public void Shoot(GameObject projectile, Transform target)
    {
        if (beforeNextShot > 0)
        {
            return;
        }
        GameObject newProj = Instantiate(projectile, transform.position + transform.forward, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0)) as GameObject;
        Vector3 heading = target.position - transform.position;
        heading.Normalize();

        LaserWarhead warhead = newProj.GetComponent<LaserWarhead>();
        Homing homing = newProj.GetComponent<Homing>();

        newProj.GetComponent<Rigidbody>().AddForce(heading * power, ForceMode.VelocityChange);

        if (warhead && homing)
        {
            warhead.Target = target;
            homing.Target = target;
        }

        beforeNextShot = rateOfFire;
    }

    void Update()
    {
        if (beforeNextShot < 0)
        {
            return;
        }

        beforeNextShot -= Time.deltaTime;
    }
}
