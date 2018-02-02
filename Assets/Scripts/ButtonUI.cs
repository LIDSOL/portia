using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour {

    public Sprite HighLightSprite;
    public Sprite NormalSprite;
    public string SceneDestination;
    public float GazeOnTime = 1.0f;

    private Image image;
    private float Timer;
    private bool GazeAt;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        image.sprite = NormalSprite;
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
                SceneManager.LoadScene(SceneDestination);
            }
        }
	}

    public void HighLight()
    {
        image.sprite = HighLightSprite;
        GazeAt = true;
    }

    public void NormalRenderer()
    {
        image.sprite = NormalSprite;
        GazeAt = false;
        Timer = 0.0f;
    }
}