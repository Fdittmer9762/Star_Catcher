using UnityEngine;
using System.Collections;

public class WolfManager : MonoBehaviour {

    public GameObject wolfGO;
    private Vector3 currentPos, previousPos, deltaPos;
    public Animator wolfAnim;
    private bool isLeft = false;

    public CharacterController enemyCC;
    public Vector3 enemyTP;
    public float jumpHeight;


    void Start() {
        //filler
    }

    void OnEnable()
    {
        StartCoroutine(EnemyMove());
        SetSpeed(0f);
    }

    void Update() { //UPDATE LATER
        Tracking();
    }

    //JUMPING  
    IEnumerator EnemyMove()
    {
        enemyTP.y = Gforce(enemyTP.y);
        enemyCC.Move(enemyTP);
        yield return null;
        StartCoroutine(EnemyMove());
    }

    private float Gforce(float g)
    {
        return g = -1f * (Statics.gravity * Time.deltaTime);
    }


    //ANIMATION
    public void Tracking() {
        currentPos = wolfGO.transform.position;//updates the current position
        deltaPos = comparePos(currentPos, previousPos); //finds the difference between the two points
        RotateWolf(deltaPos / Time.deltaTime);
        previousPos = currentPos; //sets the previous position to the last current position before it updates
    }

    public Vector3 comparePos(Vector3 curPos, Vector3 prePos) {
        return curPos -= prePos; //finds the difference between the two points
    }

    void RotateWolf(Vector3 speed) {
        if (speed.x == 0) {//if the player is not moving do nothing
            return;
        }
        SetSpeed(speed.x);//sets the animation speed
        Rotate(speed.x);
    }

    void Rotate(float dir)
    { //determines if the art needs to be flipped
        if (dir > 0 && isLeft)
        { //if input is right and the player is facing left
            FlipArt(false); //flip the art
        }
        if (dir < 0 && isLeft == false)
        { //if the input is left and the player is facing right (not left)
            FlipArt(true); //flip the art
        }
    }

    protected void FlipArt(bool l)
    {
        wolfGO.transform.Rotate(0, 180, 0); //rotates the player art
        isLeft = l; //is the player facing left now?
    }

    void SetSpeed(float animSpeed) {
        wolfAnim.SetFloat("Speed",Mathf.Abs(animSpeed));
    }
}
