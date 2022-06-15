using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToDarts : MonoBehaviour
{
    public Fade fade;
    // Start is called before the first frame update
    void Start()
    {        
        fade.FadeOut(0.5f); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
