using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarRevealer : MonoBehaviour {

    public int radius = 5;
    public GameObject mapObject;
    public Color mapColor = Color.yellow;
    private GameObject _mapIndicator;

    // Use this for initialization
    void Start () {
        FogOfWarManager.Instance.RegisterRadarRevealer(this);
        //Color color = FogOfWarManager.Instance._secondaryColor;

        //_mapIndicator = Instantiate(mapObject, transform.position, mapObject.transform.rotation, transform) as GameObject;
        //_mapIndicator.GetComponent<Renderer>().material.color = color;
        //_mapIndicator.transform.localScale = new Vector3(radius, 2.2f, radius);

        //Vector3 curPos = _mapIndicator.transform.position;
        //curPos.y = -2;
        //_mapIndicator.transform.position = curPos;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {
        FogOfWarManager.Instance.UnregisterRadarRevealer(this);
    }
}
