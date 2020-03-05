using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**

Brian M. 
3/5/2020
The Core class containing beings and other core things
**/
public class Being : MonoBehaviour
{
    private float hp;
    public ResistMap rMap;
}
public enum DamageType{
    SLASH, BLUNT, PIERCE, REND, FIRE, ICE, ELEC, HOLY, DEMONIC
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
        ResistMap r = new ResistMap();
        foreach (var rMap in list)
        {
            foreach (var key in rMap.getKeys())
            {
                
            }
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
            resists.TryGetValue(dt,out rVal)
        }
        rVal+=resistValue;
        if (rVal <0){
            rVal = 0;
        }
        resists.Add(dt,rVal);
    }

     public void replaceResist(DamageType dt, float resistValue){
        if (resists.ContainsKey(dt)){
           resists.Remove(dt); 
        }
            resists.Add(dt,resistValue);
        
    }

    public float getResistMult(DamageType dt){
        if (resists.ContainsKey(dt)){
            float toOut;
            resists.TryGetValue(dt,out toOut);
            return toOut;
        }
        return 1f;
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
