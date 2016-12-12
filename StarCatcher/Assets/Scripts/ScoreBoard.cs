using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    //SCORE KEEPING
    public int collectedStars;
    public Text starCount;


    //DEATH EVENT
    public delegate void PlayerDeath();
    public static event PlayerDeath PlayerDied;

    void Start() {
        //starCount = GetComponent<UnityEngine.UI.GUIText>();
    }

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
        UpdateScore(collectedStars += points); //may clean up later, need to call update somehow now
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
            UpdateScore(collectedStars);//updates score
        }
    }

    void UpdateScore(int score) {
        starCount.text = score.ToString();
    }
}
