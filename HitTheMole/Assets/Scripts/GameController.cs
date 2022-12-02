using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform[] spawnsFloor;
    [SerializeField] private GameObject hole;
    [SerializeField] private int numberOfHoleLevel;
    private Transform[] activeSpawns;



    private void Awake()
    {
        activeSpawns = new Transform[numberOfHoleLevel];
    }
    private void Start()
    {
        int savedHoles = 0;
        for(int block = 0; block < spawnsFloor.Length; block++)
        {
            int save = Random.Range(0, 2);         
            if (save == 1)
            {
                activeSpawns[savedHoles] = spawnsFloor[block];
                savedHoles++;
                if (savedHoles == numberOfHoleLevel)                                   
                    break;      
            }
           
        }
        
        for(int i = 0; i < activeSpawns.Length; i++)
        { 
                //SpawnsFloor[i].transform.GetChild(0).gameObject.SetActive(true);
                Instantiate(hole, new Vector3(activeSpawns[i].position.x, activeSpawns[i].position.y, activeSpawns[i].position.z),hole.transform.rotation*Quaternion.identity);
        }
    }
}
