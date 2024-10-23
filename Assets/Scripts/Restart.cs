using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour {
    void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Hand")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
