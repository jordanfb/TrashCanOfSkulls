using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public float speed;
    private Rigidbody rgd;
    private Vector3 mousePos;
    public Camera cam;
    private Vector3 camDir;
    private Vector3 camSid;
    public AudioSource footStep;
    bool playing = false;

    // Use this for initialization
    void Start () {
        rgd = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        camDir = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
        camSid = new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized;
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            transform.position = transform.position + Input.GetAxis("Horizontal") * camSid * Time.deltaTime * speed;
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            transform.position = transform.position + Input.GetAxis("Vertical") * camDir * Time.deltaTime * speed;
        }
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 && !playing || Mathf.Abs(Input.GetAxis("Vertical")) > 0 && !playing)
        {
            footStep.Play();
            playing = true;
        }
        if (Mathf.Abs(Input.GetAxis("Horizontal")) == 0 && Mathf.Abs(Input.GetAxis("Vertical")) == 0)
        {
            footStep.Stop();
            playing = false;
        }
    }
}
