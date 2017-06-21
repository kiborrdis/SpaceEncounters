using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDefense : MonoBehaviour {

    public GameObject projectile;
    public float rotationSpeed;
    public string targetTag = "redmissile";
    public float maxShootingAngle = 5.0f;
    public float maxShootingDistance = 50.0f;
    public float power = 0.0f;
    public float rateOfFire = 10.0f;

    private float rotationSpeedRad;
    private float beforeNextShot;

    private FogOfWarManager fowManager;
	// Use this for initialization
	void Start () {
        beforeNextShot = rateOfFire;
        fowManager = FogOfWarManager.Instance;

        rotationSpeedRad = Mathf.Deg2Rad * rotationSpeed;
	}

    GameObject findClosestTarget()
    {
        Vector3 curLocation = transform.position;
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        float minDistance = float.PositiveInfinity;
        GameObject target = null;

        foreach (GameObject candidate in targets)
        {
            if (!fowManager.isObjectVisible(candidate.transform.position) && !fowManager.isObjectRadarVisible(candidate.transform.position))
            {
                continue;
            }

            float candidateDistance = (candidate.transform.position - curLocation).magnitude;

            if (candidateDistance < minDistance)
            {
                minDistance = candidateDistance;
                target = candidate;
            }
        }

        return target;
    }
	
	// Update is called once per frame
	void Update () {
        GameObject target = findClosestTarget();

        if (beforeNextShot > 0)
        {
            beforeNextShot -= Time.deltaTime;
        }

        if (target)
        {
            Vector3 heading = target.transform.position - transform.position;
            float distance = heading.magnitude;
            heading.Normalize();

            Vector3 currentHeading = transform.rotation * Vector3.forward;

            Vector3 newHeading = Vector3.RotateTowards(currentHeading, heading, rotationSpeedRad * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(newHeading);

            Debug.DrawRay(transform.position, currentHeading * 20, Color.yellow);
            Debug.DrawRay(transform.position, heading * 10, Color.green);
            Debug.DrawRay(transform.position, newHeading * 10, Color.red);

            float angleToTarget = Vector3.Angle(newHeading, heading);

            if (angleToTarget <= maxShootingAngle && distance <= maxShootingDistance)
            {
                Vector3 projPos = transform.position + transform.forward;

                projPos.y = 2.5f;
                if (beforeNextShot < 0)
                {
                    Debug.Log("Shot");
                    beforeNextShot = rateOfFire;
                    GameObject newProj = Instantiate(projectile, projPos, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0)) as GameObject;
                }
            }
        }

	}
}
