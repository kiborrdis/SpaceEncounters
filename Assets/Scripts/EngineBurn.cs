using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineBurn : MonoBehaviour {
    public enum EngineDirection { Forward, Backward };
    GameObject engine;
	ParticleSystem particleView;
    public EngineDirection direction = EngineDirection.Forward;

	// Use this for initialization
	void Start () {
        //gameObject.SetActive(false);
        engine = transform.Find("EngineEmission").gameObject;

		particleView = engine.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
        float verticalDelta = Input.GetAxis("Vertical");

        switch(direction)
        {
            case EngineDirection.Forward:
                if (verticalDelta <= 0)
                {
					particleView.Stop ();
                }
                else
                {
					particleView.Play ();
                }
                break;

            case EngineDirection.Backward:
                if (verticalDelta >= 0)
                {
					particleView.Stop ();
                }
                else
                {
					particleView.Play ();
                }
                break;
        }
        
	}
}
