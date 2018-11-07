using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Move : MonoBehaviour {

    public float speed;
    private Rigidbody rgd;
    private Vector3 mousePos;
    public Camera cam;
    private Vector3 camDir;
    private Vector3 camSid;
    public AudioSource footStep;
    bool playing = false;

    bool isPlayerControllable = true;
    [SerializeField]
    Throw throwScript;
    [SerializeField]
    pickUp pickUpScript;
    [SerializeField]
    Image fadeToBlack;

    [Space]
    [SerializeField]
    private float deadRotationSpeed = .5f;

    private bool isAlive = true;
    private Transform ratHead;
    private Vector3 deadPositionStartPosition;
    private Quaternion ratHeadRotation;
    private Quaternion deadStartRotation;
    private float deadRotationTimer = 0;

    // Use this for initialization
    void Start () {
        rgd = GetComponent<Rigidbody>();
        fadeToBlack.color = new Color(0, 0, 0, 0);
    }

    public void SetPlayerControllable(bool state)
    {
        isPlayerControllable = state;
        throwScript.SetPlayerControllable(state);
        pickUpScript.SetPlayerControllable(state);
    }
	
    public void SetKillingRat(RatController rat, Transform head)
    {
        // when the player dies this is the rat they should look at
        isAlive = false;
        ratHead = head;
        ratHeadRotation = Quaternion.LookRotation(head.position - cam.transform.position);
        deadStartRotation = cam.transform.rotation;
        deadRotationTimer = 0;
        deadPositionStartPosition = transform.position;
    }

	// Update is called once per frame
	void Update () {
        if (isPlayerControllable)
        {
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
        } else if (!isAlive)
        {
            // look towards the rat head

            cam.transform.rotation = Quaternion.Lerp(deadStartRotation, ratHeadRotation, deadRotationTimer);
            deadRotationTimer += Time.deltaTime * deadRotationSpeed;
            fadeToBlack.color = new Color(0, 0, 0, Mathf.Max(1, deadRotationTimer*.5f));
            cam.transform.position = Vector3.Lerp(deadPositionStartPosition, ratHead.position, deadRotationTimer*.8f);

            if (deadRotationTimer > 2)
            {
                SceneManager.LoadScene("DeathScene");
            }
        }
    }
}
