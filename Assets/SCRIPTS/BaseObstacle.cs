using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    public bool isCollectible;
    public int damage;
    public int scoreValue;
    public string obstacleType;

    
    public virtual void Interact()
    {
        Debug.Log($"{obstacleType} interactuado.");
    }

    public void CauseDamage(PlayerCharacter player)
    {
        if (player != null)
        {
            player.TakeDamage(damage);
            Debug.Log($"El jugador recibió {damage} de daño por el {obstacleType}.");
        }
    }

    public void Collect()
    {
        if (isCollectible)
        {
            
            Debug.Log($"{obstacleType} recogido. Puntos: {scoreValue}.");
            Destroy(gameObject); 
        }
    }

    public bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
