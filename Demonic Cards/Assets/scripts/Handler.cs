using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    public static List<Being> beingList = new List<Being>();
    public static Handler h;

    public int xSize, ySize;
    public Room r;

    public Being sortBeingList(){
        beingList.Sort(new BeingComparator());
        return beingList[0];
    }

    void Awake(){
        r = new Room(xSize,ySize);
        beingList.Add(BeingFactory.generateGenericBeing(Side.PLAYER,r.getTile(4,4)));
    }

    void Start(){//use awake to put things into the beinglist
        h = this;
        //while (true){
            sortBeingList().setTurn();
       // }
    }

    public static void advanceTime(float t){
        foreach (Being b in beingList)
        {
            b.advanceTime(t);
        }
    }

    public static void clickTile(Tile t){
        if (Being.turnWho.getSide() == Side.PLAYER){
            Being.turnWho.attemptToUseCard(Being.turnWho.curCard,t);
        }
    }
}

public class BeingComparator : Comparer<Being>{

    override public int Compare(Being x, Being y){
        if (x.timeTilAction() == y.timeTilAction()){
        return 0;}
        if (x.timeTilAction() < y.timeTilAction()){//might be sorting in the wrong order
        return 1;
        }
        return -1;
    }
}
