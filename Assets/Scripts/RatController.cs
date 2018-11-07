using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class RatController : MonoBehaviour {
    [SerializeField]
    private Move player; // for now connect to the move gameobject since I'm assuming that has info on player state

    [SerializeField]
    private bool chasePlayerIfSpotted = true; // this is if the rat should chase the player if it sees the player
    [SerializeField]
    private bool canLooseTrackOfPlayer = true;
    [Space]
    [Header("The player searching information")]
    [SerializeField]
    private float distanceAlwaysSeen;
    [SerializeField]
    private float ratSightAngle = 40; // between + and - 40 degrees

    [Space]
    [SerializeField]
    private PatrolScript patrolScript; // the rat may be patrolling, if so, it should stop patrolling when it spots the player, duh.

    public bool chasingPlayer = false;

    [SerializeField]
    private Animation animator; // so that we can control the rat model!
    [SerializeField]
    private NavMeshAgent navMeshAgent;

    public bool ChasingPlayer
    {
        get
        {
            return chasingPlayer;
        }
    }

    // Use this for initialization
    void Start () {
        PlayAnimation();
        if (patrolScript)
        {
            patrolScript.StartPatrolling();
        }
    }

    [ContextMenu("ANIMATION RUN THING HOPEFULLY")]
    public void PlayAnimation()
    {
        animator.Rewind();
        animator.wrapMode = WrapMode.Loop;
        animator["Walk"].speed = 1.5f;
        animator["Walk"].wrapMode = WrapMode.Loop;
        animator.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (!chasingPlayer && chasePlayerIfSpotted)
        {
            if (CanSeePlayer())
            {
                // then chase the player!
                StartChasingPlayer();
            }
        }

        if (chasingPlayer)
        {
            if (LostTrackOfPlayer() && canLooseTrackOfPlayer)
            {
                // then stop chasing the player!
                StopChasingPlayer(false);
            } else
            {
                // chase the player!
                navMeshAgent.SetDestination(player.transform.position);
            }
        }
    }

    bool CanSeePlayer()
    {
        // first find the distance, if you're within a certain distance then the rat can see you
        Vector3 dpos = player.transform.position - transform.position;
        if (dpos.magnitude < distanceAlwaysSeen)
        {
            return true;
        }
        float angle = Mathf.Atan2(dpos.z, dpos.x)*Mathf.Rad2Deg;
        // Debug.Log(angle);
        if (Mathf.Abs(angle) < ratSightAngle)
        {
            // then check if the rat can see the player with a raycast I guess.
            // this should really use a raycast, but for now just return true.
            return true;
        }
        return false;
    }

    bool LostTrackOfPlayer()
    {
        return false; // gets called if the player is out of range of the king and out of sight probably.
    }

    [ContextMenu("Start Chasing Player")]
    public void StartChasingPlayer()
    {
        chasingPlayer = true;
        if (patrolScript)
        {
            patrolScript.StopPatrolling();
        }
    }

    public void StopChasingPlayer(bool finalAttack)
    {
        // if stuck do one final attack probably, if not stuck then just give up I guess! This would also be if you "scared away" your prey I imagine
        chasingPlayer = false;
        if (patrolScript)
        {
            patrolScript.StartPatrolling();
        }
    }
}
