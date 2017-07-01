using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIView : MonoBehaviour {

    public Text velocityDisplay;
    public GameModel model;
	
	void Update () {
        velocityDisplay.text = System.Convert.ToString(model.Velocity);
	}
}
