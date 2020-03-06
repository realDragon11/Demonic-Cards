using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile_clicker : MonoBehaviour
{
    public Tile tile;
    public tile_clicker(Tile t){
        tile = t;
    }

    public void onClick(){
        Handler.clickTile(tile);
    }

    void OnMouseDown(){
        Debug.Log(tile.getX() + " " + tile.getY() + " clicked");
        onClick();
    }
}
