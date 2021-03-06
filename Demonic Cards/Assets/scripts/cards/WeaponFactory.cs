﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory
{

    public static Weapon getWeaponByName(string str){
        switch (str){
            case "Combat Knife":
            return getCombatKnife();
            case "Harpoon":
            return getHarpoon();
            case "Raygun":
            return getRaygun();
            case "Pistol":
            return getPistol();
            case "Brass Knuckles":
            return getBrass();
        }
        throw new GenericRuntimeException("Weapon not found!");
    }

    private static Weapon combat_knife = null;

    public static Weapon getCombatKnife(){
        if (combat_knife == null){
            combat_knife = new Weapon(Resources.Load<Sprite>("sprites/trench-knife"),"Combat Knife","A balanced starting weapon that deals slashing and piercing.",ItemSubType.MELEE_WEAPON,new CombatKnifeStab(),new CombatKnifeStab(),new CombatKnifeSlice(),new CombatKnifeSlice(),new CombatKnifeBackStab());
        }
        return combat_knife;
    }

    private static Weapon harpoon = null;

    public static Weapon getHarpoon(){
        if (harpoon == null){
            harpoon = new Weapon(Resources.Load<Sprite>("sprites/harpoon-chain"),"Harpoon","For getting the enemies closer.",ItemSubType.RANGED_WEAPON,new HarpoonShoot(),new HarpoonShoot(),new HarpoonShoot(),new HarpoonShoot(),new CombatKnifeStab());
        }
        return harpoon;
    }

    private static Weapon raygun = null;

    public static Weapon getRaygun(){
        if (raygun == null){
            raygun = new Weapon(Resources.Load<Sprite>("sprites/ray-gun"),"Raygun","Pew-Pew.",ItemSubType.RANGED_WEAPON,new RaygunShoot(),new RaygunShoot(),new RaygunShoot(),new RaygunShoot(),new RaygunShoot());
        }
        return raygun;
    }

    private static Weapon pistol = null;

    public static Weapon getPistol(){
        if (pistol == null){
            pistol = new Weapon(Resources.Load<Sprite>("sprites/pistol-gun"),"Raygun","Bang-bang.",ItemSubType.RANGED_WEAPON,new PistolShoot(),new PistolShoot(),new PistolShoot(),new PistolShoot(),new PistolShoot());
        }
        return pistol;
    }
    private static Weapon brass_knuckles = null;

    public static Weapon getBrass(){
        if (brass_knuckles == null){
            brass_knuckles = new Weapon(Resources.Load<Sprite>("sprites/brass-knuckles"),"Combat Knife","Ponch.",ItemSubType.MELEE_WEAPON,new BrassSwing(),new BrassSwing(),new BrassSwing(),new BrassSwing(),new BrassSwing());
        }
        return brass_knuckles;
    }

    private static Weapon shambler_claws = null;

    public static Weapon getShamblerClaws(){
        if (shambler_claws == null){
            shambler_claws = new Weapon(null,"Shambler Claws","A zombie's swing.",ItemSubType.MELEE_WEAPON,new ShamblerSwing(),new ShamblerSwing(),new ShamblerSwing(),new ShamblerSwing(),new ShamblerSwing());
        }
        return shambler_claws;
    }

    private static Weapon goop_throw = null;
    public static Weapon getGoopThrow(){
        if (goop_throw == null){
            goop_throw = new Weapon(null,"Goop Throw","",ItemSubType.RANGED_WEAPON,new GoopThrow(),new GoopThrow(),new GoopThrow(),new GoopThrow(),new GoopThrow());
        }
        return goop_throw;
    }

    private static Weapon goop_bash = null;
    public static Weapon getGoopBash(){
        if (goop_bash == null){
            goop_bash = new Weapon(null,"Goop Bash","",ItemSubType.MELEE_WEAPON,new GoopBash(),new GoopBash(),new GoopBash(),new GoopBash(),new GoopBash());
        }
        return goop_bash;
    }
    private static Weapon pitchfork = null;
     public static Weapon getPitchfork(){
        if (pitchfork == null){
            pitchfork = new Weapon(null,"Pitchfork","",ItemSubType.MELEE_WEAPON,new Pitchfork(),new Pitchfork(),new Pitchfork(),new Pitchfork(),new Pitchfork());
        }
        return pitchfork;
    }

    private static Weapon fireball = null;
     public static Weapon getFireball(){
        if (fireball == null){
            fireball = new Weapon(null,"Fireball","",ItemSubType.RANGED_WEAPON,new Fireball(),new Fireball(),new Fireball(),new Fireball(),new Fireball());
        }
        return fireball;
    }

    private static Weapon flamesummon = null;
     public static Weapon getFlameSummon(){
        if (flamesummon == null){
            flamesummon = new Weapon(null,"Fireball","",ItemSubType.RANGED_WEAPON,new FlameSummon(),new FlameSummon(),new FlameSummon(),new FlameSummon(),new FlameSummon());
        }
        return flamesummon;
    }

    private static Weapon bat_bite = null;
     public static Weapon getBatBite(){
        if (bat_bite == null){
            bat_bite = new Weapon(null,"Fireball","",ItemSubType.MELEE_WEAPON,new BatBite(),new BatBite(),new BatBite(),new BatBite(),new BatBite());
        }
        return bat_bite;
    }

     private static Weapon mound = null;

    public static Weapon getMound(){
        if (mound == null){
            mound = new Weapon(null,"Mound","Plant smash!.",ItemSubType.MELEE_WEAPON,new MoundPound(),new MoundPound(),new MoundPound(),new MoundPound(),new MoundPound());
        }
        return mound;
    }

}

