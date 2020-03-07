using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
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
                tiles[i].Add(new Tile(i,j));
            }
        }
    }

    public Tile getTileRelative(Tile tile, TileOffset t)
    {
       try
        {
            return tiles[tile.getX()+t.xOff][tile.getY()+t.yOff];
        }
        catch (System.Exception)
        {
            return null;
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

    public static void moveTo(Being user, Tile target)
    {
        if (target.occupant != null){
            throw new GenericRuntimeException("Tile already occupied!");
        }
        if (user.getTile() != null){
        user.getTile().occupant = null;}
       target.occupant = user;
       user.setCurTile(target);
    }

    public void selects()
    {
        foreach (List<Tile> ts in tiles)
        {
            foreach (Tile t in ts)
            {
                t.linked.selectable.SetActive(false);
            }
        }
        foreach (TileOffset tOff in Being.turnWho.curCard.getTileSet().tos)
        {
            Tile t = getTile(Being.turnWho.getTile().getX()+tOff.xOff,Being.turnWho.getTile().getY()+tOff.yOff);
            if (t != null){
                t.linked.selectable.SetActive(true);
            }
        }
    }
}

public class Tile{
    public Being occupant = null;
    public Floor floor;
    public Surface surf = Surface.NONE;
    private int x, y;
    public tile_clicker linked;

    public Tile(int x2, int y2){
        x = x2;
        y = y2;
        //transform.position = new Vector3(x,y,-1);
        linked =  (GameObject.Instantiate(Handler.h.ct)).GetComponent<tile_clicker>();
        linked.gameObject.transform.position = new Vector3(x,y,-1);
        linked.tile = this;
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