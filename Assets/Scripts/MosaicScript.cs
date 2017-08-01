using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosaicScript : MonoBehaviour {
    enum Corners { leftBot, leftTop, rightTop, rightBot };
    public GameObject mosaic;

    Dictionary<String, GameObject> partsDict;
    Plane eventPlane;
    int sideSize;
    int halfSize;
    
    // Use this for initialization
    void Start () {
        partsDict = new Dictionary<string, GameObject>();
        eventPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
        
        sideSize = Mathf.RoundToInt(mosaic.transform.lossyScale.x) * 10;
        halfSize = sideSize / 2;
	}

    void fillViewport(Vector2 leftTop, Vector2 rightBot, float minX, float maxX)
    {
        for (float x = Mathf.Min(leftTop.x, minX); x <= Mathf.Max(rightBot.x, maxX); x += sideSize)
        {
            for (float y = rightBot.y; y <= leftTop.y; y +=sideSize)
            {
                String key = Mathf.RoundToInt(x) + ":" + Mathf.RoundToInt(y);

                if (!partsDict.ContainsKey(key))
                {
                    GameObject newPart = Instantiate(mosaic, new Vector3(x, transform.position.y, y), mosaic.transform.rotation, transform) as GameObject;

                    partsDict.Add(key, newPart);
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
        int width = Camera.main.pixelWidth;
        int height = Camera.main.pixelHeight;
        // leftTop, rightTop, rightBot, leftBot
        Ray[] cornerRays = {
            Camera.main.ScreenPointToRay(new Vector3(0, height, Camera.main.nearClipPlane)), 
            Camera.main.ScreenPointToRay(new Vector3(width, height, Camera.main.nearClipPlane)),
            Camera.main.ScreenPointToRay(new Vector3(width, 0, Camera.main.nearClipPlane)),
            Camera.main.ScreenPointToRay(new Vector3(0, 0, Camera.main.nearClipPlane)),
        };
        Vector2[] cornerPoints = new Vector2[6];
        int index = 0;

        foreach(Ray ray in cornerRays)
        {
            float hitDistance;
            Vector3 hitPoint;
            
            eventPlane.Raycast(ray, out hitDistance);
            hitPoint = ray.GetPoint(hitDistance);

            float x = (index == 0 || index == 3 || index == 5) ? (Mathf.Ceil(hitPoint.x / sideSize) - 1) * sideSize : (Mathf.Floor(hitPoint.x / sideSize) + 1) * sideSize;
            float y = (index == 0 || index == 2) ? (Mathf.Ceil(hitPoint.z / sideSize) - 1) * sideSize : (Mathf.Floor(hitPoint.z / sideSize) + 1) * sideSize;

            cornerPoints[index] = new Vector2(x, y);

            index += 1;
        }

        fillViewport(cornerPoints[0], cornerPoints[2], cornerPoints[2].x, cornerPoints[1].x);
    }
}
