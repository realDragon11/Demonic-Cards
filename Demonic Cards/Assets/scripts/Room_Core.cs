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
                tiles[i-1].Add(new Tile());
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
}

public class Tile{
    public Being occupant = null;
    public Floor floor;
    public Surface surf = Surface.NONE;
}

public class Floor{
    //sprite and stuff
}

public enum Surface{
    NONE, FIRE, ACID, LAVA, POISON
}