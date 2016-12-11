using UnityEngine;
using System.Collections;

public interface IResetable { //for anything that needs to or can be reset
    Vector3 originalPos { get; set;} //sets the objects starting pos
    void OnReset(Vector3 oP, GameObject obj); //will be called after an reset event, wiil send object to its starting pos
}
