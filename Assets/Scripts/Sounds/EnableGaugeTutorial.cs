using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGaugeTutorial : MonoBehaviour {
    [SerializeField] BoxCollider Collider;
    void Update() {
        Collider.enabled = GameEventManager.INSTANCE.WeldingDone;
    }
}
