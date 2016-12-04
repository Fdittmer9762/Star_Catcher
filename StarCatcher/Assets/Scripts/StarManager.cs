using UnityEngine;
using System.Collections;

public class StarManager : MonoBehaviour {


    public GameObject[] Stars;
    public Vector3 startPos;
    public int delayMin, delayMax, delay;
    public GameObject player;

    void Start() {
        startPos.y += 10;
        StartCoroutine(StarSpawning(startPos));
    }

    IEnumerator StarSpawning(Vector3 rndPos)
    { //working may change, dont like current iteration, need to try some changes
        for (int i = 0; i < Stars.Length; i++)
        {
            delay = Random.Range(delayMin, delayMax);
            yield return new WaitForSeconds(delay);
            float temp = 4+ Mathf.Abs(Statics.playerSpeed * Input.GetAxis("Horizontal")*2);
            rndPos = player.transform.position;
            rndPos += new Vector3(Random.Range(temp*-.5f, temp * 1.5f), 0, 0);
            Stars[i].transform.position = rndPos;
            Stars[i].SetActive(true);
        }
        StartCoroutine(StarSpawning(rndPos));
    }

}
