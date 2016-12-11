using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public CharacterController enemyCC;
    public Vector3 enemyTP;
    public float jumpHeight;

    void OnEnable() {
        StartCoroutine(EnemyMove());
    }

    IEnumerator EnemyMove() {
        enemyTP.y = Gforce(enemyTP.y);
        enemyCC.Move(enemyTP);
        yield return null;
        StartCoroutine(EnemyMove());
    }

    private float Gforce (float g) {
        return g = -1f *(Statics.gravity * Time.deltaTime);
    }

}
