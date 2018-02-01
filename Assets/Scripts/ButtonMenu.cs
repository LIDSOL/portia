using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour {

    public Material HighLightMaterial;
    public string SceneDestination;
    public float GazeOnTime = 1.0f;

    private Material NormalMaterial;
    private Renderer MyRenderer;
    private float Timer;
    private bool GazeAt;
    
	// Use this for initialization
	void Start () {
        this.MyRenderer = GetComponent<Renderer> ();
        this.NormalMaterial = this.MyRenderer.material;
        this.Timer = 0.0f;
        this.GazeAt = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (GazeAt)
        {
            Timer += Time.deltaTime;
            if (Timer >= GazeOnTime)
            {
                Timer = 0.0f;
                Debug.Log("Executing");
                SceneManager.LoadScene(SceneDestination);
            }
        }
	}

    public void HighLight()
    {
        MyRenderer.material = HighLightMaterial;
        GazeAt = true;
    }

    public void NormalRender()
    {
        MyRenderer.material = NormalMaterial;
        GazeAt = false;
        Timer = 0.0f;
    }

}
