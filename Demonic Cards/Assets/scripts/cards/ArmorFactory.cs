﻿using System;
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
            case "Burster Core":
            return getBursterCore();
            case "Traction Shoes":
            return getTractionShoes();
            case "Combat Boots":
            return getCombatBoots();
            case "Basic Greaves":
            return getBasicGreaves();
            case "Combat Grip":
            return getCombatGrip();
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
    private static Armor brain_bucket;
    public static Armor getBrainBucket(){
        if (brain_bucket == null){
            brain_bucket = new Armor(null,"Brain Bucket","Keeps your head on.",ItemSubType.HEAD_ARMOR,new ResistMap(),new HunkerDown(),new HunkerDown());
            brain_bucket.resist.addResist(DamageType.BLUNT,.05f);
            brain_bucket.resist.addResist(DamageType.PIERCE,.15f);
            brain_bucket.resist.addResist(DamageType.SLASH,.1f);
        }
        return brain_bucket;
    }
    private static Armor burster_core;
    public static Armor getBursterCore(){
        if (burster_core == null){
            burster_core = new Armor(null,"Burster Core","OMG LAZORS.",ItemSubType.HEAD_ARMOR,new ResistMap(),new BusterCoreBurst(),new BusterCoreBurst());
            burster_core.resist.addResist(DamageType.FIRE,.15f);
            burster_core.resist.addResist(DamageType.ICE,.1f);
            burster_core.resist.addResist(DamageType.ELEC,-.1f);
        }
        return burster_core;
    }
    private static Armor traction_shoes;
    public static Armor getTractionShoes(){
        if (traction_shoes == null){
            traction_shoes = new Armor(null,"Burster Core","Prevent you from being knocked back.",ItemSubType.FEET_ARMOR,new ResistMap(),new HunkerDown(),new HunkerDown());
        }
        return traction_shoes;
    }
    private static Armor combat_boots;
    public static Armor getCombatBoots(){
        if (combat_boots == null){
            combat_boots = new Armor(null,"Combat Boots","Keeps your feet on.",ItemSubType.FEET_ARMOR,new ResistMap(),new HunkerDown(),new HunkerDown());
            combat_boots.resist.addResist(DamageType.BLUNT,.04f);
            combat_boots.resist.addResist(DamageType.PIERCE,.04f);
            combat_boots.resist.addResist(DamageType.SLASH,.04f);
        }
        return combat_boots;
    }
    private static Armor basic_greaves;
    public static Armor getBasicGreaves(){
        if (basic_greaves == null){
            basic_greaves = new Armor(null,"Basic Greaves","Keeps your legs on.",ItemSubType.LEG_ARMOR,new ResistMap(),new HunkerDown(),new HunkerDown());
            basic_greaves.resist.addResist(DamageType.BLUNT,.1f);
            basic_greaves.resist.addResist(DamageType.PIERCE,.1f);
            basic_greaves.resist.addResist(DamageType.SLASH,.1f);
        }
        return basic_greaves;
    }
    private static Armor combat_grip;
    public static Armor getCombatGrip(){
        if (combat_grip == null){
            combat_grip = new Armor(null,"Combat Grip","Keeps your arms on.",ItemSubType.ARM_ARMOR,new ResistMap(),new HunkerDown(),new HunkerDown());
            combat_grip.resist.addResist(DamageType.BLUNT,.7f);
            combat_grip.resist.addResist(DamageType.PIERCE,.7f);
            combat_grip.resist.addResist(DamageType.SLASH,.7f);
        }
        return combat_grip;
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

public class BusterCoreBurst : Card
{
    public BusterCoreBurst(){
        cName = "Burst Core";
        tarhint = TargetHint.ENEMY;
    }

    

    public override TileSet getTileSet()
    {
        TileSet t = new TileSet();
        t.tos.Add(new TileOffset(1,0));
        return t;
    }


    public override void use(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(10f,DamageType.FIRE));
        target.occupant.damage(a);
    }
    public override float getFitness(Being user, Tile target)
    {
        if (user.getSide() == target.occupant.getSide()){
            return -1f;
        }
        Attack a = new Attack();
        a.dams.Add(new Damage(10f,DamageType.FIRE));
        return target.occupant.getDamageAmount(a);
    }
}