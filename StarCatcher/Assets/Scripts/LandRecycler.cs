using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandRecycler : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        other.gameObject.SetActive(false);
    }

}
