using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    public ResistMap resist;
 public Armor(ItemSubType ist, ResistMap r){
     this.type = ItemType.ARMOR;
     this.subType = ist;
     resist = r;
 }   
}
