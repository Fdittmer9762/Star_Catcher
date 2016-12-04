using UnityEngine;
using System.Collections;

public class StarManager : MonoBehaviour {


    public GameObject[] Stars;
    public Vector3 startPos;
    public int delayMin, delayMax, delay;

    void Start() {
        startPos.y += 6;
        StartCoroutine(StarSpawning(startPos));
    }

    IEnumerator StarSpawning(Vector3 rndPos) { //working may change, dont like current iteration, need to try some changes
        for (int i = 0; i < Stars.Length; i++) {
            yield return new WaitForSeconds(delay);
            rndPos += new Vector3(Random.Range(0,5), 0,0);
            Stars[i].transform.position = rndPos;
            Stars[i].SetActive(true);
        }
        StartCoroutine(StarSpawning(rndPos));
    }
}
