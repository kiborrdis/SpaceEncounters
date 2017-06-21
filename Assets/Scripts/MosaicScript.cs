using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosaicScript : MonoBehaviour {
    enum Corners { leftBot, leftTop, rightTop, rightBot };
    public GameObject mosaic;

    List<GameObject> mosaicList;
    List<Rect> rectList;
    Plane eventPlane;
    int sideSize;
    int halfSize;
    
    // Use this for initialization
    void Start () {
        eventPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
        mosaicList = new List<GameObject>();
        rectList = new List<Rect>();
        this.transform.GetChild(0);
        
        sideSize = Mathf.RoundToInt(mosaic.transform.lossyScale.x);
        halfSize = sideSize / 2;
        Debug.Log(sideSize + " " + halfSize);
        rectList.Add(new Rect(new Vector2(-halfSize, -halfSize), new Vector2(sideSize, sideSize)));
	}

    int roundToStep(float number)
    {
        return halfSize * (int)System.Math.Floor(number / halfSize);
    }
    

    Rect findYClosestRect(Vector2 point)
    {
        Rect closest = rectList[0];
        float minDistance = float.PositiveInfinity;
        bool isBottom = false;
        bool wasBetween = false;

        foreach (Rect rect in rectList)
        {
            float yMin = System.Math.Abs(rect.yMin - point.y);
            float yMax = Math.Abs(rect.yMax - point.y);

            bool localBetween = point.x > rect.xMin && point.x < rect.xMax;

            float min = System.Math.Min(yMin, yMax);

            if (min < minDistance)
            {
                minDistance = min;
                closest = rect;
                isBottom = minDistance == yMin;

                wasBetween = localBetween;
            }
            else if (min == minDistance && !wasBetween)
            {
                closest = rect;
                wasBetween = localBetween;
            }
        }

        return new Rect(new Vector2(closest.x, closest.y + (isBottom ? -sideSize : sideSize)), new Vector2(sideSize, sideSize));
    }

    Rect findXClosestRect(Vector2 point)
    {
        Rect closest = rectList[0];
        float minDistance = float.PositiveInfinity;
        bool isLeft = false;
        bool wasBetween = false;

        foreach (Rect rect in rectList)
        {
            float xMin = Math.Abs(rect.xMin - point.x);
            float xMax = Math.Abs(rect.xMax - point.x);

            bool localBetween = point.y > rect.yMin && point.y < rect.yMax;

            float min = Math.Min(xMin, xMax);

            if (min < minDistance)
            {
                minDistance = min;
                closest = rect;
                isLeft = minDistance == xMin;

                wasBetween = localBetween;
            } else if (min == minDistance && !wasBetween) {
                closest = rect;
                wasBetween = localBetween;
            }

            
        }

        return new Rect(new Vector2(closest.x + (isLeft ? -sideSize : sideSize), closest.y), new Vector2(sideSize, sideSize));
    }

    bool isInFilledZone(Vector2 point)
    {
        bool isContains = false;

        foreach (Rect rect in rectList)
        {
            isContains = isContains || rect.Contains(point);

            if (isContains)
            {
                return true;
            }
        }

        return isContains;
    }

    bool isInXFilledZone(Vector2 point)
    {
        bool isContains = false;

        foreach (Rect rect in rectList)
        {
            //Debug.Log("Point XMIN" + rect.xMin + " XMAX " + rect.xMax + " TARGET " + point.x);
            isContains = isContains || (point.x > rect.xMin && point.x < rect.xMax);

            if (isContains)
            {
                return true;
            }
        }

        return isContains;
    }

    bool isInYFilledZone(Vector2 point)
    {
        bool isContains = false;

        foreach (Rect rect in rectList)
        {
            //Debug.Log("Point YMIN" + rect.yMin + " YMAX " + rect.yMax + " TARGET " + point.y);
            isContains = isContains || (point.y > rect.yMin && point.y < rect.yMax);

            if (isContains)
            {
                return true;
            }
        }

        return isContains;
    }

    void instantiateNew(Vector2 point, Corners corner)
    {
        if (!isInXFilledZone(point))
        {
            Rect xOffer = findXClosestRect(point);
            //Debug.Log("not in X" + ' ' + xOffer.x + 50 + ' ' + xOffer.y + 50);
            GameObject newMosaicX = Instantiate(mosaic, new Vector3(xOffer.x + halfSize, transform.position.y, xOffer.y + halfSize), mosaic.transform.rotation, transform) as GameObject;
            rectList.Add(xOffer);
        }

        if (!isInYFilledZone(point))
        {
            Rect yOffer = findYClosestRect(point);
            GameObject newMosaicY = Instantiate(mosaic, new Vector3(yOffer.x + halfSize, transform.position.y, yOffer.y + halfSize), mosaic.transform.rotation, transform) as GameObject;
            rectList.Add(yOffer);
        }

        if (!isInFilledZone(point))
        {
            //Debug.Log("not contains");
            Rect cOffer = Rect.zero;
            switch (corner) {
                case Corners.leftBot:
                    cOffer = new Rect(new Vector2(roundToStep(point.x) - halfSize, roundToStep(point.y) - halfSize), new Vector2(sideSize, sideSize));
                    break;
                case Corners.rightBot:
                    cOffer = new Rect(new Vector2(roundToStep(point.x), roundToStep(point.y) - halfSize), new Vector2(sideSize, sideSize));
                    break;
                case Corners.leftTop:
                    cOffer = new Rect(new Vector2(roundToStep(point.x) - halfSize, roundToStep(point.y) ), new Vector2(sideSize, sideSize));
                    break;
                case Corners.rightTop:
                    cOffer = new Rect(new Vector2(roundToStep(point.x), roundToStep(point.y)), new Vector2(sideSize, sideSize));
                    break;

            }
            GameObject newMosaicC = Instantiate(mosaic, new Vector3(cOffer.x + halfSize, transform.position.y,  cOffer.y + halfSize), mosaic.transform.rotation, transform) as GameObject;
            rectList.Add(cOffer);
        }

    }

    // Update is called once per frame
    void Update () {
        int width = Camera.main.pixelWidth;
        int height = Camera.main.pixelHeight;
        Ray rightTopRay = Camera.main.ScreenPointToRay(new Vector3(width, height, Camera.main.nearClipPlane));
        Ray leftTopRay = Camera.main.ScreenPointToRay(new Vector3(0, height, Camera.main.nearClipPlane));
        Ray rightBottomRay = Camera.main.ScreenPointToRay(new Vector3(width, 0, Camera.main.nearClipPlane));
        Ray leftBottomRay = Camera.main.ScreenPointToRay(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 hitPoint;

        float distanceHit;
        //Debug.Log("Right top");
        eventPlane.Raycast(rightTopRay, out distanceHit);
        hitPoint = rightTopRay.GetPoint(distanceHit);
        instantiateNew(new Vector2(hitPoint.x, hitPoint.z), Corners.rightTop);
        
        eventPlane.Raycast(leftTopRay, out distanceHit);
        hitPoint = leftTopRay.GetPoint(distanceHit);
        instantiateNew(new Vector2(hitPoint.x, hitPoint.z), Corners.leftTop);

        eventPlane.Raycast(rightBottomRay, out distanceHit);
        hitPoint = rightBottomRay.GetPoint(distanceHit);
        instantiateNew(new Vector2(hitPoint.x, hitPoint.z), Corners.rightBot);

        eventPlane.Raycast(leftBottomRay, out distanceHit);
        hitPoint = leftBottomRay.GetPoint(distanceHit);
        instantiateNew(new Vector2(hitPoint.x, hitPoint.z), Corners.leftBot);

    }
}
