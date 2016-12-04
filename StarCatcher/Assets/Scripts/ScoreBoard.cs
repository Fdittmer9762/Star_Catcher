using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

    //SCORE KEEPING
    public int collectedStars;

    //DEATH EVENT
    public delegate void PlayerDeath();
    public static event PlayerDeath PlayerDied;

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
        return collectedStars += points;
    }

    //DAMAGE TO PLAYER
    void OnPlayerDamaged() {
        if (collectedStars <= 0) {
            if (PlayerDied != null){
                PlayerDied();//kill player, fire event
            }
            //use event to pass final score to highscores
            Debug.Log("PlayerIsDead"); //ForDebugging, replace later
        }
        if (collectedStars > 0) {
            Debug.Log("Player has lost " + collectedStars + " stars!" );
            //spawn a partical for each star collected
            collectedStars = 0; //sets current score to 0
        }
    }

}
