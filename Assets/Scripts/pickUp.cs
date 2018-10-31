using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickUp : MonoBehaviour {

    public Text txt;
    public Text noteCount;
    public Image img;

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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Notebook")
        {
            txt.text = "Press E to pick up";
            if (Input.GetKeyDown(KeyCode.E))
            {
                img.sprite = other.GetComponent<SpriteRenderer>().sprite;
                img.enabled = true;
                //Also can increment count here
                Destroy(other.gameObject);
                txt.text = "Press LMB to close";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Notebook")
        {
            txt.text = "";
        }
    }
}
