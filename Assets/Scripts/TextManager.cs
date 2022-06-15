using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class TextManager : MonoBehaviour
{
    // Start is called before the first frame update
    private ARPlaneManager planeManager;
    private bool planeFound = false;
    private bool arrowGenerated = false;
    private bool mapGenerated = false;
    private TextMeshProUGUI naviText;

    public MapGenerator mapGenerator;
    public ArrowGenerator arrowGenerator;
    public GameObject reloadButton;

    void Start()
    {
        planeManager = this.GetComponent<ARPlaneManager>();
        naviText = GameObject.Find("NaviText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    { 
        //Debug.Log(planeManager.trackables!=null);
        if (!planeFound)
        {
            foreach(ARPlane plane in planeManager.trackables)
            {
                planeFound = true;
                naviText.text = "ダーツの的を設置したい場所をタップ";
                
            }                       
        }
              
        if(mapGenerator.isGenerated && !mapGenerated)
        {
            naviText.text = "画面をスワイプしてダーツの矢を投げてください";
            mapGenerated = true;
        }

        if (mapGenerator.isGenerated && !arrowGenerated)
        {
            if (arrowGenerator.arrowGenerated)
            {
                reloadButton.SetActive(true);
                naviText.enabled = false;
                arrowGenerated = true;
            }
        }
    }
}
