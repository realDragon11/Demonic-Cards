using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Handler : MonoBehaviour
{
    public static List<Being> beingList = new List<Being>();
    public static Handler h;
    public GameObject ct;
    public GameObject athing;
    public TMP_Dropdown cardSelector;

    public int xSize, ySize;
    public Room r;

    public TMP_Text endTurnButton;

    public Being sortBeingList(){
        beingList.Sort(new BeingComparator());
        return beingList[0];
    }

    public void nextTurn(){
        Being.turnWho = sortBeingList();
        Being.turnWho.setTurn();
    }

    void Awake(){
        h = this;
        r = new Room(xSize,ySize);
        beingList.Add(BeingFactory.generateGenericBeing(Side.PLAYER,r.getTile(4,4)));
        beingList.Add(BeingFactory.generateGenericBeing(Side.DEMONS,r.getTile(5,4)));
        beingList[0].setAgilityMult(1.2f);
    }

    void Start(){//use awake to put things into the beinglist
        h = this;
        //while (true){
            sortBeingList().setTurn();
       // }
    }

    void FixedUpdate(){
        endTurnButton.text = "End Turn\nActions Left: " + Being.turnWho.getActionsLeft();
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

    public void clickEndTurn(){
        if (Being.turnWho.getSide() == Side.PLAYER){
            nextTurn();
        }
    }

    public void displayCards(List<Card> cars){
        cardSelector.ClearOptions();
        List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
        foreach (Card c in cars)
        {
            TMP_Dropdown.OptionData data =  new TMP_Dropdown.OptionData();
            //data.image = c.image;
            data.text = c.getName();
            list.Add(data);
        }
        cardSelector.AddOptions(list);
    }

    public void selectCard(){
        Being.turnWho.selectCard(cardSelector.value);
    }

    /** My handy custom random function I use in all my java projects -Brian M.**/
    public static int randRange(int i, int j) {
		return (int)(Random.Range(0,1)*(j+1-i))+i;
	}
}

public class BeingComparator : Comparer<Being>{

    override public int Compare(Being x, Being y){
        if (x.timeTilAction() == y.timeTilAction()){
        return 0;}
        if (x.timeTilAction() > y.timeTilAction()){//might be sorting in the wrong order
        return 1;
        }
        return -1;
    }
}
