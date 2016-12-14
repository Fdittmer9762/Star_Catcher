using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    //MOVEMENT VARS
    public float speed, speedDamper = 1, jumpForce, gravity; //force of jump and gravity, speed is character movement speed, speed damper allows for changes to the movement speed     ***may move gravity to a public static class later for universal access***
    private int jumpCount = 0, jumpLimit = 20;

    //PLAYER AGENT  
    public CharacterController agentCC; //responsible for moving player agent
    protected Vector3 agentTP; // responsible for setting destination of player agent

    // ANIMATION
    public Animator playerAnim;

    //PLAYER
    public CharacterController playerCC; //moves the player art
    protected Vector3 playerTP;
    protected bool isLeft; // is the player art facing left
    public Transform playerArt; //holds the player animator
    public bool isGrounded;
    public GameObject hazardDet;

    //TRACKPLAYER EVENT
    public delegate void TrackPlayer();
    public static event TrackPlayer PlayerMoved;

    //SETUP
    public void Start() {
        //set things
        speed = Statics.playerSpeed;
        jumpForce = Statics.playerJumpForce;
        StartCoroutine(Move()); //starts player movement,                   **may move to onenable()
    }

    //EVENT SUBS
    void OnEnable() { //when player agent is enabled
        GroundedEventManager.OnGrounded += ResetJumpCount;//unsubs from grounder
        ScoreBoard.PlayerDied += DisableMovement;
        DamagePlayer.OnPlayerDamaged += OnPlayerDamaged;
        ScoreBoard.PlayerDied += OnPlayerDeath;
    }

    void OnDisable() //when player agent is disabled
    {
        GroundedEventManager.OnGrounded -= ResetJumpCount; //unsubs from grounder
        StopCoroutine(Move()); //stop movement,                      **may replace with an action<t> event
        ScoreBoard.PlayerDied -= DisableMovement;
        DamagePlayer.OnPlayerDamaged -= OnPlayerDamaged;
        ScoreBoard.PlayerDied -= OnPlayerDeath;
    }

    // DAMAGED
    void OnPlayerDamaged() {
        playerAnim.SetTrigger("Damaged");
        StartCoroutine(Invulnrability());
    }

    IEnumerator Invulnrability (){
        hazardDet.SetActive(false);
        yield return new WaitForSeconds(1f);
        hazardDet.SetActive(true);
    }

    //DEATH
    void OnPlayerDeath() {
        playerAnim.SetBool("isDead", true);
        StopCoroutine(Move()); //stop movement,     
    }

    //MOVEMENT
    public IEnumerator Move() { //used in place of update
        MovePlayer();
        yield return null;
        StartCoroutine(Move());
    }

    public void MovePlayer() {
        agentTP.x = Run(Input.GetAxis("Horizontal"), agentTP.x); // uses input to cause player to set the horizontal movement
        agentTP.y = Gravity (agentTP.y); //applys gravity to the player agent

        playerTP.y = Jump(Input.GetAxis("Vertical"), playerTP.y); // uses the input causing the player to jump
        playerTP.x = agentTP.x; //sets reference to agent movement instead of calling the chain of mehtods agian, second cc causes independent movement despite parent child relationship, may alter things later

        float dif = agentCC.transform.position.x - playerCC.transform.position.x;
        agentTP.x -= dif;

        agentCC.Move(agentTP); //move player to point
        playerCC.Move(playerTP);
    }

    protected float Run(float dir, float tPX) {
        Rotate(dir); //determines if the art should be flipped
        playerAnim.SetFloat("Speed", Mathf.Abs(dir));
        if (dir != 0 && PlayerMoved != null) { //if player will move and the event is not null
            PlayerMoved(); //send player location to event subs
        }
        return tPX = dir * Time.deltaTime * speed* speedDamper; //set the players horizontal destination based on the input, returns the value
    }

    protected void Rotate(float dir) { //determines if the art needs to be flipped
        if (dir > 0 && isLeft) { //if input is right and the player is facing left
            FlipArt(false); //flip the art
        }
        if (dir < 0 &&  isLeft == false) { //if the input is left and the player is facing right (not left)
            FlipArt(true); //flip the art
        }
    }

    protected void FlipArt(bool l) {
        playerArt.Rotate(0, 180, 0); //rotates the player art
        isLeft = l; //is the player facing left now?
    }

    public float Jump(float j, float tPY) {
        if( j > 0 && playerCC.isGrounded) { //if the player is jumping
            tPY += jumpForce * Time.deltaTime;
            isGrounded = false;
            //tPY = j * jumpForce * Time.deltaTime; //sets the vert position with input and jumpforce, **uses charactercontroller.Move()
            //cause player to jump ,                                                                   ** may use animation or replace input.getaxis with input.getbutton("space") for something that fires once, even an event
            jumpCount++;
            Debug.Log(jumpCount);
        } 
        return Gravity(tPY);
    }

    public float Gravity(float j) { //adds gravity to object                                        **may have as part of inheritance later**
        if (isGrounded) {
            playerAnim.SetBool("isGrounded", true);
            return j = -gravity * Time.deltaTime;
        } else {
            playerAnim.SetBool("isGrounded", false);
            return j -= (gravity * Time.deltaTime); //returns the jump with the gravity added
        }
    }

    private void ResetJumpCount() {
        Debug.Log(" Jump count reset to" + jumpCount);
        jumpCount = 0;
        isGrounded = true;
    }

    private void DisableMovement() {
        jumpForce = 0;
        speedDamper = 0;
    }
}
