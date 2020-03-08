using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using System;

public class Handler : MonoBehaviour
{
    public static List<Being> beingList = new List<Being>();
    public static Handler h;
    public GameObject ct;
    public GameObject athing;
    public TMP_Dropdown cardSelector;
    public Vector3 mouseScreenPoint;
    public GameObject activeUnit;

    public int xSize, ySize;
    public Room r;

    public TMP_Text endTurnButton;
    public Tile selectedTile;

    public Being sortBeingList(){
        beingList.Sort(new BeingComparator());
        return beingList[0];
    }

    public void nextTurn(){
        Being.turnWho = sortBeingList();
        Being.turnWho.setTurn();
    }

    public void selectTile(Tile tile)
    {
        selectedTile = tile;
    }

    void Awake(){
        h = this;
        r = new Room(xSize,ySize);
        beingList.Add(BeingFactory.generatePlayer(1,r.getTile(4,2)));
        beingList.Add(BeingFactory.generatePlayer(2,r.getTile(4,3)));
        beingList.Add(BeingFactory.generatePlayer(3,r.getTile(4,4)));
        beingList.Add(BeingFactory.generatePlayer(4,r.getTile(4,5)));
        beingList.Add(BeingFactory.generateShambler(r.getTile(5,4)));
    }

    void Start(){//use awake to put things into the beinglist
        h = this;
        //while (true){
            sortBeingList().setTurn();
       // }
    }

    void FixedUpdate(){
         mouseScreenPoint = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
        endTurnButton.text = "End Turn\nActions Left: " + Being.turnWho.getActionsLeft();
        if (Vector2.Distance(Input.mousePosition,new Vector2(Camera.main.pixelWidth/2,Camera.main.pixelHeight/2)) > 300){
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);}
        activeUnit.transform.position = new Vector3(Being.turnWho.getTile().getX(),Being.turnWho.getTile().getY(),-3);
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
        r.selects();
    }

    /** unity random functions are bad and hard to use -Brian M.**/
    public static int randRange(int i, int j) {
		return (int)(UnityEngine.Random.Range(i,j+1));
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
