using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCard : MonoBehaviour, System.IComparable<BaseCard>
{
    string cardName;
    string id;
    Texture2D image;
    string description;
    float ballSpeed;
    
    public int CompareTo(BaseCard other)
    {
        if (other == null) return 1;
        return id.CompareTo(other.id);
    }
    
}
