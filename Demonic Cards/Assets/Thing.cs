using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour, canCollide
{
    public canCollide sub;
    private Tile tile;
    public CollidableType subType;
    public TMPro.TMP_Text text;
    public Thing(canCollide s, Tile t,CollidableType ty){
        sub = s;
        setTile(t);
        subType = ty;
    }
    public canCollide getSub()
    {
        return sub;
    }

    public CollidableType getSubType()
    {
        return subType;
    }

    public Tile getTile()
    {
        return tile;
    }

    public void setTile(Tile t){
        tile = t;
        gameObject.transform.position = new Vector3(t.getX(),t.getY(),-2);
    }

    public void updateBeing(Being b){
        text.text = "HP: " +  (int)b.getHp() + "/" + b.maxHp; 
    }
}