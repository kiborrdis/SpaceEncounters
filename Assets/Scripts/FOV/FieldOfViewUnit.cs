using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewUnit : MonoBehaviour {

    public FieldOfViewManager manager;

    public float revealRadius = 4.0f;

    public MeshFilter viewMeshFilter;
    public MeshFilter ribbonViewMeshFilter;
    Mesh viewMesh;
    Mesh ribbonMesh;

    public Mesh ViewMesh
    {
        get
        {
            return viewMesh;
        }
    }

    public Mesh RibbonViewMesh
    {
        get
        {
            return ribbonMesh;
        }
    }

    // Use this for initialization
    void Start () {
		if(!manager)
        {
            Debug.Log("This is fucking wrong!");
        }

        

        if (viewMeshFilter)
        {
            viewMesh = new Mesh();
            viewMesh.name = "View mesh";

            viewMeshFilter.mesh = viewMesh;
        }

        if (ribbonViewMeshFilter)
        {
            ribbonMesh = new Mesh();
            ribbonMesh.name = "View mesh";

            ribbonViewMeshFilter.mesh = ribbonMesh;
        }
        manager.addUnit(this);
	}

    private void OnDestroy()
    {
        if (!manager)
        {
            Debug.Log("No, really");
        }

        manager.removeUnit(this);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
