using UnityEngine;
using System.Collections;

public class GroundedEventManager : MonoBehaviour {

    public delegate void PlayerGrounded();
    public static event PlayerGrounded OnGrounded;

    void OnTriggerEnter() {
        Debug.Log("ground detected");
        if (OnGrounded != null) {
            OnGrounded();
        }
        //may be able to replace with a nullable type, look into later
    }
}
