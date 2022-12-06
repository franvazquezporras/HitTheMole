using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    private int health = 6;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite heartUp;
    public Sprite heartDown;
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
            Time.timeScale = 0;
        }
    }

    public void TakeDamage()
    {
        health--;        
    }
}
