using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Memu : MonoBehaviour {

    public GameObject button;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown() {
        if (button.name == "Play") {
            SceneManager.LoadScene("Master");
        }
        if (button.name == "Quit") {
            Application.Quit();
        }
        if (button.name == "About") {
            SceneManager.LoadScene("About");
        }
        if (button.name == "Back") {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
