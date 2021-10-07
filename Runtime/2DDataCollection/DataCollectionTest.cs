using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollectionTest : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private bool grab;

    private void Start()
    {
    }

    public void TakeCameraShot()
    {
        grab = true;
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

        string path = "/" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".jpg";

        if (ImageSaver.SaveImage(targetTexture, path))
        {
            Debug.Log("Succesfully saved the image at path:" + path);
        }

        //Debug.Log(JPGEditor.GetAllJPEGData(path, true));
        string dataToSave = TransformSerialiser.SerializeSimpleTransform(new SimpleTransform(transform));
        Debug.Log("Data to save: " + dataToSave);
        //JPGEditor.SetJPEGData(path, 270, dataToSave, true); 
        // edit the saved picture with the transformdata

    }
}
