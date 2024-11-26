using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using System.Numerics;
using UnityEngine.XR.Content.Interaction;
public class Gauge : MonoBehaviour {
    [SerializeField] float GaugeValue;
    [SerializeField] float CorrectGauge;
    [SerializeField] float GaugeThreshold; //how far off it can be while being correct;

    [SerializeField] GameObject Pin;
    [SerializeField] float noise;

    [SerializeField] List<Valve> Valves;
    public bool MiniGameStarted; 
    [SerializeField] List<float> ValveValues;
    [SerializeField] bool Completed;
    void Start() {
        GaugeValue = 0;
        MiniGameStarted = false;
    }

    void Update() {
    }

    void FixedUpdate(){
        if (!MiniGameStarted && GameEventManager.INSTANCE.WeldingDone) {
            MiniGameStarted = true;
            foreach (Valve _valve in Valves) {
                _valve.gameObject.GetComponent<XRKnob>().enabled = true;
            }
        }
        if (!MiniGameStarted) {
            return;
        }
        float randomNoise = UnityEngine.Random.Range(-noise,noise);
        
        Pin.transform.localEulerAngles = new UnityEngine.Vector3(0f, GaugeValue*360f+randomNoise, 0f);
    }

    public void ChangeGaugeValue(int valve, float input){
        if (Completed) {
            GameEventManager.INSTANCE.GaugeDone = true;
            return;
        }
        ValveValues[valve-1] = input;
        GaugeValue = 0;
        foreach(float value in ValveValues){
            GaugeValue += value;
        }
        if (GaugeValue > CorrectGauge-GaugeThreshold && GaugeValue < CorrectGauge+GaugeThreshold){
            Completed = true;
            DisableValves();
            GameEventManager.INSTANCE.GaugeDone = true;
        }
    }

    void DisableValves(){
        foreach (Valve valve in Valves) {
            valve.gameObject.GetComponent<XRKnob>().enabled = false;
        }
    }
}
