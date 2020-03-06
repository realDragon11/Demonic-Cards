using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**

Brian M. 
3/5/2020
The Core class containing beings and other core things
**/
public class Being : MonoBehaviour
{
    public static Being turnWho;
    private float hitpoints;
    private Side side;
    public ResistMap baseRMap = new ResistMap();
    public List<Item> inv = new List<Item>();
    private Armor head_a,arm_a,chest_a,leg_a,feet_a;
    public DamageMultMap damMultMap;
    public List<Buff> buffs = new List<Buff>();
    //how many cards it can play per turn
    private int speed = 2;
    private float agilityMult = 1f;
    private float timeToAct = 100f;
    private int actionsLeft = 0;

    public Side getSide(){
        return side;
    }

    public void setSide(Side s){
        side = s;
    }

    public void setHp(float h){
        hitpoints = h;
    }

    public void addHp(float h){
        hitpoints +=h;
    }

    public bool isDead(){
        return hitpoints <= 0;
    }

    public float getHp(){
        return hitpoints;
    }

    public bool damage(Attack a){
        refreshDamageMultMap();//remove if this causes lag
        foreach (Damage d in a.dams)
        {
            hitpoints-=d.dam*this.damMultMap.getResistMult(d.dt);
        }
        return isDead();
    }

    public void refreshDamageMultMap(){
        List<ResistMap> list = new List<ResistMap>();
        foreach (Buff b in buffs)//is there really no addAll?
        {
            list.Add(b.rMap);
        }
        list.Add(head_a.resist);
        list.Add(arm_a.resist);
        list.Add(chest_a.resist);
        list.Add(leg_a.resist);
        list.Add(feet_a.resist);
        list.Add(baseRMap);
        damMultMap = ResistMap.consolidate(list);
    }

    public void setSpeed(int spd){
        speed = spd;
    }

    public void setAgilityMult(float f){
        agilityMult = f;
    }
    public float getAgilityMult(){
        return agilityMult;
    }

    public float timeTilAction(){
        return timeToAct/agilityMult;
    }
    public void advanceTime(float t){
        List<Buff> removeList = new List<Buff>();
        foreach (Buff b in buffs)
        { 
            if (b.advanceTime(t)){
                removeList.Add(b);
            }
        }
        foreach (Buff b in removeList)
        {
            buffs.Remove(b);
        }
        timeToAct-=agilityMult*t;
    }

    public void setTurn(){
        Handler.advanceTime(this.timeTilAction());
        turnWho = this;
        timeToAct = 100f;
        actionsLeft = speed;
       if (this.getSide() != Side.PLAYER){
           this.aiAct();
       }
    }

    public void aiAct(){
        
    }
}
public enum DamageType{
    SLASH, BLUNT, PIERCE, REND, FIRE, ICE, ELEC, HOLY, DEMONIC
}

public enum Side{
    PLAYER, DEMONS
}
//https://www.dotnetperls.com/map //I know how to use maps in java, I just needed to lookup how dictionaries fill that role in c# - Brian M.
public class ResistMap
{
    private Dictionary<DamageType,float> resists = new Dictionary<DamageType,float>();

    public void addResist(DamageType dt, float resistValue){
        if (resists.ContainsKey(dt)){
            throw new GenericRuntimeException("map already contains resist");
        }
        resists.Add(dt,resistValue);
    }

     public void replaceResist(DamageType dt, float resistValue){
        if (resists.ContainsKey(dt)){
           resists.Remove(dt); 
        }
            resists.Add(dt,resistValue);
        
    }

    public float getResist(DamageType dt){
        if (resists.ContainsKey(dt)){
            float toOut;
            resists.TryGetValue(dt,out toOut);
            return toOut;
        }
        return 0f;
    }

    public List<DamageType> getKeys(){
        List<DamageType> keys = new List<DamageType>();
        foreach (DamageType d in resists.Keys){
            keys.Add(d);
        }
        return keys;
    }

    public static DamageMultMap consolidate(List<ResistMap> list){
        DamageMultMap r = new DamageMultMap();
        List<DamageType> typeList = new List<DamageType>();
        foreach (ResistMap rMap in list)
        {
            foreach (DamageType key in rMap.getKeys())
            {
                r.addResist(key,rMap.getResist(key));
                if (!typeList.Contains(key)){
                    typeList.Add(key);
                }
            }
        }

        foreach (DamageType dt in typeList){
            r.complete(dt);
        }

        return r;
    }
}
///used for calculating end damage, basically the sum of all the resistmaps
public class DamageMultMap
{
    private Dictionary<DamageType,float> resists = new Dictionary<DamageType,float>();

    public void addResist(DamageType dt, float resistValue){
        float rVal = 1;
        if (resists.ContainsKey(dt)){
            resists.TryGetValue(dt,out rVal);
        }
        rVal-=resistValue;
        resists.Add(dt,rVal);
    }

    public void complete(DamageType dt){
        float rVal = 1;
         if (resists.ContainsKey(dt)){
            resists.TryGetValue(dt,out rVal);
            resists.Remove(dt); 
        }
        if (rVal < 0){
            rVal = 0;
        }
        resists.Add(dt,rVal);
    }

    /*
     public void replaceResist(DamageType dt, float resistValue){
        if (resists.ContainsKey(dt)){
           resists.Remove(dt); 
        }
            resists.Add(dt,resistValue);
        
    }
    */

    public float getResistMult(DamageType dt){
        if (resists.ContainsKey(dt)){
            float toOut;
            resists.TryGetValue(dt,out toOut);
            return toOut;
        }
        return 1f;
    }
}

public class Attack{
    public List<Damage> dams = new List<Damage>();
    public Being attacker;
}

public class Damage{
    public float dam;
    public DamageType dt;
}

[System.Serializable]
public class GenericRuntimeException : System.Exception
{
    public GenericRuntimeException() { }
    public GenericRuntimeException(string message) : base(message) { }
    public GenericRuntimeException(string message, System.Exception inner) : base(message, inner) { }
    protected GenericRuntimeException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
