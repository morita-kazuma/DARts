using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellectButton: MonoBehaviour
{
    public GameObject arSessionOrigin;
    public GameObject arCamera, subCamera;
    public GameObject warpButton;
    public MapGenerator mapGenerator;

    private TextMeshProUGUI modeText;
    private CollisionMap collisionMap;

    // Start is called before the first frame update
    void Start()
    {   
        modeText = GameObject.Find("ModeText").GetComponent<TextMeshProUGUI>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (arSessionOrigin.GetComponent<MapGenerator>().map != null && collisionMap == null)
        {
            collisionMap = mapGenerator.map.GetComponent<CollisionMap>();
            Debug.Log(collisionMap);
        }
    }

    public void OnButtonTapped()
    {
        arSessionOrigin.GetComponent<ArrowGenerator>().enabled = true;
        arCamera.SetActive(true);
        subCamera.SetActive(false);
        modeText.text = "ダーツモード";
        warpButton.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
