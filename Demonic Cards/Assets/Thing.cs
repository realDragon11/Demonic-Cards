using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour, canCollide
{
    public canCollide sub;
    public Tile tile;
    public CollidableType subType;
    public Thing(canCollide s, Tile t,CollidableType ty){
        sub = s;
        tile = t;
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
}