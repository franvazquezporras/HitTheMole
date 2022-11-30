using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject[] SpawnsFloor;
    public GameObject hole;

    private void Start()
    {
        SpawnsFloor = GameObject.FindGameObjectsWithTag("Floor");

        for(int i = 0; i < SpawnsFloor.Length; i++)
        {
            Debug.Log("bloque: " + SpawnsFloor[i].name);
            if(i == 5)
            {
                //SpawnsFloor[i].transform.GetChild(0).gameObject.SetActive(true);
                Instantiate(hole, new Vector3(SpawnsFloor[i].transform.position.x, SpawnsFloor[i].transform.position.y+5.7f, SpawnsFloor[i].transform.position.z),hole.transform.rotation*Quaternion.identity);
            }
        }
    }
}
