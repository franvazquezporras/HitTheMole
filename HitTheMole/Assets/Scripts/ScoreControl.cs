using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{

    //Variables
    [Header("Score Level and Text Score")]
    public int score;
    [SerializeField] private int levelScore;
    [SerializeField] private Text actualScoreText;
    [SerializeField] private Text LevelScoreText;


    /*********************************************************************************************************************************/
    /*Funcion: Awake                                                                                                                 */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza el texto de puntos del nivel al inicio del mismo                                                        */
    /*********************************************************************************************************************************/
    private void Awake()
    {
        LevelScoreText.text = levelScore.ToString();
    }

    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Descripción: Actualiza el texto de los puntos del jugador  durante la partida y desbloquea el siguiente nivel cuando supera    */
    /*              el actual                                                                                                        */
    /*********************************************************************************************************************************/
    private void Update()
    {
        actualScoreText.text = score.ToString();
        if(score >= levelScore)
        {
            if(SceneManager.GetActiveScene().name !="ArcadeMode")
                StartCoroutine(nextLevel());
            gameObject.GetComponent<GameController>().ClearLevel();
        }
    }

    /*********************************************************************************************************************************/
    /*Funcion: SetScore                                                                                                              */
    /*Desarrollador: Vazquez                                                                                                         */
    /*Parametros de entrada: scoreValue (puntos a sumar)                                                                             */
    /*Descripción: Actualiza los puntos del jugador  durante la partida                                                              */
    /*********************************************************************************************************************************/
    public void SetScore(int scoreValue)
    {
        score += scoreValue;
    }


    /*********************************************************************************************************************************/
    /*Funcion: nextLevel                                                                                                             */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Desbloquea el siguiente nivel                                                                                     */
    /*********************************************************************************************************************************/
    private IEnumerator nextLevel()
    {   
        SelectorLevel nextLevel = new SelectorLevel();
        nextLevel.UnlockNewLevel();
        yield return new WaitForSeconds(1);
    }

}
