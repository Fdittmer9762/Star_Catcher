using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent navAgent;
    public Transform playerAgent;

    void OnEnable() {
        PlayerMovement.PlayerMoved += OnPlayerMove;
    }

    void OnDisable() {
        PlayerMovement.PlayerMoved -= OnPlayerMove;
    }

    void OnPlayerMove() {  //                                   **need to find a way to feed info through the delegate, may be lest costly, thinking action<t>**
        //Debug.Log("Tracking Player" + Time.time); //for debugging
        navAgent.destination = playerAgent.position; //updates player position
        navAgent.updateRotation = false; //prevents agent turning around messing with the sprites, cameras, etc.
    }
}
