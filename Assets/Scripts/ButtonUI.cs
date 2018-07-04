using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour {

    public Sprite OverSprite;
    public Sprite OutSprite;
    public string SceneDestination;

    private Image image;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        image.sprite = OutSprite;
	}

    // Update is called once per frame
    void Update()
    {
    
	}

    public void OnClick()
    {
        SceneManager.LoadScene(SceneDestination);
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