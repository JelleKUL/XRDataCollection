using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace JelleKUL.XRDataCollection
{

    public class LocationImageReconstructor : MonoBehaviour
    {
        private Camera cam;

        [SerializeField]
        private Shader textureShader = null;
        [SerializeField]
        private int descriptionId = 270;

        // Start is called before the first frame update
        void Start()
        {
            PlaceAllImages(Application.persistentDataPath);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlaceImage(string assetPath, float baseScale = 1, bool parent = false)
        {
            //open the image from the path


            //create a texture2D and load the image as a texture
            GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            quad.name = "Photo_" + assetPath.Substring(assetPath.LastIndexOf("\\") + 1, assetPath.Length - assetPath.LastIndexOf("\\") - 1);
            if (parent)
            {
                quad.transform.parent = transform;
            }

            float ratio = Screen.height / (float)Screen.width;
            quad.transform.localScale = new Vector3(quad.transform.localScale.x, quad.transform.localScale.x * ratio, quad.transform.localScale.z) * baseScale;

            Renderer quadRenderer = quad.GetComponent<Renderer>();
            quadRenderer.material = new Material(textureShader);
            Texture2D targetTexture = new Texture2D(Screen.width, Screen.height);
            targetTexture.LoadImage(File.ReadAllBytes(assetPath)); //read the bytes from the image file
            quadRenderer.sharedMaterial.SetTexture("_MainTex", targetTexture);

            //create a quad and place it in the correct transform from the image
            string jsonTransformData = ""; //JPGEditor.GetJPEGData(assetPath, descriptionId);
            Debug.Log("JsonData: " + jsonTransformData);
            SimpleTransform newPosData = TransformSerialiser.DeSerializeTransform(jsonTransformData);

            targetTexture.wrapMode = TextureWrapMode.Clamp;

            quad.transform.position = newPosData.position();
            quad.transform.rotation = newPosData.rotation();

        }

        public void PlaceAllImages(string folderPath)
        {
            string[] filePaths = Directory.GetFiles(folderPath, "*.jpg");

            foreach (string path in filePaths)
            {
                PlaceImage(path);
            }
        }
    }
}
