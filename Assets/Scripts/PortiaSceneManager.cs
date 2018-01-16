using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortiaSceneManager : MonoBehaviour {
    public void LoadScene(string destination) {
        SceneManager.LoadScene(destination);
    }

    public void QuitApplication() {
        Application.Quit();
    }
}
