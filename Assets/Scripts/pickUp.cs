using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pickUp : MonoBehaviour {

    public Text txt;
    public Text img;
    public Text skulls;
    public Text vials;
    private int skullCount;
    private bool gotVial = false;
    public bool cured = false;
    public GameObject wall;
    public GameObject trashSkulls;

    private bool isPlayerControllable = true;

    // Use this for initialization
    void Start () {
        img.enabled = false;
    }

    public void SetPlayerControllable(bool state)
    {
        isPlayerControllable = state;
    }

    // Update is called once per frame
    void Update () {
        if (img.enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //increment count here if you want
                img.enabled = false;
                txt.text = "";
            }
        }
        if (gotVial)
        {
            skulls.text = "Skulls: " + skullCount + "/4";
            vials.text = "Vials: " + GetComponent<Throw>().vialAmount + "/3";
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            skullCount = 4;
        }
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (isPlayerControllable)
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
                    txt.text = "Press RMB to close";
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
            else if (other.tag == "Vial" && GetComponent<Throw>().vialAmount < 3)
            {
                txt.text = "Press E to pick up";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //increment counter here
                    if (!gotVial)
                    {
                        gotVial = true;
                        Destroy(wall);
                    }
                    GetComponent<Throw>().vialAmount = 3;
                    txt.text = "";
                }
            }
            else if (other.tag == "Cure" && skullCount == 4)
            {
                txt.text = "Press E to create the cure";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    trashSkulls.SetActive(true);
                    cured = true;
                    txt.text = "";
                    skullCount = 0;
                }
            }
            else if (other.tag == "ReleaseZone" && cured)
            {
                txt.text = "Press E to release the cure";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    txt.text = "";
                    cured = false;
                    LevelManager.UnlockMouse();
                    SceneManager.LoadScene("Win");
                }
            }
        }
        else
        {
            img.enabled = false;
            txt.enabled = false;
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
