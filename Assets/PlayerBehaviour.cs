using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{
    public string[] vulnerableTo;
    public GameObject explosionPrefab;
    public int hitPoints = 5;

    bool isVulnerableTo(string tag)
    {
        foreach (string pTag in vulnerableTo)
        {
            if (pTag == tag)
            {
                return true;
            }
        }

        return false;
    }

    void OnCollisionEnter(Collision newCollision)
    {
        if (isVulnerableTo(newCollision.gameObject.tag))
        {
            hitPoints--;

            GameManager.gm.healthChange(hitPoints);
            if (explosionPrefab)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }

            if (newCollision.gameObject.GetComponent<Explosion>())
            {
                newCollision.gameObject.GetComponent<Explosion>().Explode();
            }
            Destroy(newCollision.gameObject);

           
            if (hitPoints <= 0)
            {
                Destroy(gameObject);
                GameManager.gm.EndGame();
            }
        }
    }

    void Start()
    {
        if (GameManager.gm)
        {
            GameManager.gm.healthChange(hitPoints);
        }
    }
}
