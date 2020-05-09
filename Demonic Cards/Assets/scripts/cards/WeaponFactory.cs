using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory
{
    private static Weapon combat_knife = null;

    public static Weapon getWeaponByName(string str){
        switch (str){
            case "Combat Knife":
            return getCombatKnife();
        }
        throw new GenericRuntimeException("Weapon not found!");
    }

    public static Weapon getCombatKnife(){
        if (combat_knife == null){
            combat_knife = new Weapon(Resources.Load<Sprite>("sprites/trench-knife"),"Combat Knife","A balanced starting weapon that deals slashing and piercing.",ItemSubType.MELEE_WEAPON,new CombatKnifeStab(),new CombatKnifeStab(),new CombatKnifeSlice(),new CombatKnifeSlice(),new CombatKnifeStab());
        }
        return combat_knife;
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
            goop_throw = new Weapon(null,"Goop Throw","",ItemSubType.MELEE_WEAPON,new GoopThrow(),new GoopThrow(),new GoopThrow(),new GoopThrow(),new GoopThrow());
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