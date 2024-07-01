using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float health = 10;
    public float radius = 0.7f;
    public int layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int maxColliders = 10;
        Collider[] hits = new Collider[maxColliders];

        int noOfHits = Physics.OverlapSphereNonAlloc(transform.position, radius, hits, layerMask);
        if (noOfHits > 0)
        {
            for (int _hits = 0; _hits < noOfHits; _hits++)
            {
               Enemy b = hits[_hits].GetComponent<Enemy>();

                if (b != null)
                {
                    //TakeDamage(1);
                    Debug.Log("Damage");
                }
               

            }
        }

    }
}
