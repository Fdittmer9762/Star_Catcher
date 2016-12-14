using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

    public Rigidbody starRB;
    public float rnd, rndMax, rndMin;
    public Animator starAnim;

    void Start()
    {
        starRB = GetComponent<Rigidbody>();
    }

    void OnEnable() {
        starAnim.SetTrigger("Active");//start star anim
        rnd = Random.Range(rndMin,rndMax);
        starRB.AddForce(rnd, - Mathf.Abs(rnd), 0);
    }

}
