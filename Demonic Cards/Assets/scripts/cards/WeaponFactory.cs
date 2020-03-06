using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory
{
    private static Weapon combat_knife = null;

    public static Weapon getCombatKnife(){
        if (combat_knife == null){
            combat_knife = new Weapon(null,"Combat Knife","A balanced starting weapon that deals slashing and piercing.",ItemSubType.MELEE_WEAPON,new CombatKnifeStab(),new CombatKnifeStab(),new CombatKnifeSlice(),new CombatKnifeSlice(),new CombatKnifeStab());
        }
        return combat_knife;
    }

}

public class CombatKnifeStab : Card
{
    public CombatKnifeStab(){
        cName = "Stab";
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
        a.dams.Add(new Damage(10f,DamageType.PIERCE));
        target.occupant.damage(a);
    }
    public override float getFitness(Being user, Tile target)
    {
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
    }

    public override float getFitness(Being user, Tile target)
    {
        Attack a = new Attack();
        a.dams.Add(new Damage(6f,DamageType.SLASH));
        return target.occupant.getDamageAmount(a);
    }
}