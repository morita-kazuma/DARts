using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartText : MonoBehaviour
{   
    private RectTransform firstTrans, secondTrans, thirdTrans;
    private float zAngle;

    public GameObject first, second, third;
    // Start is called before the first frame update
    void Start()
    {
        firstTrans = first.GetComponent<RectTransform>();
        secondTrans = second.GetComponent<RectTransform>();
        thirdTrans = third.GetComponent<RectTransform>();
        firstTrans.localPosition = new Vector3(800, 100, 0);
        secondTrans.localPosition = new Vector3(-800, -100, 0);
        thirdTrans.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (firstTrans.localPosition.x > 0)
        {
            firstTrans.position += new Vector3(-Time.deltaTime / 1.0f * 800.0f, 0, 0);
            secondTrans.position += new Vector3(Time.deltaTime / 1.0f * 800.0f, 0, 0);
            zAngle = 0;
        }
        else if (firstTrans.localPosition.x < 0)
        {
            firstTrans.localPosition = new Vector3(0, 100, 0);
            secondTrans.localPosition = new Vector3(0, -100, 0);
        }
        else {
            if (thirdTrans.localScale.x < 1)
            {
                thirdTrans.localScale += new Vector3(2, 2, 2) * Time.deltaTime;
                zAngle += 365 * Time.deltaTime * 2;
                thirdTrans.localRotation = Quaternion.Euler(0, 0, zAngle);
                firstTrans.localPosition += new Vector3(0, Time.deltaTime * 120, 0);
                secondTrans.localPosition += new Vector3(0, -Time.deltaTime * 120, 0);
            }
            else if(thirdTrans.localScale.x > 1)
            {
                thirdTrans.localScale = new Vector3(1, 1, 1);
                thirdTrans.localRotation = Quaternion.Euler(0, 0, 10);
                firstTrans.localPosition = new Vector3(0, 160, 0);
                secondTrans.localPosition = new Vector3(0, -160, 0);
            }
        }
    }
}
