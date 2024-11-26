using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
    [SerializeField] float CorrectValue;
    // [SerializeField] float Threshold;
    [SerializeField] float MaxOutput;
    [SerializeField] int ID;
    [SerializeField] Gauge Gauge;

    public void ChangeValue(float input){

        float output = 0;
        if (input < CorrectValue) {
            output = MaxOutput * (input/CorrectValue);
        } else if (input > CorrectValue) {
            output = MaxOutput * (CorrectValue/(input));
        } else {
            output = MaxOutput;
        }
        Debug.Log("output" + output);
        Gauge.ChangeGaugeValue(ID, output);
    }

}
