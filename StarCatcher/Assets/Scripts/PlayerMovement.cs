using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public CharacterController mainCC; //responsible for moving player agent
    protected Vector3 tempPos; // responsible for setting destination of plyaer controller
    protected bool isLeft; // is the player art facing left
    public Transform playerArt; //holds the player animator
    public float jumpForce, gravity; //force of jump and gravity

    public void MovePlayer() {
        tempPos.y = Jump(Input.GetAxis("Vertical"), tempPos.y); // uses the input causing the player to jump
        tempPos.x = Run(Input.GetAxis("Horizontal"), tempPos.x); // uses input to cause player to set the horizontal movement
        mainCC.Move(tempPos); //move player to point
    }

    protected float Run(float dir, float tPX) {
        Rotate(dir); //determines if the art should be flipped
        return tPX = dir * Time.deltaTime; //set the players horizontal destination based on the input, returns the value
    }

    protected void Rotate(float dir) { //determines if the art needs to be flipped
        if (dir > 0 && isLeft) { //if input is right and the player is facing left
            FlipArt(false); //flip the art
        }
        if (dir < 0 && ! isLeft) { //if the input is left and the player is facing right (not left)
            FlipArt(false); //flip the art
        }
    }

    protected void FlipArt(bool l) {
        playerArt.Rotate(0, 180, 0); //rotates the player art
        isLeft = l; //is the player facing left now?
    }

    public float Jump(float j, float tPY) {
        tPY = j * jumpForce * Time.deltaTime; //sets the vert position with input and jumpforce
        return Gravity(tPY);
    }

    public float Gravity(float j) { //adds gravity to object                                        **may have as part of inheritance later**
        return j -= (gravity * Time.deltaTime); //returns the jump with the gravity added
    }


}
