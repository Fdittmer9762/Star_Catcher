using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {

    public GameObject pauseScreen;
    public GameObject gameHUD;

    void OnEnable(){
        InputManager.OnPressPause += OnPause;
        EnableScreen(true, gameHUD);
    }

    void OnDisable(){
        InputManager.OnPressPause -= OnPause;
    }

    public void OnPause()
    {
        ChangeTime();
        //Debug.Log("pause button");
    }

    void ChangeTime()
    {
        if (Time.timeScale == 0)
        { //if the game is paused
            Time.timeScale = 1; // resume the game
            EnableScreen(false, pauseScreen);
            EnableScreen(true, gameHUD);
        }
        else
        {
            Time.timeScale = 0; // pause the game
            EnableScreen(true, pauseScreen);
            EnableScreen(false, gameHUD);
        }
    }

    void EnableScreen(bool enabled, GameObject gObj)
    {
        gObj.SetActive(enabled);
    }

}

