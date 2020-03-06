using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICore
{

    public static List<AIAction> oneDeep(Being b){
        List<AIAction> list = new List<AIAction>();
        foreach (Card c in b.cards)
        {
            foreach (TileOffset t in c.getTileSet().tos)
            {
                list.Add(new AIAction(b,c,Handler.h.r.getTileRelative(b.getTile(),t.flipIf(b.getSide() != Side.PLAYER))));
            }         
        }
        return list;
    }

    public static AIAction decide(List<AIAction> list){
        list.Sort(new AIActionComaparator());
        return list[0];
    }
}

public class AIAction{

    public float fitness = 0f;
    public Card card;
    public Being actor;
    public Tile target;
    public AIAction(Being b, Card c, Tile t){
        actor = b;
        card = c;
        target = t;
        try
        {
            fitness = c.getFitness(b,t);
        }
        catch (System.Exception)
        {
            fitness = -1f;
        }
        
    }
}


public class AIActionComaparator : Comparer<AIAction>{

    override public int Compare(AIAction x, AIAction y){
        if (x.fitness == y.fitness){
        return 0;}
        if (x.fitness < y.fitness){//might be sorting in the wrong order
        return 1;
        }
        return -1;
    }
}
