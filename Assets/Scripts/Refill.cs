using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refill : MonoBehaviour {
    public Throw player;
    public int refillAmount;

	// Use this for initialization
	void Start() {
		
	}

    void Update() {
 
    }

    private void OnTriggerStay(Collider other) {
		if (Input.GetKeyDown(KeyCode.E)) {
            player.vialAmount = refillAmount;
        }
	}
}
