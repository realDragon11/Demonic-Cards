using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    public static List<Being> beingList = new List<Being>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Being sortBeingList(){
        beingList.Sort(new BeingComparator<Being>());
        return beingList[0];
    }
}

public class BeingComparator<Being> : Comparer<Being>{

    override public int Compare(Being x, Being y){
        if (x.getTimeTilAction());
        return 0;
    }
}
