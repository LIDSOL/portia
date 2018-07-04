﻿using UnityEngine;
using UnityEngine.UI;

public class ButtonUIQuit : MonoBehaviour
{

    public Sprite OverSprite;
    public Sprite OutSprite;

    private Image image;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = OutSprite;
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
        image.sprite = OverSprite;
    }

    public void Out()
    {
        image.sprite = OutSprite;
    }
}