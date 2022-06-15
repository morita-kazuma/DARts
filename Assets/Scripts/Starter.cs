using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour
{
    public Fade fade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonTapped()
    {
        fade.FadeIn(0.5f, () =>
        {
            //SceneManager.sceneLoaded += LoadNextScene;
            SceneManager.LoadScene("ARDarts");            
        });
    }

    /*
    private void LoadNextScene(Scene next, LoadSceneMode mode)
    {
        Debug.Log("loaded");

        FadeToDarts fadeToDarts = GameObject.Find("FadeToDarts").GetComponent<FadeToDarts>();

        fadeToDarts.fade = fade;
                
        SceneManager.sceneLoaded -= LoadNextScene;
    }
    */
}
