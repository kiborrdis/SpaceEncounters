using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour {

    public bool enabled = false;

	private WeaponController shooter;
    private GameObject zone;

	// Use this for initialization
	void Start () {
		shooter = GetComponent<WeaponController>();

        if (shooter)
        {
            shooter.enabled = enabled;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Switch weapon") )
        {
            enabled = !enabled;
        }

        if (shooter)
        {
            shooter.enabled = enabled;
        }

    }
}
