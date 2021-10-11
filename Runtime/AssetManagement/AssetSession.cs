using System;
using System.Collections.Generic;
using UnityEngine;


namespace JelleKUL.XRDataCollection
{
    [System.Serializable]
    public class AssetSession
    {
        public string sessionId = "";

        private string jsonid = "SessionData.json";

        private List<SimpleTransform> imageTransforms = new List<SimpleTransform>();

        private List <string> meshIds = new List<string>();


        public AssetSession()
        {
            sessionId = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + sessionId);
        }

        public void SaveImage(SimpleTransform simpleTransform, Texture2D imageTexture, int quality = 75)
        {

            imageTransforms.Add(simpleTransform);

            ImageSaver.SaveImage(imageTexture, System.IO.Path.DirectorySeparatorChar + sessionId + System.IO.Path.DirectorySeparatorChar + simpleTransform.id + ".jpg", quality);

            UpdateJson();
        }

        public void SaveMesh(Mesh mesh)
        {
            string meshName = "mesh-" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

            meshIds.Add(meshName);

            ObjExporter.MeshToFile(mesh, Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + sessionId + System.IO.Path.DirectorySeparatorChar + meshName + ".obj");

            UpdateJson();
        }

        void UpdateJson()
        {
            string newJsonstring = JsonUtility.ToJson(this);

            System.IO.File.WriteAllText(Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + sessionId + System.IO.Path.DirectorySeparatorChar + jsonid, newJsonstring);
        }
    }
}
