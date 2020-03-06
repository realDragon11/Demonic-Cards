using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{

    public List<Card> cards = new List<Card>();
    public Weapon(string iname2, string desc2, ItemSubType ist, ResistMap r, Card c1, Card c2, Card c3, Card c4, Card c5){
     this.iname = iname2;
     this.desc = desc2;
     this.type = ItemType.WEAPON;
     this.subType = ist;
     cards.Add(c1);
     cards.Add(c2);
     cards.Add(c3);
     cards.Add(c4);
     cards.Add(c5);
 }   
}
