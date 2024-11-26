using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using System.Numerics;
using UnityEngine.XR.Content.Interaction;
public class Gauge : MonoBehaviour {

    // public static event Action<string> ReceiveSpecialSignEvent;
    // public static void ReceiveSpecialSign(string sign) => ReceiveSpecialSignEvent?.Invoke(sign);

    [SerializeField] float GaugeValue;
    [SerializeField] float CorrectGauge;
    [SerializeField] float GaugeThreshold; //how far off it can be while being correct;

    [SerializeField] GameObject Pin;
    [SerializeField] float noise;

    [SerializeField] List<Valve> Valves;
    [SerializeField] List<float> ValveValues;
    [SerializeField] bool Completed;

    void Start() {
        GaugeValue = 0;
        // ValveValues = new List<float>(){0,0,0};
    }

    void Update() {
    }

    void FixedUpdate(){
        float randomNoise = UnityEngine.Random.Range(-noise,noise);
        
        Pin.transform.localEulerAngles = new UnityEngine.Vector3(0f, GaugeValue*360f+randomNoise, 0f);
    }

    public void ChangeGaugeValue(int valve, float input){
        if (Completed)
            return;
        ValveValues[valve-1] = input;
        GaugeValue = 0;
        foreach(float value in ValveValues){
            GaugeValue += value;
        }
        if (GaugeValue > CorrectGauge-GaugeThreshold && GaugeValue < CorrectGauge+GaugeThreshold){
            Completed = true;
            DisableValves();
            CompletePuzzle();
        }
    }

    void DisableValves(){
        foreach (Valve valve in Valves) {
            valve.gameObject.GetComponent<XRKnob>().enabled = false;
        }
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