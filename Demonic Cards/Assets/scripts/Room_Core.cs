﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
   private List<List<Tile>> tiles = new List<List<Tile>>();
   private int xSize, ySize;

    public Room(int xSize2,int ySize2){
        xSize = xSize2;
        ySize = ySize2;
        for (int i = 0; i < xSize; i++)
        {
            tiles.Add(new List<Tile>());
            for (int j = 0; j < ySize; j++)
            {
                tiles[i-1].Add(new Tile(i,j));
            }
        }
    }

    public Tile getTile(int x, int y){
        try
        {
            return tiles[x][y];
        }
        catch (System.Exception)
        {
            return null;
        }
    }

    internal static void moveTo(Being user, Tile target)
    {
       target.occupant = user;
       user.curTile = target;
    }
}

public class Tile : MonoBehaviour{
    public Being occupant = null;
    public Floor floor;
    public Surface surf = Surface.NONE;
    private int x, y;

    public Tile(int x2, int y2){
        x = x2;
        y = y2;
        transform.position = new Vector3(x,y,-1);
    }

    public int getX(){
        return x;
    }
    public int getY(){
        return y;
    }

}

public class Floor{
    //sprite and stuff
    public Sprite image;
}

public enum Surface{
    NONE, FIRE, ACID, LAVA, POISON
}