using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour {
    [SerializeField] float CorrectValue;
    [SerializeField] float Threshold;
    [SerializeField] GameObject Lamp;
    [SerializeField] float MaxOutput;
    [SerializeField] int ID;
    [SerializeField] Gauge Gauge;
    float CurrentValue;

    public void ChangeValue(float input){
        CurrentValue = input;

        float output = 0;
        if (input < CorrectValue) {
            output = MaxOutput * (input/CorrectValue);
        } else if (input > CorrectValue) {
            output = MaxOutput * (CorrectValue/(input));
        } else {
            output = MaxOutput;
        }
        Gauge.ChangeGaugeValue(ID, output);
    }
    void Update(){
        Lamp.SetActive((Math.Abs(CurrentValue-CorrectValue)<Threshold));
    }

}
