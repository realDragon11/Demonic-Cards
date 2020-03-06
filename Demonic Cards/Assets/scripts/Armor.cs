using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    public ResistMap resist;
    public List<Card> cards = new List<Card>();
 public Armor(Sprite s,string iname2, string desc2, ItemSubType ist, ResistMap r, Card c1, Card c2){
     sprite = s;
     this.iname = iname2;
     this.desc = desc2;
     this.type = ItemType.ARMOR;
     this.subType = ist;
     resist = r;
     cards.Add(c1);
     cards.Add(c2);
 }   
}
