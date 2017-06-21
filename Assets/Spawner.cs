using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float minDistance = 10.0f;
    public float maxDistance = 100.0f;
    public GameObject objToSpawn;
    public float spawnRate = 5.0f;

    private float beforeNextSpawn;

	// Use this for initialization
	void Start () {
        beforeNextSpawn = spawnRate;
	}
	
	// Update is called once per frame
	void Update () {
        beforeNextSpawn -= Time.deltaTime;

        if (beforeNextSpawn > 0)
        {
            return;
        }

        float distance = Random.value * maxDistance + minDistance;
        float rotation = Random.value*Mathf.PI*2 - Mathf.PI;

        Vector3 heading = Vector3.RotateTowards(Vector3.forward, Vector3.back, rotation, 0);
        heading.Normalize();
        Debug.Log(rotation);
        Debug.Log(heading * distance);
        Vector3 spawnPosition = transform.position + heading*distance;
        spawnPosition.y = 2.5f;

        GameObject newProj = Instantiate(objToSpawn, spawnPosition, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as GameObject;

        beforeNextSpawn = spawnRate;
    }
}
