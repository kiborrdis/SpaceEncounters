using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWarhead : MonoBehaviour {
    Transform target;
    public GameObject projectile;
    public Transform Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    void Awake () {
        GameObject targetCandidate = GameObject.FindGameObjectWithTag("Target");

        if (targetCandidate)
        {
            target = targetCandidate.transform;
        }
    }

    

    // Update is called once per frame
    void Update () {
		if (!target)
        {
            return;
        }

        Vector3 targetLocation = target.position;
        Vector3 location = transform.position;
        Vector3 heading = targetLocation - location;
        Vector3 currentHeading = transform.rotation * Vector3.forward;
        float distanceToTarget = heading.magnitude;
        float errorAngle = Vector3.Angle(heading, currentHeading);

        if (distanceToTarget < 25 && errorAngle < 1.0f)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position + transform.forward, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0)) as GameObject;

            Destroy(this.gameObject);
        }
    }
}
