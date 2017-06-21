using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void StartLevel()
    {
        Debug.Log("Hello world!");

        Application.LoadLevel("Scene1");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
