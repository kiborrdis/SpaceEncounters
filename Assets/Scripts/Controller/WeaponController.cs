using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public GameObject projectile;
	public float power = 10.0f;
    public float maxAngleDeg = 30.0f;
	public float rateOfFire = 2.0f;
    private float maxAngleRad;
	private float fireCooldown;
	private float beforeNextShot = 0.0f;

	public void Fire(Vector3 heading, Transform target) {
		if (beforeNextShot > 0) {
			return;
		}
			
		Vector3 currentHeading = transform.rotation * Vector3.forward;

		float headingAngle = Vector3.Angle(heading, currentHeading);
		Vector3 shotDirection;

		if (headingAngle > maxAngleDeg)
		{
			shotDirection = Vector3.RotateTowards(currentHeading, heading, maxAngleRad, 0);
		} else
		{
			shotDirection = heading;
		}
			
		GameObject newProjectile = Instantiate(projectile, transform.position + transform.forward, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0)) as GameObject;

		if (!newProjectile.GetComponent<Rigidbody>()) 
		{
			newProjectile.AddComponent<Rigidbody>();
		}

		newProjectile.GetComponent<Rigidbody>().AddForce(shotDirection * power, ForceMode.VelocityChange);
		beforeNextShot = fireCooldown;

	}

	void Start()
	{
		fireCooldown = 1.0f / rateOfFire;

		maxAngleRad = maxAngleDeg * Mathf.Deg2Rad;
	}

	void Update () {
		if (beforeNextShot > 0) {
			beforeNextShot -= Time.deltaTime;

			return;
		}
	}
}
