using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    void OnEnable() {
        PlayerMovement.PlayerMoved += OnPlayerMove;
    }

    void OnDisable() {
        PlayerMovement.PlayerMoved -= OnPlayerMove;
    }

    void OnPlayerMove() {
        Debug.Log("Tracking Player" + Time.time);
    }
}
