using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    //SCORE KEEPING
    public int collectedStars;
    public Text[] starCount;

    public float dist;
    public Text[] distScore;
    public string meters = " m";

    public Text[] clock;
    public float timer = 180;
    public GameObject moon;
    public GameObject moonArt;
    //public Quaternion moonRot;


    //DEATH EVENT
    public delegate void PlayerDeath();
    public static event PlayerDeath PlayerDied;

    public float deathDelay = 5;
    public GameObject GOS; //game over screen
    public GameObject GameHUD;


    void Start() {
        StartCoroutine(Timer());
    }

    //EVENT SUB
    void OnEnable() {
        StarCollect.OnCollection += AddPoints;
        DamagePlayer.OnPlayerDamaged += OnPlayerDamaged;
        PlayerMovement.PlayerMoved += OnPlayerMove;
    }

    void OnDisable() {
        StarCollect.OnCollection -= AddPoints;
        DamagePlayer.OnPlayerDamaged -= OnPlayerDamaged;
        PlayerMovement.PlayerMoved -= OnPlayerMove;
    }

    //ADDING POINTS
    int AddPoints(int points) {
        timer -= points;//adjusts the time
        UpdateScore(collectedStars += points, starCount); //may clean up later, need to call update somehow now
        return collectedStars += points;
    }

    //DAMAGE TO PLAYER
    void OnPlayerDamaged() {
        if (collectedStars <= 0) {
            if (PlayerDied != null){
                PlayerDied();//kill player, fire event
            }
            //use event to pass final score to highscores
            OnPlayerDeath();
        }
        if (collectedStars > 0) {
            Debug.Log("Player has lost " + collectedStars + " stars!" );
            //spawn a partical for each star collected
            collectedStars = 0; //sets current score to 0
            UpdateScore(collectedStars, starCount);//updates score
        }
    }

    //FOR GAME OVER
    void OnPlayerDeath() {
        StartCoroutine(GameOverCalled());
    }

    IEnumerator GameOverCalled() {
        //stop player movement 
        //stop star spawning 
        PlayerDied();//play death anim; or call event
        yield return new WaitForSeconds(deathDelay);//wait for anim to finish
        Time.timeScale = 0; //pause time
        GOS.SetActive(true);  //game over screen
        GameHUD.SetActive(false);//game HUD
    }

    // TRACKING DISTANCE PLAYER HAS MOVED
    void OnPlayerMove(){
        dist += Time.deltaTime;
        UpdateScore(Mathf.RoundToInt(dist), distScore, meters);
    }

    //GAME TIMER
    IEnumerator Timer() {
        timer -= Time.deltaTime;//count up time
        Debug.Log(timer);
        //moonRot.z = Mathf.Abs(timer);//update moon transform
        moon.transform.Rotate(Vector3.back * Time.deltaTime);
        moonArt.transform.Rotate(Vector3.forward * Time.deltaTime);
        //moon.transform.rotation = Quaternion.Slerp(moon.transform.rotation, moonRot, Time.deltaTime*5);//move the moon to transform
        UpdateScore(Mathf.RoundToInt(timer), clock); //updates HUD clock
        yield return null;//wait one second
        if (timer <= 0) {
            OnPlayerDeath();
            StopCoroutine(Timer());
        }else {
            StartCoroutine(Timer());
        }
    }

    // UPDATING UI TEXT + OVERLOADS
    void UpdateScore(int score, Text updatedText) {
        updatedText.text = score.ToString();
    } //FOR SINGLE TEXT ELEMENTS

    void UpdateScore(float score, Text updatedText, string extraTxt){
        updatedText.text = score.ToString() + extraTxt;
    }

    void UpdateScore(float score, Text[] updatedText) {
        for (int i = 0; i< updatedText.Length; i++) {
            updatedText[i].text = score.ToString();
        }
    }//FOR ARRAYS OF UI TEXTS

    void UpdateScore(float score, Text[] updatedText, string extraTxt)
    {
        for (int i = 0; i < updatedText.Length; i++)
        {
            updatedText[i].text = score.ToString() + extraTxt;
        }
    }

}
