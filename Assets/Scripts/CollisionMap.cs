using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

[Serializable]
public class Result
{
    public code plus_code;
}

[Serializable]
public class code
{
    public string compound_code;
}

[Serializable]
public class MetaData
{
    public lle location;
    public string status;
}

[Serializable]
public class lle
{
    public double lat;
    public double lng;
}

public class CollisionMap : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 mapCoor;
    private Vector3 hitPos;
    private double lat, lon;
    private GameObject dispText;
    private GameObject arrowTmp;

    [HideInInspector] public GameObject arrowHit;
    [HideInInspector] public Vector3 zoomCamera;

    void Start()
    {
        dispText = GameObject.Find("Canvas").transform.Find("WarpButton").GetChild(0).gameObject;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        arrowTmp = other.gameObject;
        foreach (ContactPoint point in other.contacts)
        {
            hitPos = point.point;
            mapCoor = this.transform.InverseTransformPoint(hitPos);
            Debug.Log(mapCoor);
            MapCoor2XY(mapCoor, out double X, out double Y, out double ZoneN);

            Coordinate.cXY.UTMToDes(X, Y, ZoneN, out double Lat, out double Lon);

            lat = Lat;
            lon = Lon;
            
            StartCoroutine(Method());
        }
    }

    private IEnumerator Method()
    {
        string url = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat.ToString("F7") + "," + lon.ToString("F7") + "&language=ja&result_type=administrative_area_level_1|locality&key=＜API KEY>";
        //1.UnityWebRequestを生成
        UnityWebRequest request = UnityWebRequest.Get(url);
                
        //2.SendWebRequestを実行し、送受信開始
        yield return request.SendWebRequest();

        //3.isNetworkErrorとisHttpErrorでエラー判定
        if (request.isHttpError || request.isNetworkError)
        {
            //4.エラー確認
            Debug.Log("error");
        }
        else
        {
            //4.結果確認
            //Debug.Log(request.downloadHandler.text);
            Result response = JsonUtility.FromJson<Result>(request.downloadHandler.text);
            if (response.plus_code.compound_code != null)
            {
                string plusCode = response.plus_code.compound_code;
                if(plusCode.IndexOf('、') > 0)
                {
                    string urlMeta = "https://maps.googleapis.com/maps/api/streetview/metadata?size=400x400&location=" + lat.ToString("F7") + "," + lon.ToString("F7") + "&source=outdoor&radius=1000000&key=＜API KEY>";
                    UnityWebRequest requestMeta = UnityWebRequest.Get(urlMeta);
                    yield return requestMeta.SendWebRequest();
                    if (requestMeta.isHttpError || requestMeta.isNetworkError)
                    {
                        //4.エラー確認
                        Debug.Log(requestMeta.error);
                    }
                    else
                    {
                        //4.結果確認
                        Debug.Log(requestMeta.downloadHandler.text);
                        MetaData responseMeta = JsonUtility.FromJson<MetaData>(requestMeta.downloadHandler.text);
                        if(responseMeta.status == "OK")
                        {
                            lat = responseMeta.location.lat;
                            lon = responseMeta.location.lng;
                            string chimei = plusCode.Substring(plusCode.IndexOf('、') + 1);
                            arrowHit = arrowTmp;
                            zoomCamera = this.transform.TransformPoint(new Vector3(mapCoor.x, mapCoor.y, -2.0f));
                            TextMeshProUGUI text = dispText.GetComponent<TextMeshProUGUI>();
                            text.name = lat.ToString("F10") + "," + lon.ToString("F10");
                            text.text = chimei;
                            Debug.Log(chimei);
                        }                        
                    }
                }                               
            }
        }
    }

    private void MapCoor2XY(Vector3 mapCoor, out double X, out double Y, out double ZoneN)
    {
        double mapX = (double)mapCoor.x;
        double mapY = (double)mapCoor.y;

        if (mapX >= -1 && mapX < -0.5)
        {
            ZoneN = 52.0d;
            X = (mapX + 1.0d) / 0.5d * 500000.0d + 250000.0d;
        }
        else if (mapX >= -0.5 && mapX < 0)
        {
            ZoneN = 53.0d;
            X = (mapX + 0.5d) / 0.5d * 500000.0d + 250000.0d;
        }
        else if (mapX >= 0 && mapX < 0.5)
        {
            ZoneN = 54.0d;
            X = mapX / 0.5d * 500000.0d + 250000.0d;
        }
        else if (mapX >= 0.5 && mapX < 1)
        {
            ZoneN = 55.0d;
            X = (mapX - 0.5d) / 0.5d * 500000.0d + 250000.0d;
        }
        else
        {
            ZoneN = 54.0d;
            X = mapX / 0.5d * 500000.0d + 250000.0d;
        }

        Y = ((mapY * 10.0d) + 37.2d) / 90.0d * 10000000.0d;
    }
}
