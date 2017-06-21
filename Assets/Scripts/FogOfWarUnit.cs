using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarUnit : MonoBehaviour {
    private Texture2D _texture;
    private Color[] _pixels;
    private float _pixelsPerUnit;
    private Vector2 _centerPixel;
    private int _textureSize;
    private Material fowMaterial;
    private Color _fowColor;
    private Color _secondaryColor;
    private bool isInitialized = false;

    private void ClearPixels()
    {
        for (var i = 0; i < _pixels.Length; i++)
        {
            
            _pixels[i] = _fowColor;
        }
    }


    private void CreateCircle(int originX, int originY, int radius, Color color)
    {
        float time1 = Time.realtimeSinceStartup;

        int negRadius = (int)(-radius * _pixelsPerUnit);
        int posRadius = -1 * negRadius;
        int lengt = posRadius - negRadius;
        int lengt2 = lengt * lengt;
        int squaredRadPPU = (int)((radius * _pixelsPerUnit) * (radius * _pixelsPerUnit));

        for (int i = 0; i < lengt2; i++)
        {
            int x = i % lengt + negRadius;
            int y = i / lengt + negRadius;
            int arrayIndex = (originY + y) * _textureSize + originX + x;

            if (x*x + y*y <= squaredRadPPU && (originX + x < _textureSize && originX + x > 0))
            {

                if (arrayIndex < _pixels.Length && arrayIndex >= 0)
                {
                    _pixels[arrayIndex] = color;
                }
            }
        }

        //Debug.Log("Texture calculation time - " + (Time.realtimeSinceStartup - time1));
    }

    void Awake()
    {
        

        if (fowMaterial == null)
        {
            Debug.LogError("FOW Material havent found");
        }

        if (FogOfWarManager.Instance)
        {
            initialize();
        }
    }

    void initialize()
    {
        var renderer = GetComponent<Renderer>();

        Material fowMaterial = null;

        if (renderer != null)
        {
            fowMaterial = renderer.material;
        }

        _textureSize = FogOfWarManager.Instance._textureSize;
        _fowColor = FogOfWarManager.Instance._fowColor;
        _secondaryColor = FogOfWarManager.Instance._secondaryColor;


        _texture = new Texture2D(_textureSize, _textureSize, TextureFormat.RGBA32, true);
        _texture.wrapMode = TextureWrapMode.Clamp;
        _texture.filterMode = FilterMode.Trilinear;

        _pixels = _texture.GetPixels();

        fowMaterial.mainTexture = _texture;

        _pixelsPerUnit = _textureSize / transform.lossyScale.x;

        _centerPixel = new Vector2(_textureSize * 0.5f, _textureSize * 0.5f);

        isInitialized = true;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!isInitialized)
        {
            initialize();
        }

        ClearPixels();
        Color transparent = new Color(0, 0, 0, 0);
        List<Revealer> revealers = FogOfWarManager.Instance.Revealers;
        List<RadarRevealer> radarRevealers = FogOfWarManager.Instance.RadarRevealers;
        float time1 = Time.realtimeSinceStartup;
        foreach (var revealer in radarRevealers)
        {
            handleRevealer(revealer.transform.position, revealer.radius, _secondaryColor);
        }

        foreach (var revealer in revealers)
        {
            handleRevealer(revealer.transform.position, revealer.radius, transparent);
        }

        //Debug.Log(Time.realtimeSinceStartup);
        //Debug.Log("Texture calculation time - " + (Time.realtimeSinceStartup - time1));
        _texture.SetPixels(_pixels);
        StartCoroutine("applyTexture");
    }

    IEnumerator applyTexture()
    {
        float time1 = Time.realtimeSinceStartup;
        _texture.Apply(false);
        //Debug.Log("Texture apply time - " + (Time.realtimeSinceStartup - time1));
        yield return null;
    }

    void handleRevealer(Vector3 position, int radius, Color color)
    {
        Vector3 vect = position;
        vect.y = 0;
        var screenPoint = Camera.main.WorldToScreenPoint(vect);
        var ray = Camera.main.ScreenPointToRay(screenPoint);
        
        Vector3 hitPoint;
        float distanceHit;
        Plane eventPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
        eventPlane.Raycast(ray, out distanceHit);
        hitPoint = ray.GetPoint(distanceHit);

        // Translates the revealer to the center of the fog of war.
        // This way the position lines up with the center pixel and can be converted easier.

        var translatedPos = hitPoint - transform.position;

        var pixelPosX = Mathf.RoundToInt(translatedPos.x * _pixelsPerUnit + _centerPixel.x);
        var pixelPosY = Mathf.RoundToInt(translatedPos.z * _pixelsPerUnit + _centerPixel.y);

        CreateCircle(pixelPosX, pixelPosY, radius, color);
    }
}
