using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosion;

    public void Explode()
    {
        if (explosion)
        {
            GameObject obj = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        }
    }
}
