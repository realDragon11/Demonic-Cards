using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    //5 x2 weapon cards, 2 x5 armor cards, 5 class cards, 5 backstory cards
    //nts: make all cards singletons

    public abstract TileSet getTileSet();
    public abstract void use(Being user, Tile target);
}

public class TileSet{

}