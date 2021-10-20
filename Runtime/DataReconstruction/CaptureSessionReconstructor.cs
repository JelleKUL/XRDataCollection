using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JelleKUL.XRDataCollection
{
    public class CaptureSessionReconstructor : MonoBehaviour
    {
        [SerializeField]
        private string captureSessionFolderPath = "";
        [SerializeField]
        private string jsonFileName = "SessionData.json";
        [SerializeField]
        private Shader textureShader = null;


        // Start is called before the first frame update
        void Start()
        {
            ReconstructSession();
        }

        public CaptureData GetCaptureData(string path = "")
        {
            if (path == "") path = captureSessionFolderPath;

            string jsonString = System.IO.File.ReadAllText(path + System.IO.Path.DirectorySeparatorChar + jsonFileName);

            CaptureData captureData = (CaptureData)JsonUtility.FromJson(jsonString, typeof(CaptureData));

            return captureData;

        }

        public void ReconstructSession(string path = "")
        {
            if (path == "") path = captureSessionFolderPath;

            CaptureData captureData = GetCaptureData(path);

            foreach (var imgTransform in captureData.imageTransforms)
            {
                PlaceImage(imgTransform);
            }

        }

        public void PlaceImage(SimpleTransform simpleTransform, bool parent = true, float distance = 1)
        {
            Texture2D targetTexture = new Texture2D(2,2);
            targetTexture.LoadImage(System.IO.File.ReadAllBytes(captureSessionFolderPath + System.IO.Path.DirectorySeparatorChar + simpleTransform.id + ".jpg")); //read the bytes from the image file
            targetTexture.wrapMode = TextureWrapMode.Clamp;

            ObjectSpawner.SpawnImage(targetTexture, simpleTransform, textureShader, distance, transform);
        }
    }
}
