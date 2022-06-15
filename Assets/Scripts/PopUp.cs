using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject chimei;
    public GameObject telop;
    // Start is called before the first frame update
    void Start()
    {
        chimei.GetComponent<RectTransform>().localPosition= new Vector3(0, -160, 0);
        telop.SetActive(false);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(chimei.GetComponent<RectTransform>().localPosition.y < 0)
        {
            chimei.GetComponent<RectTransform>().Translate(0, Time.deltaTime / 2.0f * 150.0f, 0);
        }
        else
        {
            StartCoroutine(Disappear());        
        }
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(2.0f);
        telop.SetActive(true);
        this.gameObject.SetActive(false);
    }


}
