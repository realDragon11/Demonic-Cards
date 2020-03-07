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
        
        if (Mathf.Abs(Handler.h.mouseScreenPoint.x-this.transform.position.x) < .5f && Mathf.Abs(Handler.h.mouseScreenPoint.y-this.transform.position.y) < .5f ){
                ;
                 if (Input.GetMouseButtonDown(1)){
                     Debug.Log("Mouse detected at " + tile.getX() + " " + tile.getY());
                     onClick();
                 }else{
                     if (Input.GetMouseButtonDown(0)){
                         Debug.Log("Mouse detected at " + tile.getX() + " " + tile.getY());
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
