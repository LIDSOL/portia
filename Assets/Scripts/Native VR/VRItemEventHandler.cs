using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRStandardAssets.Utils;

[RequireComponent(typeof(VRInteractiveItem))]
public class VRItemEventHandler : MonoBehaviour {

    public UnityEvent GazeEnterEvent;
    public UnityEvent GazaExitEvent;
    public UnityEvent GazeClickEvent;

    private VRInteractiveItem interactiveItem;

    // Use this for initialization
    void Start() {
        interactiveItem = GetComponent<VRInteractiveItem>();
        interactiveItem.OnOver += OnGazeEnter;
        interactiveItem.OnOut += OnGazeExit;
        interactiveItem.OnClick += OnClick;
    }

    // Update is called once per frame
    void Update() {

    }
    
    void OnClick()
    {
        GazeClickEvent.Invoke();
    }

    void OnGazeEnter()
    {
        GazeEnterEvent.Invoke();
    }

    void OnGazeExit()
    {
        GazaExitEvent.Invoke();
    }

}
