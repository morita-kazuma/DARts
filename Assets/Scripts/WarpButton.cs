using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpButton : MonoBehaviour
{
    private TextMeshProUGUI text;
    private GameObject chimei;
    private string latlon;
    private string place;

    public Fade fade;


    void Start()
    {        
       
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        chimei = this.transform.GetChild(0).gameObject;
        text = chimei.GetComponent<TextMeshProUGUI>();

        latlon = text.name;
        place = text.text;
    }

    public void OnButtonTapped()
    {
        fade.FadeIn(0.5f, () =>
        {
            SceneManager.sceneLoaded += LoadNextScene;
            SceneManager.LoadScene("StreetView");
        });
    }

    private void LoadNextScene(Scene next, LoadSceneMode mode)
    {
        Debug.Log("loaded");
        StreetView streetView = GameObject.Find("StreetView").GetComponent<StreetView>();
        GameObject[] textObjects = GameObject.FindGameObjectsWithTag("Text");
        foreach(GameObject textObject in textObjects)
        {
            textObject.GetComponent<TextMeshProUGUI>().text = place;
        }        
        streetView.latlon = latlon;
        SceneManager.sceneLoaded -= LoadNextScene;
    }
}
