using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingPoint : MonoBehaviour {
    // Start is called before the first frame update
    public bool IsFixed {get; private set;}
    void Start() {
        IsFixed = false;
    }

    // Update is called once per frame
    void Update() {
        
    }
    // void Fix(){
    //     //Run Funny animations;
    // }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("WeldingTip") && (other.gameObject.GetComponent<WeldingContact>().WelderGun.Activated)){
            IsFixed = true;            
        }
    }
}
