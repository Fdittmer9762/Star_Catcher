using UnityEngine;
using System.Collections;

public class DamagePlayer : MonoBehaviour {

    public delegate void PlayerDamaged();
    public static event PlayerDamaged OnPlayerDamaged;

    void OnTriggerEnter() {
        Debug.Log("PlayerHit");
        if (OnPlayerDamaged != null) {
            OnPlayerDamaged();
        }
    }

}
