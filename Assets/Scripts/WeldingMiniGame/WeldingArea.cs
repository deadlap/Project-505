using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingArea : MonoBehaviour {
    [SerializeField] List<WeldingPoint> points;
    [SerializeField] GameObject GasBubbleVFX;
    public bool Completed;

    void Start() {
        for (int i = 0; i < transform.childCount; i++) {
            points.Add(transform.GetChild(i).GetComponent<WeldingPoint>());
        }
    }

    void Update() {
        bool AllFixed = true;
        bool vfxPointSet = false;
        if (!Completed){
            // Disallow the foreach loop to run before all WeldingPoints are added in Start().
            if(transform.childCount != points.Count) return;
            foreach(WeldingPoint point in points) {
                if (!point.IsFixed) {
                    AllFixed = false;
                    if (!vfxPointSet){
                        GasBubbleVFX.transform.position = point.gameObject.transform.position;
                        vfxPointSet = true;
                    }
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
        Destroy(GasBubbleVFX);
        //do stuff and animation things
    }
}
