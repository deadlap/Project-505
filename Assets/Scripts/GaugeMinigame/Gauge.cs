using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using System.Numerics;

public class Gauge : MonoBehaviour {

    // public static event Action<string> ReceiveSpecialSignEvent;
    // public static void ReceiveSpecialSign(string sign) => ReceiveSpecialSignEvent?.Invoke(sign);

    [SerializeField] float GaugeValue;
    [SerializeField] float CorrectGauge;
    [SerializeField] float GaugeThreshold; //how far off it can be while being correct;

    [SerializeField] GameObject Pin;
    [SerializeField] float noise;

    [SerializeField] List<Valve> Valves;
    [SerializeField] bool Completed;

    void Start() {
        GaugeValue = 0;
    }

    void Update() {
    }

    void FixedUpdate(){
        float randomNoise = UnityEngine.Random.Range(-noise,noise);
        Pin.transform.localEulerAngles = new UnityEngine.Vector3(0f, GaugeValue*360f+randomNoise, 0f);
    }

    public void ChangeGaugeValue(float input){
        if (Completed)
            return;
        GaugeValue += input;
        if (GaugeValue > CorrectGauge-GaugeThreshold && GaugeValue < CorrectGauge+GaugeThreshold){
            Completed = true;
            DisableValves();
        }
    }

    void DisableValves(){
        //do foreach loop to disable wheel spinning thing;
    }

    void CompletePuzzle(){
        //Run code to activate next story progression point;
    }
    
    // void OnEnable() {
    //     ReceiveSpecialSignEvent += ReceiveSign;
    //     EnableSpecialSignEvent += EnableSign;
    // }
    // void OnDisable() {
    //     ReceiveSpecialSignEvent -= ReceiveSign;
    //     EnableSpecialSignEvent -= EnableSign;
    // }
}
