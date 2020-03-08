using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**

Brian M. 
3/5/2020
The Core class containing beings and other core things
**/
public class Being : canCollide
{
    public static Being turnWho;
    private float hitpoints = 100;
    public float maxHp = 100;
    private Side side;
    public ResistMap baseRMap = new ResistMap();
    public List<Item> inv = new List<Item>();
    public Armor head_a = ArmorFactory.getGenericArmor(ItemSubType.HEAD_ARMOR),arm_a= ArmorFactory.getGenericArmor(ItemSubType.ARM_ARMOR),chest_a = ArmorFactory.getGenericArmor(ItemSubType.BODY_ARMOR),leg_a= ArmorFactory.getGenericArmor(ItemSubType.LEG_ARMOR),feet_a= ArmorFactory.getGenericArmor(ItemSubType.FEET_ARMOR);
    public Weapon aWeap = WeaponFactory.getCombatKnife(), bWeap = WeaponFactory.getCombatKnife();
    public DamageMultMap damMultMap;
    public List<Buff> buffs = new List<Buff>();
    //how many cards it can play per turn
    private int speed = 2;
    private float agilityMult = 1f;
    private float timeToAct = 100f;
    private int actionsLeft = 0;
    private Tile curTile;
    public Card curCard = new StandardMoveCard();
    public Thing linkedThing;
    public Card moveCard = new StandardMoveCard();

    public List<Card> cards = new List<Card>();

    public Being(Side s){
        side = s;
        Debug.Log("Making a being.");
        if (!GameObject.Instantiate(Handler.h.athing).TryGetComponent<Thing>(out linkedThing)){
            throw new GenericRuntimeException("Oh no!");
        }
    }

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
       hitpoints-=getDamageAmount(a);
        Debug.Log("damaged: " + hitpoints);
        linkedThing.updateBeing(this);
        return isDead();
    }

    public float getDamageAmount(Attack a){
        refreshDamageMultMap();//switch if this causes lag
        float total= 0f;
        foreach (Damage d in a.dams)
        {
            total+=d.dam*this.damMultMap.getResistMult(d.dt);
        }
        return total;
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

    public void selectCard(int value)
    {
        this.curCard = cards[value];
    }

    public void draw(){
        cards.Clear();
        cards.Add(this.moveCard);
        cards.Add(aWeap.draw());
        cards.Add(aWeap.draw());
        cards.Add(bWeap.draw());
        cards.Add(bWeap.draw());
        for (int i = 0; i < 2;i++){
            int j = Handler.randRange(1,5);
            switch (j){
                case 1:
                cards.Add(head_a.draw());
                break;
                case 2:
                cards.Add(arm_a.draw());
                break;
                case 3:
                cards.Add(chest_a.draw());
                break;
                case 4:
                cards.Add(leg_a.draw());
                break;
                case 5:
                cards.Add(feet_a.draw());
                break;
            }
        }
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
         Debug.Log("Setting turn.");
        Handler.advanceTime(this.timeTilAction());
        turnWho = this;
        timeToAct = 100f;
        actionsLeft = speed;
        draw();
       if (this.getSide() != Side.PLAYER){
           this.aiAct();
       }else{
           Handler.h.displayCards(cards);
       }
    }

    public void aiAct(){
        Debug.Log("A enemy does their turn!");
        int i = 0;
        while (actionsLeft > 0 && i < 99){
            AIAction a = AICore.decide(AICore.oneDeep(this));
            attemptToUseCard(a.card,a.target);
            i++;
        }
        Handler.h.nextTurn();
    }

    public Tile getTile()
    {
        return curTile;
    }
    public void setCurTile(Tile t){
        curTile = t;
        linkedThing.setTile(t);
    }

    public CollidableType getSubType()
    {
        return CollidableType.BEING;
    }

    public bool attemptToUseCard(Card c, Tile t){
        if (actionsLeft <= 0){
            return false;
        }
        
        int mult = 1;
        if (this.side.Equals(Side.DEMONS)){
            mult = -1;
        }
        bool pass = false;
        foreach (TileOffset toff in c.getTileSet().tos)
        {
            if (this.getTile().getY()+toff.yOff == t.getY()){
                if (this.getTile().getX()+(toff.xOff*mult) == t.getX()){
                    pass = true;
                    break;
                }
            }
        }
        if (!pass){
            return false;
        }

        if (!Core.clearRay(this,this.getTile(),t,c.getTileSet(),false)){
            return false;
        }
        c.use(this,t);
        actionsLeft--;
        return true;
    }

    public canCollide getSub(){
        return this;
    }

    public int getActionsLeft(){
        return actionsLeft;
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

    public Damage(float damage, DamageType damageType){
        dam = damage;
        dt = damageType;
    }
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

public class StandardMoveCard : Card
{
     public StandardMoveCard(){
         cName = "move";
         tarhint = TargetHint.BLANK;
     }

    public override float getFitness(Being user, Tile target)
    {
        return .5f;
    }

    public override TileSet getTileSet()
    {
        TileSet t = new TileSet();
        t.tos.Add(new TileOffset(-1,0));
        t.tos.Add(new TileOffset(1,0));
        t.tos.Add(new TileOffset(2,0));
        t.tos.Add(new TileOffset(0,-1));
        t.tos.Add(new TileOffset(0,1));
        return t;
    }

    public override void use(Being user, Tile target)
    {
       if (Core.clearRay(user,user.getTile(),target,this.getTileSet() ,true)){
           Room.moveTo(user,target);
       }
    }
}

public interface canCollide
{
    CollidableType getSubType();
     Tile getTile();
     canCollide getSub();
}

public enum CollidableType{
    BEING, WALL
}

public static class Core{
    public static bool clearRay(Being user, Tile start, Tile end, TileSet mask, bool lastSpaceFree){
        var cols = Physics2D.LinecastAll(new Vector2(start.getX(),start.getY()),new Vector2(end.getX(),end.getY()));
        List<Collider2D> colList = new List<Collider2D>();
        foreach (var item in cols)
        {
            if (Vector3.Distance(item.collider.gameObject.transform.position,new Vector3(start.getX(),start.getY())) < .3f){
                //we don't care!
            }else{
                if (!lastSpaceFree && Vector3.Distance(item.collider.gameObject.transform.position,new Vector3(end.getX(),end.getY())) < .3f){
                    continue;
                }
                canCollide b = null;
                if (item.collider.gameObject.TryGetComponent<canCollide>(out b)){
                    switch (b.getSubType()){
                        case CollidableType.BEING:
                        Being a = (Being)b.getSub();
                        Side s = a.getSide();
                        if (user.getSide() == s && mask.blockedByAllies){
                            colList.Add(item.collider);
                        }else{
                            if (mask.blockedByEnemies){
                                colList.Add(item.collider);
                            }
                        }
                        break;
                        case CollidableType.WALL:
                            if (mask.blockedByWalls){
                                colList.Add(item.collider);
                            }
                        break;
                    }
                }
            }
        }
        Debug.Log("Count: " + colList.Count);
        return !(colList.Count > 0);
    }

}