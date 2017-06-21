using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGroupController : MonoBehaviour {
	public bool rapidFire = true;

	private Plane eventPlane;
	private List<WeaponController> weapons;

	void Start () {
		WeaponController[] rawWeapons = GetComponentsInChildren<WeaponController> ();
		weapons = new List<WeaponController> ();

		foreach (WeaponController weapon in rawWeapons) {
			weapons.Add (weapon);
		}

		eventPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
	}
	
	void Update () {
		bool needToFire = rapidFire ? Input.GetButton ("Fire1") : Input.GetButtonDown ("Fire1");

		if (needToFire) {
			FireWeapons ();
		}
	}

	Vector3 GetFireDirection() {
		Vector3 pos = Input.mousePosition;
		Ray ray = Camera.main.ScreenPointToRay(pos);

		float hitDistance;
		eventPlane.Raycast(ray, out hitDistance);

		Vector3 direction = ray.GetPoint (hitDistance) - transform.position;
		direction.Normalize ();

		return direction;
	}

	void FireWeapons() {
		Vector3 direction = GetFireDirection ();

		foreach (WeaponController weapon in weapons) {
			weapon.Fire (direction, null);
		}
	}
}
