using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameEventManager : MonoBehaviour {
    public static GameEventManager INSTANCE;
    public bool WeldingDone;
    public bool GaugeDone;
    public bool BrokenReached;
    public bool InsideDivingBellDone;
    [SerializeField] List<WeldingArea> AllWeldAreas;
    bool AllWeldsCompleted;
    public static event Action CompleteWeldingEvent;
    public static void OnCompleteWeldingEvent() => CompleteWeldingEvent?.Invoke();
    public static event Action BrokenEvent;
    public static void OnBrokenEvent() => BrokenEvent?.Invoke();
    void Start() {
        INSTANCE = this;
        AllWeldsCompleted = false;
    }

    void Update() {
        bool AllFixed = true;
        if (!AllWeldsCompleted){
            if (AllWeldAreas.Count == 0) return;
            foreach(WeldingArea area in AllWeldAreas) {
                if (!area.Completed) {
                    AllFixed = false;
                }
            }
        }
        if (AllFixed && !WeldingDone){
            //indsæt kode der skal køres når welding er færdig 
            CompleteWeldingEvent.Invoke();
            AllWeldsCompleted = true;
            WeldingDone = true;
        }
    }
    public void StartBrokenSequence(){
        if (BrokenReached && !InsideDivingBellDone) {
            InsideDivingBellDone = true;
            BrokenEvent?.Invoke();
        }
    }
}
