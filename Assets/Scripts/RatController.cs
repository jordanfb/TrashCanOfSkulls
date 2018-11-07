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
    [SerializeField]
    private float looseTrackDistance = 20;

    [Space]
    [SerializeField]
    private PatrolScript patrolScript; // the rat may be patrolling, if so, it should stop patrolling when it spots the player, duh.
    [SerializeField]
    private Transform ratHead;

    [SerializeField]
    private float stunAnimationSpeed = .5f;

    [Space]
    public bool chasingPlayer = false;
    private bool isStunned = false; // if this is true then it won't be able to move so stop following the path!
    private float stunTimer = 0;


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
    void Start()
    {
        animator.AddClip(animator["Stun"].clip, "UnStun");
        animator["UnStun"].speed = -1;
        animator["Stun"].speed = stunAnimationSpeed;

        animator["UnStun"].time = animator["UnStun"].length;
        animator["UnStun"].wrapMode = WrapMode.ClampForever;
        animator["Stun"].wrapMode = WrapMode.ClampForever;

        PlayAnimation();
        if (patrolScript)
        {
            patrolScript.StartPatrolling();
        }
    }

    [ContextMenu("ANIMATION RUN THING HOPEFULLY")]
    public void PlayAnimation()
    {
        Rewind();
        // animator.wrapMode = WrapMode.Loop;
        animator["Walk"].speed = 1.5f;
        animator["Walk"].wrapMode = WrapMode.Loop;
        animator.Play("Walk");
    }

    private void Rewind()
    {
        // this is a custom function to do this because we have to correct the time for the UnStun animation
        animator.Rewind();
        animator["UnStun"].time = animator["UnStun"].length;
    }
	
	// Update is called once per frame
	void Update () {
        if (isStunned) {
            // count down the timer
            // Debug.Log(animator.IsPlaying("UnStun"));
            // Debug.Log(animator["UnStun"].time);
            print(animator["Stun"].clip.length);
            if (animator["Stun"].time <= .2f)
            {
                // keep it between .2 and the ending whih is something like .29 or .27 seconds
                animator["Stun"].speed = stunAnimationSpeed;
            } else if (animator["Stun"].time >= animator["Stun"].clip.length)
            {
                animator["Stun"].speed = -stunAnimationSpeed; // go back
            }
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                UnStunRat();
            }
        }
        else
        {
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
                // CanSeePlayer();
                if (LostTrackOfPlayer() && canLooseTrackOfPlayer)
                {
                    // then stop chasing the player!
                    StopChasingPlayer(false);
                }
                else
                {
                    // chase the player!
                    navMeshAgent.SetDestination(player.transform.position);
                }
            }
        }
    }

    bool CanSeePlayer()
    {
        // first find the distance, if you're within a certain distance then the rat can see you
        Vector3 dpos = player.transform.position - transform.position;
        if (dpos.magnitude >= looseTrackDistance && canLooseTrackOfPlayer)
        {
            return false; // you can't see the player this far away otherwise the behavior breaks
        }
        if (dpos.magnitude < distanceAlwaysSeen)
        {
            Debug.Log("Caught sight of the player it was within the distance");
            return true;
        }
        float angle = Vector2.Angle(transform.forward, dpos);
        // float angle = Mathf.Atan2(dpos.z, dpos.x)*Mathf.Rad2Deg;
        // Debug.Log(angle);
        if (Mathf.Abs(angle) < ratSightAngle)
        {
            // then check if the rat can see the player with a raycast I guess.
            // this should really use a raycast, but for now just return true.
            Debug.Log("Caught sight of player it was within the angle");
            return true;
        }
        return false;
    }

    bool LostTrackOfPlayer()
    {
        Vector3 dpos = player.transform.position - transform.position;
        if (dpos.magnitude >= looseTrackDistance)
        {
            Debug.Log("Lost track of player it was too far away");
            return true;
        }
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
            Debug.Log("Start patrolling");
        }
    }

    [ContextMenu("Kill Player")]
    public void CatchPlayer()
    {
        // if you catch the player then KILL THEM
        // animator["PlayerDeath"].weight = 0;
        animator.Play("PlayerDeath");
        player.SetPlayerControllable(false);
        // here we need to do some magic.
        // we want to turn the player towards the mouse and move it towards the head as well
        // and also fade to black
        player.SetKillingRat(this, ratHead);
    }

    public void UnStunRat()
    {
        isStunned = false;
        stunTimer = 0;
        navMeshAgent.isStopped = false;
        animator.PlayQueued("Walk");
        Debug.Log("Rat no longer stunned");
        animator["Walk"].weight = 0;
        animator.Blend("Walk", 1, 1);
        animator.Blend("Stun", 0, 1);
    }

    [ContextMenu("Stun rat")]
    private void TestStunRat()
    {
        StunRat();
    }

    public void StunRat(float time = 5)
    {
        stunTimer = time;
        isStunned = true;
        animator.PlayQueued("Stun");
        // animator["UnStun"].time = animator["UnStun"].length;
        // animator.PlayQueued("UnStun", QueueMode.CompleteOthers, PlayMode.StopAll);
        Debug.Log("Rat stunned");
        navMeshAgent.isStopped = true;
        animator["Stun"].time = 0;
        animator["Stun"].weight = 0;
        animator.Blend("Stun", 1, 1);
        animator.Blend("Walk", 0, 1);
    }
}
