using System;
using System.Collections.Generic;
using UnityEngine;

namespace JelleKUL.XRDataCollection
{
    public class CaptureSession
    {
        CaptureData sessionData = new CaptureData();

        /// <summary>
        /// The instantiator for a new session
        /// automatically creates a new folder with the current timestamp.
        /// </summary>
        public CaptureSession()
        {
            sessionData.sessionId = "session-" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + sessionData.sessionId);
        }

        /// <summary>
        /// Adds the image to the session and sends a save request to the ImageSaver
        /// </summary>
        /// <param name="simpleTransform">The transform of the image</param>
        /// <param name="imageTexture">The 2D captured camera texture</param>
        /// <param name="quality">The quality of the Jpeg compression</param>
        public void SaveImage(SimpleTransform simpleTransform, Texture2D imageTexture, int quality = 75)
        {
            sessionData.imageTransforms.Add(simpleTransform);
            ImageSaver.SaveImage(imageTexture, System.IO.Path.DirectorySeparatorChar + sessionData.sessionId + System.IO.Path.DirectorySeparatorChar + simpleTransform.id + ".jpg", quality);
            
            UpdateJson();
        }

        /// <summary>
        ///  Adds the mesh to the session and sends a save request to the ObjExporter
        /// </summary>
        /// <param name="mesh">the mesh to save to obj and session</param>
        public void SaveMesh(Mesh mesh)
        {
            string meshName = "mesh-" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            sessionData.meshIds.Add(meshName);
            ObjExporter.MeshToFile(mesh, Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + sessionData.sessionId + System.IO.Path.DirectorySeparatorChar + meshName + ".obj");

            UpdateJson();
        }

        /// <summary>
        /// Updates the Json file to match the current CaptureData object
        /// </summary>
        void UpdateJson()
        {
            string newJsonstring = JsonUtility.ToJson(sessionData, true);
            System.IO.File.WriteAllText(Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + sessionData.sessionId + System.IO.Path.DirectorySeparatorChar + sessionData.jsonId, newJsonstring);
        }

        /// <summary>
        /// Converts the CaptureData to Json
        /// </summary>
        /// <returns>A formatted string of the current CaptureData</returns>
        public string GetJsonString()
        {
            return JsonUtility.ToJson(sessionData, true);
        }
    }

    /// <summary>
    /// A Data class to store all the CaptureSession info.
    /// </summary>
    [System.Serializable]
    public class CaptureData
    {
        public string sessionId = "";
        public string jsonId = "SessionData.json";
        public List<SimpleTransform> imageTransforms = new List<SimpleTransform>();
        public List<string> meshIds = new List<string>();
    }

}
