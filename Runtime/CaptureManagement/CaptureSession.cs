using System;
using System.Collections.Generic;
using UnityEngine;


namespace JelleKUL.XRDataCollection
{
    public class CaptureSession
    {
        CaptureData sessionData = new CaptureData();

        public CaptureSession()
        {
            sessionData.sessionId = "session-" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + sessionData.sessionId);
        }

        public void SaveImage(SimpleTransform simpleTransform, Texture2D imageTexture, int quality = 75)
        {
            Debug.Log("saving data");

            sessionData.imageTransforms.Add(simpleTransform);

            ImageSaver.SaveImage(imageTexture, System.IO.Path.DirectorySeparatorChar + sessionData.sessionId + System.IO.Path.DirectorySeparatorChar + simpleTransform.id + ".jpg", quality);

            UpdateJson();
        }

        public void SaveMesh(Mesh mesh)
        {
            string meshName = "mesh-" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

            sessionData.meshIds.Add(meshName);

            ObjExporter.MeshToFile(mesh, Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + sessionData.sessionId + System.IO.Path.DirectorySeparatorChar + meshName + ".obj");

            UpdateJson();
        }

        void UpdateJson()
        {
            string newJsonstring = JsonUtility.ToJson(sessionData, true);

            System.IO.File.WriteAllText(Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + sessionData.sessionId + System.IO.Path.DirectorySeparatorChar + sessionData.jsonId, newJsonstring);
        }
    }

    [System.Serializable]
    public class CaptureData
    {
        public string sessionId = "";

        public string jsonId = "SessionData.json";

        public Vector3 globalPosition = Vector3.zero;
        public Vector4 globalRotation = Vector4.zero;

        public List<SimpleTransform> imageTransforms = new List<SimpleTransform>();

        public List<string> meshIds = new List<string>();
    }

}
