using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //RESET EVENT
    public delegate void ResetLevel();
    public static event ResetLevel OnLevelReset;

    //EVENT SUBS
    void OnEnable() {
        ScoreBoard.PlayerDied += OnPlayerDeath;
    }

    void OnDisable(){
        ScoreBoard.PlayerDied -= OnPlayerDeath;
    }

    void OnPlayerDeath() {
        //Pause Game Time
        //bring in game over screen
        Debug.Log("GameOver");
    }

    void OnResetCall() {
        if (OnLevelReset != null){
            OnLevelReset(); //Send level reset to all subs, anything that needs to be reset for a new game
        } 
        }
}
