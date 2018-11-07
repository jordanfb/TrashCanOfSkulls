using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerScript : MonoBehaviour {

    [SerializeField]
    private bool requirePlayerHasCure = false;
    [SerializeField]
    private pickUp pickUpScript;
    [SerializeField]
    private bool oneTimeTrigger = true;

    public UnityEvent triggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("Player hit trigger");
            if (!requirePlayerHasCure || false)
            {
                // then run the event
                Debug.Log("Trigger Triggered");
                triggerEvent.Invoke();
                if (oneTimeTrigger)
                {
                    this.enabled = false; // disable yourself. we could also disable the gameobject but this is slightly safer in case we stick this where we shouldn't.
                }
            }
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
