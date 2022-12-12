using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    //Variables
    [Header("Max Lifes")]
    private int health = 6;
    public int numOfHearts;

    [Header("Sprites Hearts")]
    public Image[] hearts;
    public Sprite heartUp;
    public Sprite heartDown;

    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza las vidas del player y controla cuando llegue a 0 para llamar a la funcion de LosePanel                 */
    /*********************************************************************************************************************************/
    private void Update()
    {
        if (health > numOfHearts)
            health = numOfHearts;
        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < health)            
                hearts[i].sprite = heartUp;            
            else            
                hearts[i].sprite = heartDown;            
            if (i < numOfHearts)            
                hearts[i].enabled = true;            
            else            
                hearts[i].enabled = false;            
        }
        if (health == 0)
        {
            GetComponent<GameController>().LosePanel();
        }
    }
    /*********************************************************************************************************************************/
    /*Funcion: TakeDamage                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Reduce el numero de vidas en 1                                                                                    */
    /*********************************************************************************************************************************/
    public void TakeDamage()
    {
        health--;        
    }
}
