using UnityEngine;
using System.Collections;

public class StarCollect : MonoBehaviour {

    public int sValue;
    public GameObject star;

    public delegate int StarCollected(int val);
    public static event StarCollected OnCollection;

    void OnTriggerEnter() {
        if (OnCollection != null) {
            OnCollection(sValue);
        }
        star.SetActive(false); //deactivate the star after collection
    }

}
