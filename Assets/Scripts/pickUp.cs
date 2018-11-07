using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickUp : MonoBehaviour {

    public Text txt;
    public Text img;
    public Text skulls;
    private int skullCount;

    // Use this for initialization
    void Start () {
        img.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (img.enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //increment count here if you want
                img.enabled = false;
                txt.text = "";
            }
        }
        skulls.text = "Skulls: " + skullCount + "/4";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Notebook")
        {
            txt.text = "Press E to pick up";
            if (Input.GetKeyDown(KeyCode.E))
            {
                img.enabled = true;
                img.text = other.GetComponent<TextHolder>().data.ToString();
                //Also can increment count here
                Destroy(other.gameObject);
                txt.text = "Press LMB to close";
            }
        }
        else if (other.tag == "Skull")
        {
            txt.text = "Press E to pick up";
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(other.gameObject);
                skullCount += 1;
                //increment counter here
                txt.text = "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Notebook")
        {
            txt.text = "";
        }
        else if (other.tag == "Skull")
        {
            txt.text = "";
        }
    }
}
