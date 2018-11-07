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

    private bool readyToExplode = true;

    public void Start()
    {
        particleSys.Stop();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (readyToExplode)
        {
            GetComponent<AudioSource>().Play();
            readyToExplode = false;
            Debug.Log("VIAL HIT SOMETHING OH GOD KILL EVERYTHING");
            particleSys.Play();
            // then circle cast to see if we can hit the enemy nearby
            RaycastHit hit;
            // Physics.SphereCastAll(transform.position, explosionRadius, transform.forward, out hit, 0, sphereCastLayerMask, QueryTriggerInteraction.Collide)
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, explosionRadius, transform.forward, 0, sphereCastLayerMask);

            foreach (RaycastHit h in hits)
            {
                RatController rat = h.transform.GetComponent<RatController>();
                Debug.Log("VIAL HIT ENEMY");
                if (rat)
                {
                    Debug.Log("STUNN RAT");
                    // then stun the rat
                    rat.StunRat();
                }
            }
        }
    }
}
