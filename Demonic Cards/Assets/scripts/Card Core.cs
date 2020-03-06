using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    //5 x2 weapon cards, 2 x5 armor cards, 5 class cards, 5 backstory cards
    //nts: make all cards singletons

    public abstract TileSet getTileSet();
    public abstract void use(Being user, Tile target);
    public abstract float getFitness(Being user, Tile target);
    public Sprite image;
    protected string cName;
    public bool aiDisabled = false;
    public string getName(){
        return cName;
    }

    public TargetHint tarhint;
}

public enum TargetHint{
    ENEMY, ALLY, BLANK
}

public class TileSet{

public bool blockedByAllies = true, blockedByWalls = true, blockedByEnemies = true;
public List<TileOffset> tos = new List<TileOffset>();
}

public class TileOffset{
    public int xOff, yOff;
    public TileOffset(int x,int y){
        xOff = x;
        yOff = y;
    }
}