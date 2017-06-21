using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour {


    public GameObject projectile;
    public float maxShootingDistance = 30.0f;
    public float maxShootingAngle = 20.0f;
    public float minDistance = 20.0f;
    public float speed = 5.0f;
    public float rotationSpeed = 30.0f;
    Rigidbody engine;

    private float rotationSpeedRad;

    private EnemyShooter gun;

    Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Transform gunObj = transform.Find("EnemyGun");

        gun = transform.Find("EnemyGun").GetComponent<EnemyShooter>();

        rotationSpeedRad = rotationSpeed * Mathf.Deg2Rad;

        

    }
	
	// Update is called once per frame
	void Update () {
        if (!player)
        {
            return;
        }

                Vector3 heading = player.position - transform.position;
                float distance = heading.magnitude;
                heading.Normalize();

                Vector3 currentHeading = transform.rotation * Vector3.forward;

                Vector3 newHeading = Vector3.RotateTowards(currentHeading, heading, rotationSpeedRad * Time.deltaTime, 0);
                transform.rotation = Quaternion.LookRotation(newHeading);

                Debug.DrawRay(transform.position, currentHeading * 20, Color.yellow);
                Debug.DrawRay(transform.position, heading*10, Color.green);
                Debug.DrawRay(transform.position, newHeading*10, Color.red);

                float angleToTarget = Vector3.Angle(newHeading, heading);


                if (distance > minDistance && Vector3.Angle(newHeading, heading) < 5.0f)
                {
                    transform.position = transform.position + newHeading * speed * Time.deltaTime;
                    //transform.Translate(newHeading * speed * Time.deltaTime);
                }

                if (distance <= maxShootingDistance && angleToTarget <= maxShootingAngle)
                {
                    gun.Shoot(projectile, player);
                }
    }
}
