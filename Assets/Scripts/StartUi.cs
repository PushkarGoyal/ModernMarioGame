using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUi : MonoBehaviour
{

    [SerializeField] GameObject controlsPanel;

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void showControls()
    {
        controlsPanel.SetActive(true);
    }

    public void closePanel()
    {
        controlsPanel.SetActive(false);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
