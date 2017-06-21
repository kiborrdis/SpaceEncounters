using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour {
    public float rotationSpeed = 45.0f;

    private float _rotationSpeed;
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

        _rotationSpeed = rotationSpeed * Mathf.Deg2Rad;

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
	
	// Update is called once per frame
	void Update () {
        if (target)
        {
            Vector3 targetLocation = target.position;
            Vector3 location = transform.position;
            Vector3 heading = targetLocation - location;
            heading.Normalize();

            Vector3 currentHeading = transform.rotation * Vector3.forward;
            currentHeading.Normalize();
            //Debug.Log("?????????????");
            //Debug.Log(targetLocation);
            //Debug.Log("-------------");
            //Debug.Log(currentHeading);
            //Debug.Log(heading);
            //Debug.Log(2.0f * Time.deltaTime);
            Vector3 newHeading = Vector3.RotateTowards(currentHeading, heading, _rotationSpeed * Time.deltaTime, 0);
            //newHeading = Vector3.right;
            newHeading.Normalize();
            Debug.DrawRay(transform.position, currentHeading*2, Color.yellow);
            Debug.DrawRay(transform.position, heading, Color.green);
            Debug.DrawRay(transform.position, newHeading, Color.red);

        
            transform.rotation = Quaternion.LookRotation(newHeading);
            Vector3 pos = transform.position;
            pos.y = 2.5f;

            transform.position = pos;
            if (Vector3.Angle(newHeading, heading) < 5.0f)
            {
                emission.SetActive(true);
                engine.AddForce(newHeading * 5 * Time.deltaTime, ForceMode.VelocityChange);
            } else
            {
                emission.SetActive(false);
                engineView.Stop();
            }
        }
		
	}
}
