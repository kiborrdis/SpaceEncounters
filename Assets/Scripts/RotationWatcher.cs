using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationWatcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float horizontalDelta = Input.GetAxis("Horizontal");
        float z = gameObject.transform.eulerAngles.z;
        z = z > 180 ? z - 360 : z;

        if (Mathf.Abs(horizontalDelta) > 0)
        {
            if (Mathf.Abs(z) < 30 || Mathf.Sign(z) == Mathf.Sign(horizontalDelta))
            {
                gameObject.transform.Rotate(-1 * Vector3.forward * horizontalDelta * 120 * Time.deltaTime);
            }
        } else
        {
            if (Mathf.Abs(Mathf.Round(z)) > 0)
            {
                gameObject.transform.Rotate(Vector3.forward * 120 * Time.deltaTime * -1 * Mathf.Sign(z));
            }
        }
        
    }
}
