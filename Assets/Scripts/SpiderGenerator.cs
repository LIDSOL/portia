using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderGenerator : MonoBehaviour {

    public GameObject spider;
    public int MaxNumSpiders = 10;
    public float Delay = 1.0f;
    public int TargetScore = 20;
    public Text TextScore;

    private int Score;
    private bool generate = true;
    private List<GameObject> spiders; 

	// Use this for initialization
	void Start () {
        this.Score = TargetScore;
        this.TextScore.text = Score.ToString();
        spiders = new List<GameObject>();
        for(int i = 0; i < MaxNumSpiders; i++)
        {
            spiders.Add((GameObject) Instantiate(spider));
        }
        foreach (GameObject s in spiders)
        {
            s.GetComponent<SpiderScript>().setGenerator(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (generate)
        {
            foreach (GameObject s in spiders)
            {
                s.GetComponent<SpiderScript>().reactivate();
            }
        }
        this.verifyConditions();
    }
        

    public void addToScore()
    {
        this.Score -= 1;
        this.TextScore.text =  Score.ToString();
    }

    void verifyConditions()
    {
        if (Score == 0)
        {
            // TODO: Mostrar mensaje de que ha finalizado
            generate = false;
            foreach (GameObject s in spiders)
                s.SetActive(false);
        }
    }

}