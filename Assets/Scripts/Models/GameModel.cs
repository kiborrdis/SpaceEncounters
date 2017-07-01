using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : MonoBehaviour {

    static GameModel _instance;

    float velocity = 0.0f;

    public float Velocity
    {
        get
        {
            return velocity;
        }
        set
        {
            velocity = value;
        }
    }

    public static GameModel Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake () {
        _instance = this;
	}
	
	void Update () {
		
	}
}
