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

    void FixedUpdate(){
        if (Mathf.Abs(Input.mousePosition.x-this.transform.position.x) < 1 && Mathf.Abs(Input.mousePosition.y-this.transform.position.y) < 1 ){
                 if (Input.GetMouseButtonDown(1)){
                     onClick();
                 }else{
                     if (Input.GetMouseButtonDown(0)){
                         Handler.h.selectTile(tile);
                     }
                 }
            }
       
    }

    /**broke with the new camera addition
    void OnMouseDown(){
        Debug.Log(tile.getX() + " " + tile.getY() + " clicked");
        onClick();
    }
    **/
}
