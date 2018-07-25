using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour {

    public Material OverMaterial;
    public Material OutMaterial;
    public string SceneDestination;

    private BoxCollider boxCollider;
    private Renderer MyRenderer;
    private float Timer;
    private bool GazeAt;
    
	// Use this for initialization
	void Start () {
        this.MyRenderer = GetComponent<Renderer> ();
        this.MyRenderer.material = OutMaterial;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void OnClick()
    {
        SceneManager.LoadScene(SceneDestination);
    }

    public void Over()
    {
        MyRenderer.material = OverMaterial;
    }

    public void Out()
    {
        MyRenderer.material = OutMaterial;
    }

}
