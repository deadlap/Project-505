using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingArea : MonoBehaviour {
    [SerializeField] List<WeldingPoint> points;
    bool Completed;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        bool AllFixed = true;
        if (!Completed){
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
        Completed = true;
        //do stuff and animation things
    }
}
