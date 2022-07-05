using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnClick : MonoBehaviour
{
    public static int width=1;
    public static int length=1;
    public static bool isNew;

    public InputField LengthField;
    public InputField WidthField;

    public void threeTimes()
    {
        isNew = true;
        width = 3;
        length = 3;
        SceneManager.LoadScene("buildScene");
    }

    public void sixTimes()
    {
        isNew = true;
        width = 6;
        length = 6;
        SceneManager.LoadScene("buildScene");
    }

    public void CustomSize()
    {
        isNew = true;
        width = int.Parse(WidthField.text);
        length = int.Parse(LengthField.text);
        SceneManager.LoadScene("buildScene");
    }

    public void MainMenu()
    {
        GameObject[] furnitureGO = GameObject.FindGameObjectsWithTag("Furniture");
        foreach (GameObject furniture in furnitureGO)
        {
            Destroy(furniture);
        }
        SceneManager.LoadScene("menuScene");
    }

}