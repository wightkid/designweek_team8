using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "Power Up") ]
public class PowerUp : ScriptableObject
{
    public float duration; //Duration

    public string powerUpName;

    public virtual void Use(GameObject player)
    {
        Debug.Log($"Power Up used: {powerUpName}");
    }
}
