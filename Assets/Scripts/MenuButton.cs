using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

    //private Vector3 startingPosition;
    public Material inactiveMaterial;
    public Material activeMaterial;
    public string destination;
    private float gazeOnTime = 2f;
    private float timer;
    private bool gazeAt;

	// Use this for initialization
	void Start () {
        //startingPosition = transform.localPosition;
        timer = 0f;
        GetComponent<Renderer>().material = inactiveMaterial;
    }
	
	// Update is called once per frame
	void Update () {
        if (gazeAt)
        {
            timer += Time.deltaTime;
            if (timer >= gazeOnTime)
            {
                timer = 0f;
            }
        }
    }

    public void PointerEnter()
    {
        Debug.Log("Pointer Enter");
        GetComponent<Renderer>().material = activeMaterial;
        gazeAt = true;
    }

    public void PointerExit()
    {
        Debug.Log("Pointer Exit");
        GetComponent<Renderer>().material = inactiveMaterial;
        timer = 0f;
        gazeAt = false;
    }

    public void PointerClick()
    {
        Debug.Log("Pointer Click");
        SceneManager.LoadScene(destination);
    }

    public void QuitApp()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
