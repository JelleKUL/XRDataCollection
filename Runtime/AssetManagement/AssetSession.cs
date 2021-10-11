using System;
using System.Collections.Generic;
using UnityEngine;


namespace JelleKUL.XRDataCollection
{
    public class AssetSession
    {
        SessionData sessionData = new SessionData();

        public AssetSession()
        {
            sessionData.sessionId = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + sessionData.sessionId);
        }

        public void SaveImage(SimpleTransform simpleTransform, Texture2D imageTexture, int quality = 75)
        {

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

            System.IO.File.WriteAllText(Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + sessionData.sessionId + System.IO.Path.DirectorySeparatorChar + sessionData.jsonid, newJsonstring);
        }
    }

    [System.Serializable]
    public class SessionData
    {
        public string sessionId = "";

        public string jsonid = "SessionData.json";

        public List<SimpleTransform> imageTransforms = new List<SimpleTransform>();

        public List<string> meshIds = new List<string>();
    }

}
