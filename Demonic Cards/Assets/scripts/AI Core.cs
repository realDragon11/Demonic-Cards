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
                list.Add(new AIAction(b,c,Handler.h.r.getTileRelative(b.getTile(),t)));
            }         
        }
        return list;
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
