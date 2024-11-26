using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameEventManager : MonoBehaviour {
    public static GameEventManager INSTANCE;
    public bool TutorialDone;
    public bool RadioCutOutDone;
    public bool WeldingDone;
    public bool RadioRestablishedDone;
    public bool GaugeDone;
    public bool RadioCutOut2Done;
    public bool TentaclePipeDone;
    public bool RadioRestablished2Done;
    public bool InsideDivingBellDone;
    [SerializeField] List<WeldingArea> AllWeldAreas;

    bool AllWeldsCompleted;

    // public static event Action<string> CompleteTaskEvent;
    // public static void OnCompleteTaskEvent(string task) => CompleteTaskEvent?.Invoke(task);
    
    void Start(){
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
        if (AllFixed){
            WeldingDone = true;
        }
    }
    
    // void CompleteTask(string task) {
    //     switch(task.ToLower()){
    //         case "tutorial":
    //             TutorialDone = true;
    //             break;
    //         case "radiocut1":
    //             RadioCutOutDone = true;
    //             break;
    //         case "welding":
    //             WeldingDone = true;
    //             break;
    //         case "radioestablished1":
    //             RadioRestablishedDone = true;
    //             break;
    //         case "gauge":
    //             GaugeDone = true;
    //             break;
    //         case "radiocut2":
    //             RadioCutOut2Done = true;
    //             break;
    //         case "tentaclepipe":
    //             TentaclePipeDone = true;
    //             break;
    //         case "radioestablished2":
    //             RadioRestablished2Done = true;
    //             break;
    //         case "backsafe":
    //             InsideDivingBellDone = true;
    //             break;
    //     }
    // }

}
