using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour {

	public LayerMask mask;
	public GameObject selection;
	static TargetingController _instance;
	GameObject target;


	public static TargetingController Instance
	{
		get
		{
			return _instance;
		}
	}

	public GameObject Target {
		get {
			return target;
		}
	}

	// TODO Find a way to do this without a singleton
	void Awake () {
		_instance = this;

	}
	
	void Update () {
		//TODO check out if it's possible to use events instead
		if (Input.GetButtonDown ("Fire2")) {
			Debug.Log ("Check targeting");

			Vector3 mousePos = Input.mousePosition;
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(mousePos);

			if (Physics.Raycast (ray, out hit, 10000, mask)) {
				Debug.Log ("Found target");
				if (target) {
					//TODO get rid of this "name" shit
					Transform oldSelection = target.transform.FindChild("Selection");

					Destroy(oldSelection.gameObject);
				}

				target = hit.collider.gameObject;

				GameObject newSelection = Instantiate(selection, target.transform.TransformPoint(Vector3.zero), new Quaternion(), target.transform) as GameObject;
				//TODO get rid of this "name" shit
				newSelection.name = "Selection";
			}
		}
	}
}
