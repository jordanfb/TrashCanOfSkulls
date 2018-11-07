using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialScript : MonoBehaviour {

    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private ParticleSystem particleSys;
    [SerializeField]
    private LayerMask sphereCastLayerMask;

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("VIAL HIT SOMETHING OH GOD KILL EVERYTHING");
        // then circle cast to see if we can hit the enemy nearby
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, explosionRadius, transform.forward, out hit, 0, sphereCastLayerMask, QueryTriggerInteraction.Collide))
        {
            RatController rat = hit.transform.GetComponent<RatController>();
            Debug.Log("VIAL HIT ENEMY");
            if (rat)
            {
                // then stun the rat
                rat.StunRat();
            }
        }
    }
}
