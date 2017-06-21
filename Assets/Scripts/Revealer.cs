using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revealer : MonoBehaviour {

    public int radius = 5;
    public GameObject mapObject;
    public Color mapColor = Color.green;
    private GameObject _mapIndicator;

    private void Start()
    {
        FogOfWarManager.Instance.RegisterRevealer(this);
        //Color color = FogOfWarManager.Instance._fowColor;

        //_mapIndicator = Instantiate(mapObject, transform.position, mapObject.transform.rotation, transform) as GameObject;
        //_mapIndicator.GetComponent<Renderer>().material.color = color;
        //_mapIndicator.transform.localScale = new Vector3(radius, 2.3f, radius);

        //Vector3 curPos = _mapIndicator.transform.position;
        //curPos.y = -1;
        //_mapIndicator.transform.position = curPos;
    }

	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {
        FogOfWarManager.Instance.UnregisterRevealer(this);
    }
}
