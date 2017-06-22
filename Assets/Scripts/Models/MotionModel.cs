using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionModel : MonoBehaviour {

    public enum Rotation { stale, left, right };
    public enum Acceleration { stale, forward, backward, left, right };

    Rotation currentRotation = Rotation.stale;
    Acceleration currentAcceleration = Acceleration.stale;

    public Rotation rotation
    {
        set
        {
            Debug.Log(value);
            currentRotation = value;
        }
        get
        {
            return currentRotation;
        }
    }

    public Acceleration acceleration
    {
        set
        {
            Debug.Log(value);
            currentAcceleration = value;
        }
        get
        {
            return currentAcceleration;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
