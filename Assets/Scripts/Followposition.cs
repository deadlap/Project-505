using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Followposition : MonoBehaviour {
    [SerializeField] GameObject Follow;
    void Update() {
        transform.position = Follow.transform.position;
    }
}
