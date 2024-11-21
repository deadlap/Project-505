using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
    [SerializeField] float MaxCorrectValue;
    [SerializeField] float MinCorrectValue;
    float MaxOutput;
    int ID;
    [SerializeField] Gauge Gauge;

    void Start(){
        MaxOutput = 0.33f;
    }

    public void ChangeValue(float input){
        float input_ = (float)input; Debug.Log(input);
        float output = 0;
        if (input_ < MinCorrectValue) {
            output = MaxOutput * (MinCorrectValue/input_);
        } else if (input_ > MaxCorrectValue) {
            output = MaxOutput * (input_/MaxCorrectValue);
        } else {
            output = MaxOutput;
        }
        Gauge.ChangeGaugeValue(ID, output);

        // float newValue = (float)input/();
    }

}
