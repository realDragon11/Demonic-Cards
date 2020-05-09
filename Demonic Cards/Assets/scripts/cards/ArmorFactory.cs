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
            case "Hazmat Suit":
            return getHazmat();
        }
        throw new GenericRuntimeException("Armor not found!");
    }

    private static Armor mine_helm = null;

    public static Armor getMiningHelmet(){
        if (mine_helm == null){
            mine_helm = new Armor(Resources.Load<Sprite>("sprites/mining-helmet"),"Mining Helmet","Has a bright flash.",ItemSubType.HEAD_ARMOR,new ResistMap(),new MineHelmStun(),new MineHelmStun());
            mine_helm.resist.addResist(DamageType.BLUNT,.1f);
        }
        return mine_helm;
    }
    private static Armor brain_bucket;
    public static Armor getBrainBucket(){
        if (brain_bucket == null){
            brain_bucket = new Armor(Resources.Load<Sprite>("sprites/full-metal-bucket"),"Brain Bucket","Keeps your head on.",ItemSubType.HEAD_ARMOR,new ResistMap(),new HunkerDown(),new HunkerDown());
            brain_bucket.resist.addResist(DamageType.BLUNT,.05f);
            brain_bucket.resist.addResist(DamageType.PIERCE,.15f);
            brain_bucket.resist.addResist(DamageType.SLASH,.1f);
        }
        return brain_bucket;
    }
    private static Armor burster_core;
    public static Armor getBursterCore(){
        if (burster_core == null){
            burster_core = new Armor(Resources.Load<Sprite>("sprites/laser-warning-icon"),"Burster Core","OMG LAZORS.",ItemSubType.HEAD_ARMOR,new ResistMap(),new BusterCoreBurst(),new BusterCoreBurst());
            burster_core.resist.addResist(DamageType.FIRE,.15f);
            burster_core.resist.addResist(DamageType.ICE,.1f);
            burster_core.resist.addResist(DamageType.ELEC,-.1f);
        }
        return burster_core;
    }
    private static Armor traction_shoes;
    public static Armor getTractionShoes(){
        if (traction_shoes == null){
            traction_shoes = new Armor(Resources.Load<Sprite>("sprites/boot-stomp"),"Traction Shoes","Prevent you from being knocked back.",ItemSubType.FEET_ARMOR,new ResistMap(),new HunkerDown(),new HunkerDown());
        }
        return traction_shoes;
    }
    private static Armor combat_boots;
    public static Armor getCombatBoots(){
        if (combat_boots == null){
            combat_boots = new Armor(Resources.Load<Sprite>("sprites/boots"),"Combat Boots","Keeps your feet on.",ItemSubType.FEET_ARMOR,new ResistMap(),new HunkerDown(),new HunkerDown());
            combat_boots.resist.addResist(DamageType.BLUNT,.04f);
            combat_boots.resist.addResist(DamageType.PIERCE,.04f);
            combat_boots.resist.addResist(DamageType.SLASH,.04f);
        }
        return combat_boots;
    }
    private static Armor basic_greaves;
    public static Armor getBasicGreaves(){
        if (basic_greaves == null){
            basic_greaves = new Armor(Resources.Load<Sprite>("sprites/armored-pants"),"Basic Greaves","Keeps your legs on.",ItemSubType.LEG_ARMOR,new ResistMap(),new HunkerDown(),new HunkerDown());
            basic_greaves.resist.addResist(DamageType.BLUNT,.1f);
            basic_greaves.resist.addResist(DamageType.PIERCE,.1f);
            basic_greaves.resist.addResist(DamageType.SLASH,.1f);
        }
        return basic_greaves;
    }
    private static Armor combat_grip;
    public static Armor getCombatGrip(){
        if (combat_grip == null){
            combat_grip = new Armor(Resources.Load<Sprite>("sprites/arm"),"Combat Grip","Keeps your arms on.",ItemSubType.ARM_ARMOR,new ResistMap(),new HunkerDown(),new HunkerDown());
            combat_grip.resist.addResist(DamageType.BLUNT,.1f);
            combat_grip.resist.addResist(DamageType.PIERCE,.1f);
            combat_grip.resist.addResist(DamageType.SLASH,.1f);
        }
        return combat_grip;
    }
    private static Armor hazmat;
    public static Armor getHazmat(){
        if (hazmat == null){
            hazmat = new Armor(Resources.Load<Sprite>("sprites/hazmat-suit"),"Hazmat Suit","Keeps your skin on.",ItemSubType.BODY_ARMOR,new ResistMap(),new HunkerDown(),new HunkerDown());
            hazmat.resist.addResist(DamageType.ACID,.5f);
            hazmat.resist.addResist(DamageType.FIRE,.5f);
            hazmat.resist.addResist(DamageType.ICE,.5f);
            hazmat.resist.addResist(DamageType.ELEC,.5f);
            hazmat.resist.addResist(DamageType.PIERCE,-.3f);
            hazmat.resist.addResist(DamageType.SLASH,-.3f);
        }
        return hazmat;
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
        image = Resources.Load<Sprite>("sprites/mining-helmet");
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
        image = Resources.Load<Sprite>("sprites/defensive-wall");
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
        image = Resources.Load<Sprite>("sprites/laser-sparks");
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