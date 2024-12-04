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
    float CurrentWeldTimer;
    float MaxWeldTime;

    public bool IsFixed;
    void Start() {
        IsFixed = false;
        MaxWeldTime = .83f;
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
        newWeld.transform.rotation = transform.rotation;
        newWeld.transform.localScale = new Vector3(Random.Range(minScale, maxScale), weldHeight, Random.Range(minScale, maxScale));
        IsFixed = true;            
    }
    void OnTriggerStay(Collider other) {
        if (other.CompareTag("WeldingTip") && (other.gameObject.GetComponent<WeldingContact>().WelderGun.Activated)){
            if (IsFixed)
                return;
            CurrentWeldTimer += Time.deltaTime;
            if (MaxWeldTime <= CurrentWeldTimer) 
                SpawnWeld();
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("WeldingTip") && (other.gameObject.GetComponent<WeldingContact>().WelderGun.Activated)){
            // SpawnWeld();
            CurrentWeldTimer += Time.deltaTime;
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("WeldingTip") && (other.gameObject.GetComponent<WeldingContact>().WelderGun.Activated)){
            // SpawnWeld();
            CurrentWeldTimer = 0f;
        }
    }
}
