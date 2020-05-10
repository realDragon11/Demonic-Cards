using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using System;
using UnityEngine.SceneManagement;

public class Handler : MonoBehaviour
{

    public static void logA(String s,Sprite i){
        h.log.Insert(0,s);
        h.logImage.Insert(0,i);
    }

    public static void loadLog(){
        h.logDrop.ClearOptions();
        List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
        for (int i = 0;i < h.log.Count;i++)
        {
            TMP_Dropdown.OptionData data =  new TMP_Dropdown.OptionData();
            //data.image = c.image;
            data.text = h.log[i];
            data.image = h.logImage[i];
            list.Add(data);
        }
        h.logDrop.AddOptions(list);
    }

    public TMP_Dropdown logDrop;
    public static List<Being> beingList = new List<Being>();
    public List<String> log = new List<String>();
    public List<Sprite> logImage = new List<Sprite>();
    public static Handler h;
    public GameObject ct;
    public GameObject athing;
    public TMP_Dropdown cardSelector;
    public Vector3 mouseScreenPoint;
    public GameObject activeUnit;
    public int level;

    public int xSize, ySize;
    public Room r;

    public TMP_Text endTurnButton;
    public Tile selectedTile;

    public Being sortBeingList(){
        beingList.Sort(new BeingComparator());
        return beingList[0];
    }

    public void kill(Being b){
        beingList.Remove(b);
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
        beingList.Clear();
        level = PlayerPrefs.GetInt("curLevel");
        h = this;
        r = new Room(xSize,ySize);
        beingList.Add(BeingFactory.generatePlayer(1,r.getTile(4,2)));
        beingList.Add(BeingFactory.generatePlayer(2,r.getTile(4,3)));
        beingList.Add(BeingFactory.generatePlayer(3,r.getTile(4,4)));
        beingList.Add(BeingFactory.generatePlayer(4,r.getTile(4,5)));
        switch (level){
        case 0: 
        beingList.Add(BeingFactory.generateShambler(r.getTile(5,4)));
        beingList.Add(BeingFactory.generateShambler(r.getTile(7,5)));
        beingList.Add(BeingFactory.generateShambler(r.getTile(8,3)));
        beingList.Add(BeingFactory.generateGoo(r.getTile(9,7)));
        break;
        case 1: 
        beingList.Add(BeingFactory.generatePitchfork(r.getTile(8,4)));
        beingList.Add(BeingFactory.generatePitchfork(r.getTile(8,5)));
        beingList.Add(BeingFactory.generateImp(r.getTile(8,3)));
        beingList.Add(BeingFactory.generateImp(r.getTile(8,7)));
        break;
        case 2: 
        beingList.Add(BeingFactory.generateImp(r.getTile(8,4)));
        beingList.Add(BeingFactory.generateImp(r.getTile(8,5)));
        beingList.Add(BeingFactory.generateFlameDemon(r.getTile(8,3)));
        beingList.Add(BeingFactory.generateFlameDemon(r.getTile(8,7)));
        break;
        case 3: 
        beingList.Add(BeingFactory.generateBat(r.getTile(6,4)));
        beingList.Add(BeingFactory.generateBat(r.getTile(6,5)));
        beingList.Add(BeingFactory.generateBat(r.getTile(6,3)));
        beingList.Add(BeingFactory.generateBat(r.getTile(6,7)));
        beingList.Add(BeingFactory.generateBat(r.getTile(8,4)));
        beingList.Add(BeingFactory.generateBat(r.getTile(8,5)));
        beingList.Add(BeingFactory.generateBat(r.getTile(8,3)));
        beingList.Add(BeingFactory.generateBat(r.getTile(8,7)));
        break;
        }
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
        bool hasPlayer = false;
        bool hasFoe = false;
        foreach (Being b in beingList)
        {
            b.advanceTime(t);
            if (b.getSide().Equals(Side.PLAYER)){
                hasPlayer = true;
            }else{
                hasFoe = true;
            }
        }
        if (hasFoe == false){
            nextLevel();
        }else{
            if (hasPlayer == false){
                resetGame();
            }
        }
    }
    public static void resetGame(){
        PlayerPrefs.SetInt("curLevel",0);
        SceneManager.LoadScene(0);
    }
    public static void nextLevel(){
        PlayerPrefs.SetInt("curLevel",h.level+1);
        SceneManager.LoadScene(1);
    }

    public static void clickTile(Tile t){
        if (Being.turnWho.getSide() == Side.PLAYER){
            loadLog();
            Being.turnWho.attemptToUseCard(Being.turnWho.curCard,t);
        }
        foreach (Being item in beingList)
        {
            item.checkDeadRemove();
        }
    }

    public void clickEndTurn(){
        if (Being.turnWho.getSide() == Side.PLAYER){
            nextTurn();
        }
    }

    public void displayCards(List<Card> cars){
        loadLog();
        cardSelector.ClearOptions();
        List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();
        foreach (Card c in cars)
        {
            TMP_Dropdown.OptionData data =  new TMP_Dropdown.OptionData();
            //data.image = c.image;
            data.text = c.getName();
            data.image = c.image;
            list.Add(data);
        }
        cardSelector.AddOptions(list);
        selectCard();
    }

    public void selectCard(){
        Being.turnWho.selectCard(cardSelector.value);
        r.selects();
    }

    /** unity random functions are bad and hard to use -Brian M.**/
    public static int randRange(int i, int j) {
		return (int)(UnityEngine.Random.Range(i,j+1));
	}

    public Being getRandomPlayer(){
        List<Being> pList = new List<Being>();
        foreach(Being b in beingList){
            if (b.getSide().Equals(Side.PLAYER)){
                pList.Add(b);
            }
        }
        return pList[Handler.randRange(0,pList.Count-1)];
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


