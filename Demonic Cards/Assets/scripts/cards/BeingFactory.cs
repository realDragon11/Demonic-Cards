using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingFactory
{

    public static Being generateGenericBeing(Side s, Tile t){
        Being b = new Being(s);
        Room.moveTo(b,t);
        return b;
    }

    public static Being generateShambler( Tile t){
        Being b = new Being(Side.DEMONS);
        Room.moveTo(b,t);
        b.moveCard = new ShamblerMoveCard();
        b.aWeap = WeaponFactory.getShamblerClaws();
        b.bWeap = WeaponFactory.getShamblerClaws();
        b.baseRMap.addResist(DamageType.FIRE,-.2f);
        b.baseRMap.addResist(DamageType.SLASH,-.1f);
        b.setSprite(Resources.Load<Sprite>("sprites/shambling-zombie"));
        b.maxHp = 30f;
        b.setHp(b.maxHp);
        b.name = "Shambler";
        return b;
    }//
    public static Being generateGoo( Tile t){
        Being b = new Being(Side.DEMONS);
        Room.moveTo(b,t);
        b.moveCard = new GooMoveCard();
        b.aWeap = WeaponFactory.getGoopThrow();
        b.bWeap = WeaponFactory.getGoopBash();
        b.baseRMap.addResist(DamageType.FIRE,-.3f);
        b.baseRMap.addResist(DamageType.SLASH,-.2f);
        b.baseRMap.addResist(DamageType.ACID,.5f);
        b.setSprite(Resources.Load<Sprite>("sprites/gooey-daemon"));
        b.maxHp = 20f;
        b.setHp(b.maxHp);
        b.name = "Goo Demon";
        return b;
    }
    public static Being generatePitchfork( Tile t){
        Being b = new Being(Side.DEMONS);
        Room.moveTo(b,t);
        b.moveCard = new PitchforkMoveCard();
        b.aWeap = WeaponFactory.getPitchfork();
        b.bWeap = WeaponFactory.getPitchfork();
        b.baseRMap.addResist(DamageType.FIRE,.1f);
        b.baseRMap.addResist(DamageType.HOLY,-.25f);
        b.setSprite(Resources.Load<Sprite>("sprites/evil-fork"));
        b.maxHp = 15f;
        b.setHp(b.maxHp);
        b.name = "Demonic Pitchfork";
        return b;
    }

    public static Being generateImp( Tile t){
        Being b = new Being(Side.DEMONS);
        Room.moveTo(b,t);
        b.moveCard = new PitchforkMoveCard();
        b.aWeap = WeaponFactory.getPitchfork();
        b.bWeap = WeaponFactory.getFireball();
        b.baseRMap.addResist(DamageType.FIRE,.45f);
        b.baseRMap.addResist(DamageType.HOLY,-.45f);
        b.setSprite(Resources.Load<Sprite>("sprites/imp"));
        b.maxHp = 25f;
        b.setHp(b.maxHp);
        b.name = "Imp";
        return b;
    }

    public static Being generateFlameDemon( Tile t){
        Being b = new Being(Side.DEMONS);
        Room.moveTo(b,t);
        b.moveCard = new NoMoveCard();
        b.aWeap = WeaponFactory.getFlameSummon();
        b.bWeap = WeaponFactory.getFireball();
        b.baseRMap.addResist(DamageType.FIRE,.70f);
        b.baseRMap.addResist(DamageType.HOLY,-.45f);
        b.setSprite(Resources.Load<Sprite>("sprites/ifrit"));
        b.maxHp = 35f;
        b.setHp(b.maxHp);
        b.name = "Flame Demon";
        return b;
    }

    public static Being generateBat( Tile t){
        Being b = new Being(Side.DEMONS);
        Room.moveTo(b,t);
        b.moveCard = new ShamblerMoveCard();
        b.aWeap = WeaponFactory.getBatBite();
        b.bWeap = WeaponFactory.getBatBite();
        b.setSprite(Resources.Load<Sprite>("sprites/evil-bat"));
        b.maxHp = 10f;
        b.setHp(b.maxHp);
        b.name = "Bat";
        b.setSpeed(3);
        b.setAgilityMult(1.2f);
        return b;
    }

    public static Being generatePlayer(int num, Tile t)
    {
        Being b = new Being(Side.PLAYER);
        Room.moveTo(b,t);
        b.moveCard = new StandardMoveCard();
        b.aWeap = WeaponFactory.getWeaponByName(PlayerPrefs.GetString(num+"_w1"));
        b.bWeap = WeaponFactory.getWeaponByName(PlayerPrefs.GetString(num+"_w2"));
        b.head_a = ArmorFactory.getArmorByName(PlayerPrefs.GetString(num+"_h"));
        b.chest_a = ArmorFactory.getArmorByName(PlayerPrefs.GetString(num+"_c"));
        b.arm_a = ArmorFactory.getArmorByName(PlayerPrefs.GetString(num+"_a"));
        b.leg_a = ArmorFactory.getArmorByName(PlayerPrefs.GetString(num+"_l"));
        b.feet_a = ArmorFactory.getArmorByName(PlayerPrefs.GetString(num+"_f"));
        b.setSprite(Resources.Load<Sprite>("sprites/space-suit"));
        switch (num){
            case 1: b.name = "Adam";break;
            case 2: b.name = "Britanny";break;
            case 3: b.name = "Chris";break;
            case 4: b.name = "Dylan";break;
        }
        return b;
    }
}


