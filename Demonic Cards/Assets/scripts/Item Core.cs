using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected ItemType type;
    protected Sprite sprite;
    protected ItemSubType subType;
    protected string iname, desc;
    public ItemType getItemType(){
        return type;
    }
     public ItemSubType getItemSubType(){
        return subType;
    }

    public string getName(){
        return iname;
    }

    public string getDesc(){
        return desc;
    }
}


public enum ItemType{
    WEAPON, USABLE, ARMOR
}

public enum ItemSubType{
    MELEE_WEAPON, RANGED_WEAPON, HEALING_USABLE, HEAD_ARMOR,BODY_ARMOR,ARM_ARMOR,LEG_ARMOR,FEET_ARMOR
}