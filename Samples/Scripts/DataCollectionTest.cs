using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JelleKUL.XRDataCollection;

public class DataCollectionTest : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Mesh saveMesh;

    [SerializeField]
    AssetSessionManager assetSessionManager;

    private bool grab;

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }

    public void TakeCameraShot()
    {
        
        grab = true;
    }

    public void SaveMesh()
    {
        if (saveMesh)
        {
            Debug.Log("Attempting to save a mesh");
            assetSessionManager.SaveMesh(saveMesh);
        }
    }

    public void GetPicInfo()
    {
       // Debug.Log(JPGEditor.GetAllJPEGData(Application.persistentDataPath + "/testImg.jpg"));
    }

    private void OnPostRender()
    {
        if (!grab) return;
        if (!cam) return;
        grab = false;

        SavePicture();

    }

    private void SavePicture()
    {
        Debug.Log("attempting to take a picture");

        Texture2D targetTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        targetTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        targetTexture.Apply();

        assetSessionManager.SaveImage(new SimpleTransform(Camera.main.transform), targetTexture);

    }


}
