using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UI;
using System.IO;
using System.ComponentModel;
using System;
using UnityEngine;
using System.Collections;
using System.Diagnostics;


public class HyperImage : UnityEngine.MonoBehaviour
{
    private GameObject[] cubes;
    private GameObject cube;
    public string imageUrl;
    void Start()
    {
        StartCoroutine(GetText());
        cubes = GameObject.FindGameObjectsWithTag("cube");
        cube = cubes[0];
    }

    IEnumerator GetText()
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                UnityEngine.Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded asset bundle
                var texture = DownloadHandlerTexture.GetContent(uwr);
                cube.GetComponent<Renderer>().materials[0].mainTexture=texture;
                UnityEngine.Debug.Log("swapped texture!");
            }
        }
    }
}