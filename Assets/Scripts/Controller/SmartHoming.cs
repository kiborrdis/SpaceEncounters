using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartHoming : MonoBehaviour {
    public float rotationSpeed = 45.0f;
    public float maxSpeed = 5.0f;
    public float accel = 1;

    private float rotationSpeedRad;
    public Transform target;
    Rigidbody engine;
    ParticleSystem engineView;

    GameObject emission;

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


    // Use this for initialization
    void Awake () {
        GameObject targetCandidate = GameObject.FindGameObjectWithTag("Target");

        engine = gameObject.GetComponent<Rigidbody>();

        rotationSpeedRad = rotationSpeed * Mathf.Deg2Rad;

        emission = transform.Find("EngineEmission").gameObject;

        if (emission)
        {
            engineView = emission.GetComponent<ParticleSystem>();
        }

        if (targetCandidate)
        {
            target = targetCandidate.transform;
        }
	}

    Vector3 findCollisionSpeedVector()
    {
        DummyEnemy dummy = target.GetComponent<DummyEnemy>();

        Vector3 targetSpeed = dummy.Speed;
        targetSpeed.x -= engine.velocity.x;
        targetSpeed.y -= engine.velocity.y;

        float dx = target.position.x - transform.position.x;
        float dy = target.position.z - transform.position.z;

        float k = targetSpeed.x * dy - targetSpeed.z * dx;

        float a = dx * dx + dy * dy;
        float b = 2 * k * dx;
        float c = k * k - dy * dy * maxSpeed * maxSpeed;

        float D = b * b - 4 * a * c;

        if (D < 0)
        {
            Debug.Log("D is lower than zero!");
            return Vector3.zero;
        }

        float r1 = (-b + Mathf.Sqrt(D)) / (2 * a);
        float r2 = (-b - Mathf.Sqrt(D)) / (2 * a);
        float vmy;

        if (target.position.z - transform.position.z > 0)
        {
            vmy = r1;
        }
        else
        {
            vmy = r2;
        }

        float t = dy / (vmy - targetSpeed.z);
        float vmx = (dx + targetSpeed.x * t) / t;
        Vector3 missileSpeed = new Vector3(vmx, 0, vmy);

        Debug.DrawRay(transform.position, missileSpeed * t, Color.cyan);

        return missileSpeed;
    }

    // Update is called once per frame
    void Update () {

        GameObject targetCandidate = GameObject.FindGameObjectWithTag("Target");

        if (targetCandidate)
        {
            target = targetCandidate.transform;
        }

        Vector3 desireHeading = findCollisionSpeedVector();
        desireHeading.Normalize();

        Vector3 currentHeading = transform.rotation * Vector3.forward;
        currentHeading.Normalize();

        Vector3 newHeading = Vector3.RotateTowards(currentHeading, desireHeading, rotationSpeedRad * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newHeading);


        if (Vector3.Angle(newHeading, desireHeading) < 1.0f)
        {

            if (engine.velocity.magnitude < maxSpeed)
            {
                engine.AddForce(newHeading * accel * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
        //transform.position = transform.position + missileSpeed * Time.deltaTime;

    }
}
