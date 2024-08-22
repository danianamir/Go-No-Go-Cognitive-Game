using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;


public class UiMnager : MonoBehaviour
{



    Canvas canvas;
 
    public GameObject inputField;


    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();

        GameObject instantiatedPrefa = Instantiate(inputField, canvas.transform);

        // input field 





    }

}
