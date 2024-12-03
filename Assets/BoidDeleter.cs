using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidDeleter : MonoBehaviour
{
    public GameObject Fishies;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("Destroyer!");

            Destroy(Fishies);
        Destroy(gameObject);
        }
    }
}
