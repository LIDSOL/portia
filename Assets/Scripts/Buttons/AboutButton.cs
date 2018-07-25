using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutButton : MonoBehaviour {
    public Material OverMaterial; // Material para cuando el objeto es señalado
    public Material OutMaterial;
    public Animator AboutAnimator;

    public float AboutTime = 3.0f; // Tiempo que se muestra el mensaje
	public Canvas MyCanvas;
    
    private Renderer MyRenderer;
    private bool GazeAt; // Si el objeto esta señalado o no

    // Variables para controlar tiempo activo del canvas
    private bool CanvasEnable;
    private float TimeCanvas;

    // Más eficiente utilizar hash en lugar de string
    private int ScaleAnimatorHash = Animator.StringToHash("Scale");
    private int CloseAnimatorHash = Animator.StringToHash("Close");

    void Start () {
        this.MyRenderer = GetComponent<Renderer>();
        this.MyRenderer.material = OutMaterial; 
        this.GazeAt = false;
        this.CanvasEnable = false;
        this.TimeCanvas = 0.0f;
	}
	
	
	void Update () {
        if (CanvasEnable)
        {
            TimeCanvas += Time.deltaTime;
            if (TimeCanvas >= AboutTime)
            {
                CanvasEnable = false;
                TimeCanvas = 0.0f;
                AboutAnimator.SetBool(ScaleAnimatorHash, false);
                AboutAnimator.SetBool(CloseAnimatorHash, true);
                if (!GazeAt)
                {
                    this.MyRenderer.material = OutMaterial;
                }
            }
        }
	}


    public void OnClick()
    {
        StartAnimation();
    }

    /**
     * Callback que se llama cuando el objeto es señalado
     */
    public void Over()
    {
        this.GazeAt = true;
        this.MyRenderer.material = OverMaterial;
    }

    /**
     * Callback que se llama cuando el objeto deja de ser señalado 
     */
    public void Out()
    {
        if(!CanvasEnable)
        {
            this.MyRenderer.material = OutMaterial;
        }
        this.GazeAt = false;
        
    }

    /**
     * Realiza los cambios necesarios para mostrar el texto
     * correctamente
     */
    private void StartAnimation()
    {
        CanvasEnable = true;
        AboutAnimator.SetBool(ScaleAnimatorHash, true);
        AboutAnimator.SetBool(CloseAnimatorHash, false);
    }
}
