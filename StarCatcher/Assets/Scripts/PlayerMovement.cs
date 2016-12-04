using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    //MOVEMENT VARS
    public float speed, speedDamper = 1, jumpForce, gravity; //force of jump and gravity, speed is character movement speed, speed damper allows for changes to the movement speed     ***may move gravity to a public static class later for universal access***
    private int jumpCount = 0, jumpLimit = 2;

    //PLAYER AGENT  
    public CharacterController agentCC; //responsible for moving player agent
    protected Vector3 agentTP; // responsible for setting destination of player agent

    //PLAYER
    public CharacterController playerCC; //moves the player art
    protected Vector3 playerTP;
    protected bool isLeft; // is the player art facing left
    public Transform playerArt; //holds the player animator

    //SETUP
    public void Start() {
        //set things
        StartCoroutine(Move());
    }

    //EVENT SUBS
    void OnEnable() {
        GroundedEventManager.OnGrounded += ResetJumpCount;
    }

    void OnDisable()
    {
        GroundedEventManager.OnGrounded -= ResetJumpCount;
        StopCoroutine(Move());
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
        agentCC.Move(agentTP); //move player to point
        playerCC.Move(playerTP);
    }

    protected float Run(float dir, float tPX) {
        Rotate(dir); //determines if the art should be flipped
        return tPX = dir * Time.deltaTime * speed; //set the players horizontal destination based on the input, returns the value
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
        if( j > 0 && jumpCount < jumpLimit) {
            //tPY = j * jumpForce * Time.deltaTime; //sets the vert position with input and jumpforce
            //cause player to jump
            jumpCount++;
            Debug.Log(jumpCount);
        } 
        return Gravity(tPY);
    }

    public float Gravity(float j) { //adds gravity to object                                        **may have as part of inheritance later**
        return j -= (gravity * Time.deltaTime); //returns the jump with the gravity added
    }

    private void ResetJumpCount() {
        Debug.Log(jumpCount + " :reset to");
        jumpCount = 0;
    }
}
