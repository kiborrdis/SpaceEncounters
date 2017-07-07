using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartHoming : MonoBehaviour {
    public TrajectorySolver trajectorySolver;
    public MotionModel model;

    public GameObject warhead;
    public float detonationRange;

    private GameObject target;
    private Rigidbody objectRigidbody;
    private Rigidbody targetRigidBody;

    public GameObject Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;

            targetRigidBody = value.GetComponent<Rigidbody>();
        }
    }


    // Use this for initialization
    void Awake () {
        objectRigidbody = GetComponent<Rigidbody>();

        if (TargetingController.Instance)
        {
            GameObject targetCandidate = TargetingController.Instance.Target;

            if (targetCandidate)
            {
                Target = targetCandidate;
            }
        }
	}

    void Detonate ()
    {
        GameObject warheadEffect = Instantiate(warhead, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
        if (!target)
        {
            return;
        }

        if (Vector3.Distance(transform.position, target.transform.position) < detonationRange)
        {
            Detonate();

            return;
        }

        TrajectorySolver.Trajectory trajectory = trajectorySolver.calculateTrajectoryFromTo(transform.position, objectRigidbody.velocity, target.transform.position, targetRigidBody.velocity, model.maxSpeed);

        Debug.DrawLine(transform.position, trajectory.endPoint, Color.cyan);

        model.DesiredVelocity = trajectory.velocity;
        model.DesiredHeading = trajectory.velocity.normalized;
    }
}
