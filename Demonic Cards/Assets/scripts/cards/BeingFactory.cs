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
        b.aWeap = WeaponFactory.getShamblerClaws();
        b.baseRMap.addResist(DamageType.FIRE,-.2f);
        b.baseRMap.addResist(DamageType.SLASH,-.1f);
        b.setSprite(Resources.Load<Sprite>("sprites/shambling-zombie"));
        b.maxHp = 30f;
        b.setHp(b.maxHp);
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
        return b;
    }
}


public class ShamblerMoveCard : Card
{
     public ShamblerMoveCard(){
         cName = "shambler move";
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