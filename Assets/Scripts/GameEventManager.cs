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
    public static event Action CompleteGaugeEvent;
    public static void OnCompleteGaugeEvent() => CompleteGaugeEvent?.Invoke();
    public static event Action BrokenEvent;
    public static void OnBrokenEvent() => BrokenEvent?.Invoke();
    public static event Action EndEvent;
    public static void OnEndEvent() => EndEvent?.Invoke();
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
    public void CompleteGauge(){
        if (!GaugeDone) {
            GaugeDone = true;
            CompleteGaugeEvent?.Invoke();
        }
    }
    public void StartBrokenSequence(){
        if (BrokenReached) {
            BrokenEvent?.Invoke();
        }
    }
    public void StartEndSequence(){
        if (BrokenReached && !InsideDivingBellDone) {
            InsideDivingBellDone = true;
            EndEvent?.Invoke();
        }
    }
}
