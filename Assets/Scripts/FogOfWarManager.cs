using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarManager : MonoBehaviour {

    public int _textureSize = 1024;
    public Color _fowColor = Color.red;
    public Color _secondaryColor = Color.yellow;
    public LayerMask _fowLayer;

    private List<Revealer> _revealers;
    private List<RadarRevealer> _radarRevealers;
    private static FogOfWarManager _instance;

    public static FogOfWarManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public List<Revealer> Revealers
    {
        get
        {
            return _revealers;
        }
    }

    public List<RadarRevealer> RadarRevealers
    {
        get
        {
            return _radarRevealers;
        }
    }


    public bool isObjectVisible(Vector3 position)
    {
        Vector2 location = new Vector2(position.x, position.z);
        
        foreach(Revealer revealer in _revealers)
        {
            Vector2 revealerLocation = new Vector2(revealer.transform.position.x, revealer.transform.position.z);
            bool inRadius = (location - revealerLocation).magnitude < revealer.radius;

            if (inRadius)
            {
                return true;
            }
        }

        return false;
    }

    public bool isObjectRadarVisible(Vector3 position)
    {
        Vector2 location = new Vector2(position.x, position.z);

        foreach (RadarRevealer revealer in _radarRevealers)
        {
            Vector2 revealerLocation = new Vector2(revealer.transform.position.x, revealer.transform.position.z);
            bool inRadius = (location - revealerLocation).magnitude < revealer.radius;

            if (inRadius)
            {
                return true;
            }
        }

        return false;
    }




    public void RegisterRevealer(Revealer revealer)
    {
        _revealers.Add(revealer);
        
    }

    public void UnregisterRevealer(Revealer revealer)
    {
        _revealers.Remove(revealer);
    }

    public void RegisterRadarRevealer(RadarRevealer revealer)
    {
        _radarRevealers.Add(revealer);
    }

    public void UnregisterRadarRevealer(RadarRevealer revealer)
    {
        _radarRevealers.Remove(revealer);
    }



    // Use this for initialization
    void Awake () {
        Debug.Log("Foo");
        _instance = this;

        _revealers = new List<Revealer>();
        _radarRevealers = new List<RadarRevealer>();
    }
	
	// Update is called once per frame
	void Update () {

     


    }
}
