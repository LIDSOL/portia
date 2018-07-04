using UnityEngine;

public class ButtonMenuExit : MonoBehaviour
{

    public Material OverMaterial;
    public Material OutMaterial;

    private Renderer MyRenderer;

    // Use this for initialization
    void Start()
    {
        this.MyRenderer = GetComponent<Renderer>();
        this.MyRenderer.material = OutMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Application.Quit();
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
