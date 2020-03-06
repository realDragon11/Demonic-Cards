using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingFactory
{

    public static Being generateGenericBeing(Side s, Tile t){
        Being b = new Being();
        b.setSide(s);
        b.curTile = t;
        return b;
    }
}
