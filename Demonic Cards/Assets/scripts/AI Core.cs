using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICore : MonoBehaviour
{

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
        fitness = c.getFitness(b,t);
    }
}