public class CombatKnifeStab : Card
{
    public CombatKnifeStab(){
        cName = "Stab";
        tarhint = TargetHint.ENEMY;
        image = Resources.Load<Sprite>("sprites/knife-thrust");
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
        a.dams.Add(new Damage(10f,DamageType.PIERCE));
        target.occupant.damage(a);
        Handler.logA(user.name + " stabs the " + target.occupant.name + "!",this.image);
    }
    public override float getFitness(Being user, Tile target)
    {
        if (user.getSide() == target.occupant.getSide()){
            return -1f;
        }
        Attack a = new Attack();
        a.dams.Add(new Damage(10f,DamageType.PIERCE));
        return target.occupant.getDamageAmount(a);
    }
}

public class CombatKnifeSlice : Card
{
    public CombatKnifeSlice(){
        cName = "Slice";
        tarhint = TargetHint.ENEMY;
        image = Resources.Load<Sprite>("sprites/sword-slice");
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
        Attack a = new Attack();
        a.dams.Add(new Damage(6f,DamageType.SLASH));
        target.occupant.damage(a);
        Handler.logA(user.name + " slices at the " + target.occupant.name + "!",this.image);
    }

    public override float getFitness(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(6f,DamageType.SLASH));
        return target.occupant.getDamageAmount(a);
    }
}

public class CombatKnifeBackStab : Card
{
    public CombatKnifeBackStab(){
        cName = "Backstab";
        tarhint = TargetHint.ENEMY;
        image= Resources.Load<Sprite>("sprites/backstab");
    }

    

    public override TileSet getTileSet()
    {
        TileSet t = new TileSet();
        t.tos.Add(new TileOffset(-1,0));
        return t;
    }


    public override void use(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(20f,DamageType.PIERCE));
        target.occupant.damage(a);
        Handler.logA(user.name + " backstabs the " + target.occupant.name + "!",this.image);
    }
    public override float getFitness(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(20f,DamageType.PIERCE));
        return target.occupant.getDamageAmount(a);
    }
}

public class ShamblerSwing : Card
{
    public ShamblerSwing(){
        cName = "Swing";
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
        Attack a = new Attack();
        a.dams.Add(new Damage(5f,DamageType.BLUNT));
        a.dams.Add(new Damage(5f,DamageType.SLASH));
        target.occupant.damage(a);
        Handler.logA("The " + user.name + " swipes at " + target.occupant.name + "!",user.sprite);
    }

    public override float getFitness(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(5f,DamageType.BLUNT));
        a.dams.Add(new Damage(5f,DamageType.SLASH));
        //Debug.Log("Shambled! - " + target.occupant.getDamageAmount(a));
        return target.occupant.getDamageAmount(a);
    }
}

public class GoopThrow : Card
{
    public GoopThrow(){
        cName = "GooThrow";
        tarhint = TargetHint.ENEMY;
    }
    public override TileSet getTileSet()
    {
        TileSet t = new TileSet();
        t.tos.Add(new TileOffset(2,0));
        t.tos.Add(new TileOffset(2,-1));
        t.tos.Add(new TileOffset(2,1));
        t.tos.Add(new TileOffset(3,0));
        t.tos.Add(new TileOffset(3,-1));
        t.tos.Add(new TileOffset(3,1));
        t.tos.Add(new TileOffset(1,0));
        t.tos.Add(new TileOffset(1,-1));
        t.tos.Add(new TileOffset(1,1));
        return t;
    }


