using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeldingPoint : MonoBehaviour {
    
    [Header("Create Weld")]
    [SerializeField] GameObject weldPrefab;
    [SerializeField] float weldHeight = 0.01f;
    [SerializeField] float minScale, maxScale;
    [SerializeField] bool testWeld;

    public bool IsFixed;
    void Start() {
        IsFixed = false;
    }

    // For testing purposes.
    void Update()
    {
        if (!testWeld) return;
        SpawnWeld();
    }

    void SpawnWeld() {
        // Create a weld with a random size.
        if (IsFixed) return;
        GameObject newWeld = Instantiate(weldPrefab, transform.position, Quaternion.identity);
        newWeld.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
        newWeld.transform.localScale = new Vector3(Random.Range(minScale, maxScale), weldHeight, Random.Range(minScale, maxScale));
        IsFixed = true;            
    }
    // void Fix(){
    //     //Run Funny animations;
    // }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("WeldingTip") && (other.gameObject.GetComponent<WeldingContact>().WelderGun.Activated)){
            SpawnWeld();
        }
    }
}
