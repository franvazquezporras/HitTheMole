using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationLevel : MonoBehaviour
{
    /*********************************************************************************************************************************/
    /*Funcion: Update                                                                                                                */
    /*Desarrollador: Vazquez                                                                                                         */    
    /*Descripción: Modifica la rotacion del objeto en cada frame (utilizado en la camara del ultimo nivel)                           */
    /*********************************************************************************************************************************/
    void Update()
    {
        transform.Rotate(new Vector3(0f, 15f, 0f) * Time.deltaTime);
    }
}
