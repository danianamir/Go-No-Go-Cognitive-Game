using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class start : MonoBehaviour
{

    public static string adress;
    public static string out_adress;
    public static string id_name;


    public TMP_InputField inputField_adress;
    public TMP_InputField output_adress;
    public TMP_InputField id;




    private void Update()
    {
        if (inputField_adress.text != "")
        {
            adress = inputField_adress.text;



        }

        if (output_adress.text != "")
        {
            out_adress = output_adress.text;



        }
        else
        {
            out_adress = Application.dataPath;
            string parentFolder = Directory.GetParent(out_adress).FullName;
            string parentFolder2= Directory.GetParent(parentFolder).FullName;
            out_adress = Path.Combine(parentFolder2, "outputs");
        }

        if (id.text != "")
        {
            id_name = id.text;



        }

    }








    public void change_scene()
    {
        if (inputField_adress.text != ""  && id.text != "")
        {
            SceneManager.LoadScene("GONOGO");
        }
        else
        {

        }

    }
}
