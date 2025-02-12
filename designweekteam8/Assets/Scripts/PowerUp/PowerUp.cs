using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "Power Up") ]
public class PowerUp : ScriptableObject
{
    public float duration = 10f; //Duration

    public string powerUpName;

    public virtual void Use(GameObject player)
    {
        Debug.Log($"Power Up used: {powerUpName}");
    }

    public virtual void End(GameObject player)
    {
        Debug.Log($"Power Up: {powerUpName}");
    }

    //public abstract void Use(GameObject player);

    //public abstract void End(GameObject player);

    //public IEnumerator PowerUpTimer(GameObject player)
    //{
    //    Use(player);
    //    yield return new WaitForSeconds(duration);
    //    End(player);
    //}
}
