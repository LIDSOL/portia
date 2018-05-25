using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderGenerator : MonoBehaviour {

    public GameObject spider;
    public int MaxNumSpiders = 10;
    public float Delay = 1.0f;
    public int TargetScore = 20;

    private int Score = 0;
    private bool generate = true;
    private List<GameObject> spiders; 

	// Use this for initialization
	void Start () {
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
        this.Score += 1;
        Debug.Log("Score " + Score);
    }

    void verifyConditions()
    {
        if (Score > TargetScore)
        {
            Debug.Log("Se ha alcanzado el objetivo");
            // TODO: Mostrar animación de felicitación porque es bien mach@ :v
            // TODO: Mostrar tiempo en el entorno
            generate = false;
        }
    }

}