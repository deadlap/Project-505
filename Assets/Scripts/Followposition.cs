using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Followposition : MonoBehaviour {
    [SerializeField] GameObject Follow;
    [SerializeField] Vector3 Offset;
    // Update is called once per frame
    void Update() {
        transform.position = Follow.transform.position + Offset;
        // transform.rotation = follow.transform.rotation;
    }
}
