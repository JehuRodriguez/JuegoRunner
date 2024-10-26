using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObstacle: BaseObstacle
{
    public int damageAmount = 5;

    public override void Interact()
    {
       
        Debug.Log("�Chocaste contra una roca!");

        
        CauseDamage();
        
    }

    private void CauseDamage()
    {
        
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player != null)
        {
           
            player.TakeDamage(damageAmount);
            Debug.Log("Se caus� " + damageAmount + " puntos de da�o al jugador.");
        }
        else
        {
            Debug.Log("No se encontr� al jugador para causar da�o.");
        }
    }

   
}
