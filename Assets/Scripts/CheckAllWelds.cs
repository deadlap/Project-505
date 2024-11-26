using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckAllWelds : MonoBehaviour {
    [SerializeField] List<WeldingArea> AllAreas;
    bool AllCompleted;
    void Start(){
        AllCompleted = false;
    }
    void Update() {
        bool AllFixed = true;
        if (!AllCompleted){
            if (AllAreas.Count == 0) return;
            foreach(WeldingArea area in AllAreas) {
                if (!area.Completed) {
                    AllFixed = false;
                }
            }
        }
        if (AllFixed){
            GameEventManager.INSTANCE.WeldingDone = true;
        }
    }
}
