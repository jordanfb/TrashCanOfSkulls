using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    private float rotateX;
    private float rotateY;
    public float rotateSpeedX;
    public float rotateSpeedY;

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        rotateX += rotateSpeedX * Input.GetAxis("Mouse X");
        rotateY -= rotateSpeedY * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(rotateY, rotateX, 0);
    }
}
