using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObstacle: BaseObstacle
{
    public int damageAmount = 5;

    public GameObject impactEffect; 
    public AudioClip impactSound; 
    

    public override void Interact()
    {
       
        Debug.Log("¡Chocaste contra una roca!");

        
        CauseDamage();
        ShowImpactEffect();
    }

    private void CauseDamage()
    {
        
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player != null)
        {
           
            player.TakeDamage(damageAmount);
            Debug.Log("Se causó " + damageAmount + " puntos de daño al jugador.");
        }
        else
        {
            Debug.Log("No se encontró al jugador para causar daño.");
        }
    }

    private void ShowImpactEffect()
    {
        if (impactEffect != null)
        {
           
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
    }
}
