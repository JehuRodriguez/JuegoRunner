using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : BaseObstacle
{
    public new int scoreValue = 1;

    public override void Interact()
    {
       
        Collect(); 
    }

    public  override void Collect()
    {
        
        PlayerCharacter player = FindObjectOfType<PlayerCharacter>();

        if (player != null)
        {
            
            player.score += scoreValue; 
            Debug.Log("¡Moneda recolectada! Puntos: " + scoreValue);

           
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No se encontró al jugador para aumentar la puntuación.");
        }
    }
}
