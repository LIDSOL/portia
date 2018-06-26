using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutButton : MonoBehaviour {
    public Material HighlightMaterial; // Material para cuando el objeto es señalado
    public Material NormalMaterial;


    public float GazeOnTime = 1.0f; // Tiempo para disparar evento del botón
    public float AboutTime = 3.0f; // Tiempo que se muestra el mensaje
	public Canvas MyCanvas;
    
    private Renderer MyRenderer;
    private float Timer; // Tiempo que ha sido señalado el objeto
    private bool GazeAt; // Si el objeto esta señalado o no
    private Renderer myRenderer;
    // Variables para controlar tiempo activo del canvas
    private bool CanvasEnable;
    private float TimeCanvas;

	void Start () {
        this.myRenderer = GetComponent<Renderer>();
        this.myRenderer.material = NormalMaterial; 
        this.MyCanvas.enabled = false;
        this.Timer = 0.0f;
        this.GazeAt = false;
        this.CanvasEnable = false;
        this.TimeCanvas = 0.0f;
	}
	
	
	void Update () {
		if (GazeAt && !CanvasEnable)
        {
            Timer += Time.deltaTime;
            if (Timer >= GazeOnTime)
            {
                this.myRenderer.material = HighlightMaterial;
                Timer = 0.0f;
                startAnimation();
            }
        }
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
                    this.myRenderer.material = NormalMaterial;
                }
            }
        }
	}


    /**
     * Callback que se llama cuando el objeto es señalado
     */
    public void HighLight()
    {
        this.GazeAt = true;
        this.myRenderer.material = HighlightMaterial;
    }

    /**
     * Callback que se llama cuando el objeto deja de ser señalado 
     */
    public void normalRender()
    {
        if(!CanvasEnable)
        {
            this.myRenderer.material = NormalMaterial;
        }
        this.Timer = 0.0f;
        this.GazeAt = false;
        
    }

    /**
     * Realiza los cambios necesarios para mostrar el texto
     * correctamente
     */
    private void startAnimation()
    {
        // TODO: Agregar efectos de animación
        CanvasEnable = true;
        MyCanvas.enabled = true;
    }
}
