using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour {

    public GameObject newLandTrigger;
    public GameObject[] LandModules;
    int attemptLimit = 0;

    void OnTriggerEnter() {
        MoveLand();
        Debug.Log("trigger");
    }

    void MoveLand() {
        int rndNum = Random.Range(0, LandModules.Length);
        switch (LandModules[rndNum].gameObject.activeInHierarchy) {
            case true:
                attemptLimit++;
                if (attemptLimit <= LandModules.Length) {
                    MoveLand();
                }
                break;
            case false:
                LandModules[rndNum].transform.position = newLandTrigger.transform.position;
                LandModules[rndNum].SetActive(true);
                newLandTrigger.transform.Translate(Vector3.right * Statics.landModuleSize);
                attemptLimit = 0;
                break;
        }
    }

}
