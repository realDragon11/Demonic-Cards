using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public ResistMap rMap = new ResistMap();

    private float timeLeft = 100f;

    public float getTimeLeft(){
        return timeLeft;
    }

    public void setTimeLeft(float f){
        timeLeft = f;
    }

    public bool advanceTime(float f){
        timeLeft-=f;
        return timeLeft < 0;
    }

}
