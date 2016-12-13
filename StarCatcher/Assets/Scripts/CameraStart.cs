using UnityEngine;
using System.Collections;

public class CameraStart : MonoBehaviour {

    public GameObject cam, gameManager;
    public Vector3 startMarker, endMarker;
    private float startTime, journeyLength, speed = 1;

    void Start() {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker, endMarker);
        StartCoroutine(MoveCam());
    }

    IEnumerator MoveCam() {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        cam.transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
        yield return null;
        if (fracJourney < 1)
        {
            StartCoroutine(MoveCam());
        }
        else {
            gameManager.SetActive(true);
            Statics.speedDamper = 1;
        }
    }

}
