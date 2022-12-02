using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour
{

    //Variable
    [SerializeField] private GameObject mole;
    [SerializeField] private GameObject penguin;
    [SerializeField] private GameObject duck;
    [SerializeField] private GameObject plant;

    private Vector3 startPosition = new Vector3(0f,0f,0f);
    private Vector3 endPosition = new Vector3(0f, 12f, 0f);
    private float showDuration = 0.5f;
    private float duration = 1f;

    private GameObject mob;
    private bool hittable = true;

    public enum MoleType { typeMole, typePenguin,typeDuck,typePlant};
    private MoleType mobType;
    private float penguinRate = 0.25f;
    private int lives;


    private void Awake()
    {
        mob = gameObject;
    }
    private void Start()
    {
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

    private void CreateNext()
    {
        float random = Random.Range(0f, 1f);
        if(random  < penguinRate)
        {
            mobType = MoleType.typePenguin;
            lives = 2;
        }
        else
        {
            mobType = MoleType.typeMole;
            lives = 1;
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
                    StopAllCoroutines();
                    StartCoroutine(QuickHide());
                    hittable = false;
                    break;
                case MoleType.typePenguin:
                    if (lives > 0)                    
                        lives--;
                    else
                    {
                        StopAllCoroutines();
                        StartCoroutine(QuickHide());
                        hittable = false;                        
                    }
                    break;
                case MoleType.typeDuck:
                    break;
                case MoleType.typePlant:
                    break;
                default:
                    break;

            }
            
        }
        
    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.25f);
        if(!hittable)
            Hide();
    }


    public void Hide()
    {
        transform.localPosition = startPosition;
    }
    private IEnumerator ShowHide(Vector3 start, Vector3 end)
    {
        transform.localPosition = start;

        float elapsed = 0f;
        while(elapsed < showDuration)
        {
            transform.localPosition = Vector3.Lerp(start, end, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = end;

        yield return new WaitForSeconds(duration);

        elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector3.Lerp(end, start, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = start;
    }

}
