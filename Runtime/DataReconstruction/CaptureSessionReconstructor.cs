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

        // Update is called once per frame
        void Update()
        {

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
            //open the image from the path


            //create a texture2D and load the image as a texture
            GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            quad.name = simpleTransform.id;

            if (parent)
            {
                quad.transform.parent = transform;
            }

            Texture2D targetTexture = new Texture2D(2,2);
            targetTexture.LoadImage(System.IO.File.ReadAllBytes(captureSessionFolderPath + System.IO.Path.DirectorySeparatorChar + simpleTransform.id + ".jpg")); //read the bytes from the image file
            targetTexture.wrapMode = TextureWrapMode.Clamp;

            float ratio = targetTexture.width / (float)targetTexture.height;
            simpleTransform.fov = Mathf.Clamp(simpleTransform.fov, 0, 179);
            float height = 2 * Mathf.Tan(simpleTransform.fov / 2f * Mathf.Deg2Rad) * distance;
            quad.transform.localScale = new Vector3(ratio, 1, 1) * height;

            Renderer quadRenderer = quad.GetComponent<Renderer>();
            quadRenderer.material = new Material(textureShader);
            quadRenderer.sharedMaterial.SetTexture("_MainTex", targetTexture);
            

            quad.transform.position = simpleTransform.position();
            quad.transform.rotation = simpleTransform.rotation();

        }
    }
}
