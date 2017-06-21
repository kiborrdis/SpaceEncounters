using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityController : MonoBehaviour {

    private bool isVisible;
    private bool isDetectable;
    public FieldOfViewManager sightManager;
    public FieldOfViewManager radarManager;
    public GameObject radarShadow;
    public GameObject originForm;



    public bool IsVisible
    {
        get
        {
            return IsVisible;
        }
        set
        {
            isVisible = value;

            if (value)
            {
                objRenderer.enabled = true;
                shadowRenderer.enabled = false;
            } else
            {
                objRenderer.enabled = false;

                if (isDetectable)
                {
                    shadowRenderer.enabled = true;
                }

            }
            //objRenderer.material.SetColor("_Color", value ? Color.red : Color.cyan);
        }
    }

    public bool IsDetectable
    {
        get
        {
            return isDetectable;
        }
        set
        {
            isDetectable = value;

            if (value && !isVisible)
            {
                shadowRenderer.enabled = true;

            } else
            {
                shadowRenderer.enabled = false;

            }
        }
    }
    Renderer objRenderer;
    Renderer shadowRenderer;

    

	// Use this for initialization
	void Start () {
        objRenderer = originForm.GetComponent<Renderer>();
        shadowRenderer = radarShadow.GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update () {
        if (radarManager.visibleTargets.Contains(transform))
        {
            IsDetectable = true;
        }
        else
        {
            IsDetectable = false;
        }


        if (sightManager.visibleTargets.Contains(transform))
        {
            IsVisible = true;
        } else
        {
            IsVisible = false;
        }
	}
}
