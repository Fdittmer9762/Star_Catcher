using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

    public int currentScore;

    //EVENT SUB
    void OnEnable() {
        StarCollect.OnCollection += AddPoints;
        DamagePlayer.OnPlayerDamaged += OnPlayerDamaged;
    }

    void OnDisable() {
        StarCollect.OnCollection -= AddPoints;
        DamagePlayer.OnPlayerDamaged -= OnPlayerDamaged;
    }

    //ADDING POINTS
    int AddPoints(int points) {
        Debug.Log("Added: " + points + " points!");
        return currentScore += points;
    }

    //DAMAGE TO PLAYER
    void OnPlayerDamaged() {
        if (currentScore <= 0) {
            //kill player, fire event
            //use event to pass final score to highscores
            Debug.Log("PlayerIsDead"); //ForDebugging, replace later
        }
        if (currentScore > 0) {
            Debug.Log("Player has lost " + currentScore + " stars!" );
            //spawn a partical for each star collected
            currentScore = 0; //sets current score to 0
        }
    }

}
