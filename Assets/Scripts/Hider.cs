using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : MonoBehaviour {

    public string mainVisName;

    FogOfWarManager fowManager;
    Renderer curRenderer;
    GameObject radarMark;

	// Use this for initialization
	void Start () {
        fowManager = FogOfWarManager.Instance;
        if (mainVisName != null)
        {
            curRenderer = transform.Find(mainVisName).GetComponent<Renderer>();
        } else
        {
            curRenderer = GetComponent<Renderer>();
        }
        

        foreach (Transform child in transform)
        {
            if (child.CompareTag("RadarMark"))
            {
                radarMark = child.gameObject;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        //{
        //    curRenderer.enabled = true;
        //    radarMark.SetActive(false);
        //} else if (fowManager.isObjectRadarVisible(transform.position))
        //{
        //    curRenderer.enabled = false;
        //    radarMark.SetActive(true);
        //} else { 
        //    curRenderer.enabled = false;
        //    rada		if (fowManager.isObjectVisible(transform.position))rMark.SetActive(false);

        //    if (gameObject.tag == "Target")
        //    {
        //        gameObject.tag = "Untagged";
        //        Transform oldSelection = transform.FindChild("Selection");

        //        Destroy(oldSelection.gameObject);
        //    }
        //}
	}
}
