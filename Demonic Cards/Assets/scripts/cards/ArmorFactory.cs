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
            case "Brain Bucket":
            return getBrainBucket();
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
    public static Armor getBrainBucket(){
        if (mine_helm == null){
            mine_helm = new Armor(null,"Brain Bucket","Keeps your head on.",ItemSubType.HEAD_ARMOR,new ResistMap(),new HunkerDown(),new HunkerDown());
            mine_helm.resist.addResist(DamageType.BLUNT,.05f);
            mine_helm.resist.addResist(DamageType.PIERCE,.15f);
            mine_helm.resist.addResist(DamageType.SLASH,.1f);
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
        return target.occupant.stunAmount(20f)/2;
    }
}

public class HunkerDown : Card
{
    public HunkerDown(){
        cName = "Hunker Down";
        aiDisabled = true;
        tarhint = TargetHint.ALLY;
    }

    public override float getFitness(Being user, Tile target)
    {
        return .5f;
    }

    public override TileSet getTileSet()
    {
        TileSet t = new TileSet();
        t.tos.Add(new TileOffset(0,0));
        return t;
    }


    public override void use(Being user, Tile target)
    {
        Buff b = new Buff();
        b.rMap.addResist(DamageType.BLUNT,.1f);
        b.rMap.addResist(DamageType.PIERCE,.1f);
        b.rMap.addResist(DamageType.SLASH,.1f);
        b.rMap.addResist(DamageType.REND,.05f);
        b.setTimeLeft(200);
        user.buffs.Add(b);
    }
}