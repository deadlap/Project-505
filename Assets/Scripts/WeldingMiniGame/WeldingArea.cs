using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingArea : MonoBehaviour {
    [SerializeField] List<WeldingPoint> points;
    bool Completed;

    void Start() {
        for (int i = 0; i < transform.childCount; i++) {
            points.Add(transform.GetChild(i).GetComponent<WeldingPoint>());
        }
    }

    void Update() {
        bool AllFixed = true;
        if (!Completed){
            // Disallow the foreach loop to run before all WeldingPoints are added in Start().
            if(transform.childCount != points.Count) return;
            foreach(WeldingPoint point in points){
                if (!point.IsFixed){
                    AllFixed = false;
                }
            }
            if (AllFixed){
                Fix();
            }
        }
    }

    void Fix(){
        if(Completed) return;
        print("completed");
        Completed = true;
        //do stuff and animation things
    }
}
