using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorFactory
{

    public static Armor getGenericArmor(ItemSubType ist){
       return new Armor(null,"Generic Armor","Placeholder",ist,new ResistMap(),new BlankArmorCard(),new BlankArmorCard());
    }
}

public class BlankArmorCard : Card
{
    public BlankArmorCard(){
        cName = "Blank Armor";
    }
    public override TileSet getTileSet()
    {
        TileSet t = new TileSet();
        t.tos.Add(new TileOffset(0,0));
        return t;
    }


    public override void use(Being user, Tile target)
    {

    }
}