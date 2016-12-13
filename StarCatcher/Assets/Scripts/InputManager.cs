using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public delegate void Pause();
    public static event Pause OnPressPause;

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (OnPressPause != null) {
                OnPressPause();
            }
        }
    }

}
