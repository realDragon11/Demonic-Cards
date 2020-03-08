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
        List<AIAction> removeList = new List<AIAction>();
        foreach (AIAction a in list)
        {
            if (list[0].fitness-a.fitness > .2f){
                removeList.Add(a);
            }
        }
        foreach (AIAction a in removeList)
        {
            list.Remove(a);
        }
        
        return list[Handler.randRange(0,list.Count-1)];
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
            if (c.tarhint != TargetHint.BLANK){
                if (c.tarhint == TargetHint.ALLY){
                    if (t.occupant != null && t.occupant.getSide() == b.getSide()){
                        fitness = c.getFitness(b,t);
                    }else{
                        fitness = -.8f;
                    }
                }else{
                    if (t.occupant != null && t.occupant.getSide() != b.getSide()){
                        fitness = c.getFitness(b,t);
                    }else{
                        fitness = -.8f;
                    }
                }
            }else{
                if (t.occupant == null){
            fitness = c.getFitness(b,t);}else{fitness = -.8f;}
            }
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
