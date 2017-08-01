using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModel : MonoBehaviour {

    public float minScale = 0.1f;
    public float maxScale = 4.0f;

    public float scaleStep = 0.1f;

    float currentScale = 1.0f;

    public float CurrentScale
    {
        get
        {
            return currentScale;
        }

        set
        {
            currentScale = value;
        }
    }

    public void ZoomOut()
    {
        float nextScale = CurrentScale - scaleStep;

        CurrentScale = nextScale < minScale ? minScale : nextScale;
    }

    public void ZoomIn()
    {
        float nextScale = CurrentScale + scaleStep;

        CurrentScale = nextScale > maxScale ? maxScale : nextScale;
    }
}
