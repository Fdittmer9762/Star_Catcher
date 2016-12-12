using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

    public Rigidbody starRB;
    public float rnd, rndMax, rndMin;

    void Start()
    {
        starRB = GetComponent<Rigidbody>();
    }

    void OnEnable() {
        Debug.Log("Fire!!!");
        rnd = Random.Range(rndMin,rndMax);
        starRB.AddForce(rnd, - Mathf.Abs(rnd), 0);
    }

}
