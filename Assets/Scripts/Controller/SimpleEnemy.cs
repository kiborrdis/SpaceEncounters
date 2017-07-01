using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour {

    public MotionModel model;
    public GameObject projectile;
    public float maxShootingDistance = 30.0f;
    public float maxShootingAngle = 20.0f;
    public float minDistance = 20.0f;

    Rigidbody engine;

    private EnemyShooter gun;

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        Transform gunObj = transform.Find("EnemyGun");
        engine = GetComponent<Rigidbody>();
        //gun = transform.Find("EnemyGun").GetComponent<EnemyShooter>();
    }

	// Update is called once per frame
	void Update () {
        if (!player)
        {
            Debug.Log("No player");
            return;
        }

        Vector3 direction = (player.transform.position - transform.position).normalized;
        Vector3 desiredLocation = 0.75f * maxShootingDistance * (-1 * direction) + player.transform.position;
        Vector3 desiredVelocity;

        if (Vector3.Distance(player.transform.position, transform.position) < maxShootingDistance)
        {
            desiredVelocity = Vector3.zero;
        } else
        {
            desiredVelocity = direction * model.maxSpeed;
        }

        model.DesiredVelocity = desiredVelocity;
        model.DesiredHeading = direction;
    }
}