    public override void use(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(1f,DamageType.BLUNT));
        a.dams.Add(new Damage(14f,DamageType.ACID));
        target.occupant.damage(a);
        Handler.logA("The " + user.name + " throws goo at " + target.occupant.name + "!",user.sprite);
    }

    public override float getFitness(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(1f,DamageType.BLUNT));
        a.dams.Add(new Damage(14f,DamageType.ACID));
        //Debug.Log("Shambled! - " + target.occupant.getDamageAmount(a));
        return target.occupant.getDamageAmount(a);
    }
}

public class GoopBash : Card
{
    public GoopBash(){
        cName = "GooBash";
        tarhint = TargetHint.ENEMY;
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
        Attack a = new Attack();
        a.dams.Add(new Damage(5f,DamageType.BLUNT));
        a.dams.Add(new Damage(5f,DamageType.ACID));
        target.occupant.damage(a);
        Handler.logA("The " + user.name + " bashes into " + target.occupant.name + "!",user.sprite);
    }

    public override float getFitness(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(5f,DamageType.BLUNT));
        a.dams.Add(new Damage(5f,DamageType.ACID));
        //Debug.Log("Shambled! - " + target.occupant.getDamageAmount(a));
        return target.occupant.getDamageAmount(a);
    }
}

public class Pitchfork : Card
{
    public Pitchfork(){
        cName = "Pitchfork";
        tarhint = TargetHint.ENEMY;
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
        Attack a = new Attack();
        a.dams.Add(new Damage(15f,DamageType.PIERCE));
        target.occupant.damage(a);
        Handler.logA("The " + user.name + " skewers " + target.occupant.name + "!",user.sprite);
    }

    public override float getFitness(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(15f,DamageType.PIERCE));
        //Debug.Log("Shambled! - " + target.occupant.getDamageAmount(a));
        return target.occupant.getDamageAmount(a);
    }
}

public class Fireball : Card
{
    public Fireball(){
        cName = "Fireball toss";
        tarhint = TargetHint.ENEMY;
    }
    public override TileSet getTileSet()
    {
        TileSet t = new TileSet();
        t.tos.Add(new TileOffset(2,0));
        t.tos.Add(new TileOffset(2,-1));
        t.tos.Add(new TileOffset(2,1));
        t.tos.Add(new TileOffset(3,0));
        t.tos.Add(new TileOffset(3,-1));
        t.tos.Add(new TileOffset(3,1));
        return t;
    }


    public override void use(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(20f,DamageType.FIRE));
        target.occupant.damage(a);
        Handler.logA("The " + user.name + " throws a fireball at " + target.occupant.name + "!",user.sprite);
    }

    public override float getFitness(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(20f,DamageType.FIRE));
        //Debug.Log("Shambled! - " + target.occupant.getDamageAmount(a));
        return target.occupant.getDamageAmount(a);
    }
}

public class FlameSummon : Card
{
    public FlameSummon(){
        cName = "FlameSummon";
        tarhint = TargetHint.BLANK;
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
        Attack a = new Attack();
        a.dams.Add(new Damage(5f,DamageType.FIRE));
        
        Being tar = Handler.h.getRandomPlayer();
        tar.damage(a);
        Handler.logA("The " + user.name + " summons flames under " + tar.name + "'s feet!",user.sprite);
    }

    public override float getFitness(Being user, Tile target)
    {
        return 5f;
    }
}

public class BatBite : Card
{
    public BatBite(){
        cName = "Bite";
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
        Attack a = new Attack();
        a.dams.Add(new Damage(3f,DamageType.REND));
        target.occupant.damage(a);
        Handler.logA("The " + user.name + " bites " + target.occupant.name + "!",user.sprite);
    }

    public override float getFitness(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(3f,DamageType.REND));
        //Debug.Log("Shambled! - " + target.occupant.getDamageAmount(a));
        return target.occupant.getDamageAmount(a);
    }
}

public class HarpoonShoot : Card
{
    public HarpoonShoot(){
        cName = "Shoot the Harpoon";
        tarhint = TargetHint.ENEMY;
        image = Resources.Load<Sprite>("sprites/harpoon-chain");
    }

    

