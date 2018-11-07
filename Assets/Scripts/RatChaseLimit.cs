using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatChaseLimit : MonoBehaviour {

    [Header("The area the rat will chase the player")]
    [SerializeField]
    private Transform patrolAreaLimit;
    [SerializeField]
    private float patrolAreaRadius;

    [SerializeField]
    private List<RatController> rats;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 1, .5f);
        Gizmos.DrawWireSphere(transform.position, patrolAreaRadius);
    }

    // Update is called once per frame
    void Update () {
		foreach (RatController r in rats)
        {
            if (r.ChasingPlayer)
            {
                if (Vector3.Distance(r.transform.position, transform.position) > patrolAreaRadius)
                {
                    // stop it from chasing the player
                    // Debug.Log("Rat is outside zone so stop chasing");
                    r.StopChasingPlayer(true);
                }
            }
        }
	}
}
