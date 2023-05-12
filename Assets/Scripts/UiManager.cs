using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script Top Handle Ui Elements And Logic

public class UiManager : MonoBehaviour

{
    public TextMeshProUGUI coinCountText; // Reference To CoinCounttext Element
    public Image RestartDialog; // Reference To Restart Dialog Element
    public GameObject pausePanel;
    public Slider firePowerSlider;
    public TextMeshProUGUI firetext;
    public Image fireImage;
    public Button jumpButton;
    public Button leftButton;
    public Button rightButton;
    //public FixedJoystick joystick;





    public bool jump;

    private void Start()
    {
        platformSetUp();

    }

    void platformSetUp()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {

            jumpButton.gameObject.SetActive(false);
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
            //joystick.gameObject.SetActive(false);
        }
        else
        {

            jumpButton.gameObject.SetActive(true);
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
            //joystick.gameObject.SetActive(true);
        }
    }

    // Method to Restart Game

    public void RestartGame()
    {
        Time.timeScale = 1;

        RestartDialog.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method To Exit Game

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void pauseGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }

        else
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void handleFireUi(bool state)
    {
        firePowerSlider.gameObject.SetActive(state);
        firetext.gameObject.SetActive(state);
        fireImage.gameObject.SetActive(state);
    }

    public void TakeScreenShort()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            string currentTime = System.DateTime.Now.ToString();
            ScreenCapture.CaptureScreenshot("Screenshort" + currentTime + ".png");
            Debug.Log("Screenshort Was Taken");
        }
    }


}
