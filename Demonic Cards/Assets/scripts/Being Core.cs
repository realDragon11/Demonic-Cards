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
