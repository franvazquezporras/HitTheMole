using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoleController : MonoBehaviour
{

    //Variable
    //Mobs
    [SerializeField] private GameObject mole;
    [SerializeField] private GameObject penguin;
    [SerializeField] private GameObject duck;
    [SerializeField] private GameObject plant;
    private enum MoleType { typeMole, typePenguin, typeDuck, typePlant };
    private MoleType mobType;

    //AudiosMob
    [SerializeField] private AudioClip moledeath;
    [SerializeField] private AudioClip duckdeath;
    [SerializeField] private AudioClip penguindeath;
    [SerializeField] private AudioClip plantdeath;
    [SerializeField] private AudioSource soundActive;
    
    //Positions and rates
    private Vector3 startPosition = new Vector3(0f,-5f,0f);
    private Vector3 endPosition = new Vector3(0f, 5f, 0f);
    private float showDuration = 0.5f;
    private float duration = 2f;    
    private bool hittable = true;    
    private float penguinRate = 0.25f;
    private float plantRate = 0.1f;
    private float duckRate = 0.2f;
    
    //score and extra time
    [SerializeField] private int score;  
    GameObject gameController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").gameObject;
    }
    private void Start()
    {
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

    private void CreateNext()
    {
        float random = Random.Range(0f, 1f);
        if(random < plantRate)
        {
            mobType = MoleType.typePlant;
            Instantiate(plant, this.transform);                
        }else if(random < duckRate)
        {
            mobType = MoleType.typeDuck;
            Instantiate(duck, this.transform);    
        }
        else if(random  < penguinRate)
        {
            mobType = MoleType.typePenguin;
            Instantiate(penguin, this.transform);                
        }
        else
        {
            mobType = MoleType.typeMole;
            Instantiate(mole, this.transform);    
        }        
        hittable = true;
    }

    private void OnMouseDown()
    {
        if (hittable)
        {
            switch (mobType)
            {
                case MoleType.typeMole:
                    soundActive.clip = moledeath;
                    score = 1;
                    GetDamage();
                    break;
                case MoleType.typePenguin:
                    soundActive.clip = penguindeath;
                    score = 2;
                    GetDamage();                    
                    break;
                case MoleType.typeDuck:
                    soundActive.clip = duckdeath;
                    score = -1;
                    GetDamage();
                    gameController.GetComponent<Health>().TakeDamage();
                    break;
                case MoleType.typePlant:
                    soundActive.clip = plantdeath;
                    score = 0;
                    gameController.GetComponent<Timer>().SetExtraTime();
                    GetDamage();
                    break;
                default:
                    break;
            }
        }        
    }
    private void GetDamage()
    {
        gameController.GetComponent<ScoreControl>().SetScore(score);
        soundActive.Play();
        gameObject.GetComponent<Collider>().enabled = false;
        StopAllCoroutines();
        StartCoroutine(QuickHide());
        hittable = false;
        Refresh();
    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.25f);
        if(!hittable)
            Hide();
    }


    private void Hide()
    {
        transform.localPosition = startPosition;       
    }

    private void Refresh()
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }
    private IEnumerator ShowHide(Vector3 start, Vector3 end)
    {
        transform.localPosition = start;

        yield return new WaitForSeconds(1);
        float elapsed = 0f;
        
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector3.Lerp(start, end, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameObject.GetComponent<Collider>().enabled = true;
        transform.localPosition = end;
        duration = Random.Range(1, 5);
        yield return new WaitForSeconds(duration);

        elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector3.Lerp(end, start, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = start;
        Refresh();
    }

}
