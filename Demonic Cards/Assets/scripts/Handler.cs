using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    public static List<Being> beingList = new List<Being>();

    public Being sortBeingList(){
        beingList.Sort(new BeingComparator());
        return beingList[0];
    }
}

public class BeingComparator : Comparer<Being>{

    override public int Compare(Being x, Being y){
        if (x.timeTilAction() == y.timeTilAction()){
        return 0;}
        if (x.timeTilAction() < y.timeTilAction()){
        return 1;
        }
        return -1;
    }
}
