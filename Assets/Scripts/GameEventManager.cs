using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameEventManager : MonoBehaviour {
    public static GameEventManager INSTANCE;
    public bool WeldingDone;
    public bool GaugeDone;
    public bool InsideDivingBellDone;
    [SerializeField] List<WeldingArea> AllWeldAreas;
    bool AllWeldsCompleted;

    // public static event Action<string> CompleteTaskEvent;
    // public static void OnCompleteTaskEvent(string task) => CompleteTaskEvent?.Invoke(task);

    void Start()
    {
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
            AllWeldsCompleted = true;
            WeldingDone = true;
        }
    }
}
