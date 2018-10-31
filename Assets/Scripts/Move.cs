using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {


    public float speed;
    private Rigidbody rgd;
    public float rotateSpeedX;
    public float rotateSpeedY;
    private Vector3 mousePos;
    private float rotateX;
    private float rotateY;

    // Use this for initialization
    void Start () {
        rgd = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0, 0);
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            transform.Translate(0, 0, speed * Input.GetAxis("Vertical") * Time.deltaTime);
        }
        rotateX += rotateSpeedX * Input.GetAxis("Mouse X");
        rotateY -= rotateSpeedY * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(rotateY, rotateX, 0);
    }
}
