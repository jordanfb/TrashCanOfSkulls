using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {
    public GameObject vial;
    public GameObject playerCamera;

    public Vector3 vialPosition;
    public float vialThrowingForce;

    public int vialAmount = 0;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (vialAmount > 0) {
                GameObject instance = Instantiate(vial, playerCamera.transform.position 
                                                      + playerCamera.transform.forward * vialPosition.z
                                                      + playerCamera.transform.right * vialPosition.y
                                                      + playerCamera.transform.up * vialPosition.x, playerCamera.transform.rotation);
                instance.GetComponent<Rigidbody>().useGravity = true;
                instance.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * vialPosition.z * vialThrowingForce);
                vialAmount -= 1;
            }
        }
    }
}