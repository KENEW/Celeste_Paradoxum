using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScene : MonoBehaviour
{
    public AudioClip LogoSound;

    private IEnumerator Start()
    {
        SoundManager.Instance.PlaySFX(LogoSound);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("MainMenuScene");
    }
}