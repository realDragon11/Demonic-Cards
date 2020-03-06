using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory
{
    private static Weapon combat_knife = null;

    public static Weapon getCombatKnife(){
        if (combat_knife == null){
            combat_knife = new Weapon(null,"Combat Knife","A balanced starting weapon that deals slashing and piercing.",ItemSubType.MELEE_WEAPON,new CombatKnifeStab(),new CombatKnifeStab(),new CombatKnifeStab(),new CombatKnifeStab(),new CombatKnifeStab());
        }
        return combat_knife;
    }

}

public class CombatKnifeStab : Card
{
    public CombatKnifeStab(){
        cName = "Stab";
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
}