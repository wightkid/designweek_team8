using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "Power Up") ]
public class PowerUp : ScriptableObject
{
    //This script doesn't need to be applied to anything
    public string powerUpName;

    public virtual void Use(GameObject player)
    {
        Debug.Log($"Power Up used: {powerUpName}");
    }

    public virtual void End(GameObject player)
    {
        Debug.Log($"Power Up: {powerUpName}");
    }
}
