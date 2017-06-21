using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{

    public string[] vulnerableTo;

    // target impact on game
    public int scoreAmount = 0;
    public float timeAmount = 0.0f;

    // explosion when hit?
    public GameObject explosionPrefab;

    void Awake()
    {
        
    }

    bool isVulnerableTo(string tag)
    {
        foreach(string pTag in vulnerableTo)
        {
            if (pTag == tag)
            {
                return true;
            }
        }

        return false;
    }

	// when collided with another gameObject
	void OnCollisionEnter (Collision newCollision)
	{
		// exit if there is a game manager and the game is over
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}
        // only do stuff if
        

		if (isVulnerableTo(newCollision.gameObject.tag)) {
            
            if (explosionPrefab) {
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate (explosionPrefab, transform.position, transform.rotation);
			}

			// destroy the projectile
			Destroy (newCollision.gameObject);
				
			// destroy self
			Destroy (gameObject);

            GameManager.gm.targetHit(scoreAmount, 0);
        }
	}
}
