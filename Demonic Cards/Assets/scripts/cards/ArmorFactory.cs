using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorFactory
{

    public static Armor getGenericArmor(ItemSubType ist){
       return new Armor(null,"Generic Armor","Placeholder",ist,new ResistMap(),new BlankArmorCard(),new BlankArmorCard());
    }

    public static Armor getArmorByName(string str){
        switch (str){
            case "Mining Helmet":
            return getMiningHelmet();
        }
        throw new GenericRuntimeException("Armor not found!");
    }

    private static Armor mine_helm = null;

    public static Armor getMiningHelmet(){
        if (mine_helm == null){
            mine_helm = new Armor(null,"Mining Helmet","Has a bright flash.",ItemSubType.HEAD_ARMOR,new ResistMap(),new MineHelmStun(),new MineHelmStun());
            mine_helm.resist.addResist(DamageType.BLUNT,.1f);
        }
        return mine_helm;
    }
}

public class BlankArmorCard : Card
{
    public BlankArmorCard(){
        cName = "Blank Armor";
        aiDisabled = true;
        tarhint = TargetHint.ALLY;
    }

    public override float getFitness(Being user, Tile target)
    {
        return 0f;
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

public class MineHelmStun : Card
{
    public MineHelmStun(){
        cName = "Flash Helmet";
        tarhint = TargetHint.ENEMY;
    }
    public override TileSet getTileSet()
    {
        TileSet t = new TileSet();
        t.tos.Add(new TileOffset(1,0));
        t.tos.Add(new TileOffset(1,-1));
        t.tos.Add(new TileOffset(1,1));
        return t;
    }


    public override void use(Being user, Tile target)
    {
        target.occupant.stun(20f);
    }

    public override float getFitness(Being user, Tile target)
    {
        return target.occupant.stunAmount(20f);
    }
}