    public override TileSet getTileSet()
    {
        TileSet t = new TileSet();
        t.tos.Add(new TileOffset(2,0));
        t.tos.Add(new TileOffset(3,0));
        t.tos.Add(new TileOffset(4,0));
        t.tos.Add(new TileOffset(5,0));
        return t;
    }


    public override void use(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(10f,DamageType.PIERCE));
        target.occupant.damage(a);
        Handler.logA(user.name + " harpoons the " + target.occupant.name + "!",this.image);
        Being person = target.occupant;
        Tile t = Handler.h.r.getTileRelative(target,new TileOffset(user.getSide().Equals(Side.PLAYER) ? -1: 1,0));
        if (t.occupant == null){
        Room.moveTo(person,t);}
    }
    public override float getFitness(Being user, Tile target)
    {
        if (user.getSide() == target.occupant.getSide()){
            return -1f;
        }
        Attack a = new Attack();
        a.dams.Add(new Damage(10f,DamageType.PIERCE));
        return target.occupant.getDamageAmount(a);
    }
}

public class RaygunShoot : Card
{
    public RaygunShoot(){
        cName = "Ray";
        tarhint = TargetHint.ENEMY;
        image = Resources.Load<Sprite>("sprites/laser-blast");
    }

    

    public override TileSet getTileSet()
    {
        TileSet t = new TileSet();
        t.tos.Add(new TileOffset(1,0));
        t.tos.Add(new TileOffset(2,0));
        t.tos.Add(new TileOffset(3,0));
        t.tos.Add(new TileOffset(4,0));
        t.tos.Add(new TileOffset(5,0));
        t.tos.Add(new TileOffset(6,0));
        return t;
    }


    public override void use(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(10f,DamageType.FIRE));
        a.dams.Add(new Damage(2f,DamageType.ELEC));
        target.occupant.damage(a);
        Handler.logA(user.name + " lasers the " + target.occupant.name + "!",this.image);
    }
    public override float getFitness(Being user, Tile target)
    {
        if (user.getSide() == target.occupant.getSide()){
            return -1f;
        }
        Attack a = new Attack();
         a.dams.Add(new Damage(10f,DamageType.FIRE));
        a.dams.Add(new Damage(2f,DamageType.ELEC));
        return target.occupant.getDamageAmount(a);
    }
}

public class PistolShoot : Card
{
    public PistolShoot(){
        cName = "Shoot Pistol";
        tarhint = TargetHint.ENEMY;
        image = Resources.Load<Sprite>("sprites/supersonic-bullet");
    }

    

    public override TileSet getTileSet()
    {
        TileSet t = new TileSet();
        t.tos.Add(new TileOffset(1,0));
        t.tos.Add(new TileOffset(2,0));
        t.tos.Add(new TileOffset(3,0));
        t.tos.Add(new TileOffset(4,0));
        t.tos.Add(new TileOffset(5,0));
        t.tos.Add(new TileOffset(6,0));
        return t;
    }


    public override void use(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(12f,DamageType.PIERCE));
        target.occupant.damage(a);
        Handler.logA(user.name + " lasers the " + target.occupant.name + "!",this.image);
    }
    public override float getFitness(Being user, Tile target)
    {
        if (user.getSide() == target.occupant.getSide()){
            return -1f;
        }
        Attack a = new Attack();
        a.dams.Add(new Damage(12f,DamageType.PIERCE));
        return target.occupant.getDamageAmount(a);
    }
}

public class BrassSwing : Card
{
    public BrassSwing(){
        cName = "Swing";
        tarhint = TargetHint.ENEMY;
        image = Resources.Load<Sprite>("sprites/brass-knuckles");
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
        Attack a = new Attack();
        a.dams.Add(new Damage(10f,DamageType.BLUNT));
        target.occupant.damage(a);
        Handler.logA(user.name + " punches the " + target.occupant.name + "!",user.sprite);
    }

    public override float getFitness(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(10f,DamageType.BLUNT));
        return target.occupant.getDamageAmount(a);
    }
}

public class MoundPound : Card
{
    public MoundPound(){
        cName = "Mound Pound";
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
        a.dams.Add(new Damage(15f,DamageType.BLUNT));
        target.occupant.damage(a);
        Handler.logA("The " + user.name + " pounds " + target.occupant.name + "!",user.sprite);
    }

    public override float getFitness(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(15f,DamageType.BLUNT));
        return target.occupant.getDamageAmount(a);
    }
}