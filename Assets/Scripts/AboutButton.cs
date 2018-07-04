using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutButton : MonoBehaviour {
    public Material OverMaterial; // Material para cuando el objeto es señalado
    public Material OutMaterial;


    public float AboutTime = 3.0f; // Tiempo que se muestra el mensaje
	public Canvas MyCanvas;
    
    private Renderer MyRenderer;
    private bool GazeAt; // Si el objeto esta señalado o no
    private Renderer myRenderer;
    // Variables para controlar tiempo activo del canvas
    private bool CanvasEnable;
    private float TimeCanvas;

	void Start () {
        this.myRenderer = GetComponent<Renderer>();
        this.myRenderer.material = OutMaterial; 
        this.MyCanvas.enabled = false;
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
                MyCanvas.enabled = false;
                TimeCanvas = 0.0f;
                if (!GazeAt)
                {
                    this.myRenderer.material = OutMaterial;
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
        this.myRenderer.material = OverMaterial;
    }

    /**
     * Callback que se llama cuando el objeto deja de ser señalado 
     */
    public void Out()
    {
        if(!CanvasEnable)
        {
            this.myRenderer.material = OutMaterial;
        }
        this.GazeAt = false;
        
    }

    /**
     * Realiza los cambios necesarios para mostrar el texto
     * correctamente
     */
    private void StartAnimation()
    {
        // TODO: Agregar efectos de animación
        CanvasEnable = true;
        MyCanvas.enabled = true;
    }
}
