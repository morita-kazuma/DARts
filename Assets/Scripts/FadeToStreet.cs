using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToStreet : MonoBehaviour
{
    public Fade fade;
    public GameObject popUp;
    // Start is called before the first frame update
    void Start()
    {        
        fade.FadeOut(1.0f, ()=>
        {
            popUp.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
