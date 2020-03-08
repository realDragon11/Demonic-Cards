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
        return b;
    }

    public static Being generatePlayer(int num, Tile t)
    {
        Being b = new Being(Side.PLAYER);
        Room.moveTo(b,t);
        b.moveCard = new StandardMoveCard();
        b.aWeap = WeaponFactory.getWeaponByName(PlayerPrefs.GetString(num+"_w1"));
        b.bWeap = WeaponFactory.getWeaponByName(PlayerPrefs.GetString(num+"_w2"));
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
       if (Core.clearRay(user,user.getTile(),target,this.getTileSet() ,true)){
           Room.moveTo(user,target);
       }
    }
}