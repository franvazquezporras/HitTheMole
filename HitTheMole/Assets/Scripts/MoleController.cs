using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoleController : MonoBehaviour
{

    //Variable
    [Header("Mob Prefabs")]
    [SerializeField] private GameObject mole;
    [SerializeField] private GameObject penguin;
    [SerializeField] private GameObject duck;
    [SerializeField] private GameObject plant;
    private enum MoleType { typeMole, typePenguin, typeDuck, typePlant };
    private MoleType mobType;

    [Header("Audio")]
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

    [Header("Score Mob")]
    [SerializeField] private int score;  
    GameObject gameController;


    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: asigna la referencia del gamecontroller                                                                           */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").gameObject;
    }

    /*********************************************************************************************************************************/
    /*Funcion: Start                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Genera el primer mob e inicia su coroutine de aparacion                                                           */
    /*********************************************************************************************************************************/
    private void Start()
    {
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }


    /*********************************************************************************************************************************/
    /*Funcion: CreateNext                                                                                                            */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Genera el primer mob de forma aleatoria                                                                           */
    /*********************************************************************************************************************************/
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

    /*********************************************************************************************************************************/
    /*Funcion: OnMouseDown                                                                                                           */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Controla el clic del jugador sobre los distintos enemigos que se muestren en el terreno                           */
    /*             asigna los puntos y si es necesario aumenta el tiempo o reduce el numero de vidas segun el mob                    */
    /*********************************************************************************************************************************/
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

    /*********************************************************************************************************************************/
    /*Funcion: GetDamage                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: suma los puntos del mob, reproduce el sonido de muerte, desactiva su colision una vez muerto y oculta el mob      */
    /*********************************************************************************************************************************/
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


    /*********************************************************************************************************************************/
    /*Funcion: QuickHide                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Oculta el mob una vez clicado                                                                                     */
    /*********************************************************************************************************************************/
    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.25f);
        if(!hittable)
            Hide();
    }

    /*********************************************************************************************************************************/
    /*Funcion: Hide                                                                                                                  */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Oculta el mob volviendolo a su posicion inicial                                                                   */
    /*********************************************************************************************************************************/
    private void Hide()
    {
        transform.localPosition = startPosition;       
    }



    /*********************************************************************************************************************************/
    /*Funcion: Refresh                                                                                                               */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: destruye el ultimo mob y genera uno nuevo                                                                         */
    /*********************************************************************************************************************************/
    private void Refresh()
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

    /*********************************************************************************************************************************/
    /*Funcion: ShowHide                                                                                                              */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: una vez creado el mob se muestra pasado unos segundos, si no es golpeado tras unos segundos vuelve a ocultarse    */
    /*********************************************************************************************************************************/
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
