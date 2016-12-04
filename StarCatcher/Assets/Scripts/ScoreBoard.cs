using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

    public int currentScore;

    //EVENT SUB
    void OnEnable() {
        StarCollect.OnCollection += AddPoints;
    }

    void OnDisable() {
        StarCollect.OnCollection -= AddPoints;
    }

    int AddPoints(int points) {
        Debug.Log("Added: " + points + " points!");
        return currentScore += points;
    }

}