public class ShamblerMoveCard : Card
{
     public ShamblerMoveCard(){
         cName = "shambles";
         tarhint = TargetHint.BLANK;
     }

    public override float getFitness(Being user, Tile target)
    {
        return .5f;
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
        try{
        if (user.getTile().getX() < 3){
            Debug.Log(Handler.h.r.getXSize());
            Room.moveTo(user,Handler.h.r.getTile(Handler.h.r.getXSize()-1,user.getTile().getY()));
            return;
        }}catch(Exception e){Debug.Log(e);}
       if (Core.clearRay(user,user.getTile(),target,this.getTileSet() ,true)){
           Room.moveTo(user,target);
       }
    }
}

public class NoMoveCard : Card
{
     public NoMoveCard(){
         cName = "sits";
         tarhint = TargetHint.BLANK;
     }

    public override float getFitness(Being user, Tile target)
    {
        return .1f;
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
    }
}

public class GooMoveCard : Card
{
     public GooMoveCard(){
         cName = "goo move";
         tarhint = TargetHint.BLANK;
     }

    public override float getFitness(Being user, Tile target)
    {
        return .5f;
    }

    public override TileSet getTileSet()
    {
        TileSet t = new TileSet();
        t.tos.Add(new TileOffset(1,0));
        t.tos.Add(new TileOffset(1,-1));
        t.tos.Add(new TileOffset(1,1));
        t.tos.Add(new TileOffset(2,0));
        t.tos.Add(new TileOffset(2,-1));
        t.tos.Add(new TileOffset(2,1));
        return t;
    }

    public override void use(Being user, Tile target)
    {
        try{
        if (user.getTile().getX() < 3){
            Debug.Log(Handler.h.r.getXSize());
            Room.moveTo(user,Handler.h.r.getTile(Handler.h.r.getXSize()-1,user.getTile().getY()));
            return;
        }}catch(Exception e){Debug.Log(e);}
       if (Core.clearRay(user,user.getTile(),target,this.getTileSet() ,true)){
           Room.moveTo(user,target);
       }
    }
}

public class PitchforkMoveCard : Card
{
     public PitchforkMoveCard(){
         cName = "pitchfork move";
         tarhint = TargetHint.BLANK;
     }

    public override float getFitness(Being user, Tile target)
    {
        return .5f;
    }

    public override TileSet getTileSet()
    {
        TileSet t = new TileSet();
        t.tos.Add(new TileOffset(1,0));
        t.tos.Add(new TileOffset(1,-1));
        t.tos.Add(new TileOffset(1,1));
        t.tos.Add(new TileOffset(0,1));
        t.tos.Add(new TileOffset(0,-1));
        t.tos.Add(new TileOffset(-1,-1));
        t.tos.Add(new TileOffset(-1,1));
        t.tos.Add(new TileOffset(-1,0));
        return t;
    }

    public override void use(Being user, Tile target)
    {
        try{
        if (user.getTile().getX() < 3){
            Debug.Log(Handler.h.r.getXSize());
            Room.moveTo(user,Handler.h.r.getTile(Handler.h.r.getXSize()-1,user.getTile().getY()));
            return;
        }}catch(Exception e){Debug.Log(e);}
       if (Core.clearRay(user,user.getTile(),target,this.getTileSet() ,true)){
           Room.moveTo(user,target);
       }
    }
}