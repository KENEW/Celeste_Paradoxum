using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BillageButton : MonoBehaviour
{
    public void OnButtonFirst()
    {
        SceneManager.LoadScene("Patience");
    }

    public void OnButtonSecond()
    {
        SceneManager.LoadScene("Charity");
    }

    public void OnButtonThird()
    {
        SceneManager.LoadScene("Kindness");
    }

    public void OnButtonFourth()
    {
        SceneManager.LoadScene("Chastity");
    }
}