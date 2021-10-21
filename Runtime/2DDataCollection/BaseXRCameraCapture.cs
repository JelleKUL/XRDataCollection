using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JelleKUL.XRDataCollection
{
    public class BaseXRCameraCapture : MonoBehaviour
    {
        [Header("Save Parameters")]
        [SerializeField]
        protected bool saveImage = true;
        [SerializeField]
        protected bool useCaptureSession = false;
        [SerializeField]
        protected CaptureSessionManager captureSessionManager;
        [SerializeField]
        [Tooltip("Only used if captureSessionManager is not used")]
        private string customSavePath = "image.jpg";

        [Header("Spawn Parameters")]
        [SerializeField]
        protected bool spawnInScene = true;
        [SerializeField]
        private Shader textureShader = null;
        [SerializeField]
        protected TextMesh text = null;
        [SerializeField]
        private float imageSpawnDistance = 1f;

        protected Texture2D cameraTexture;

        public virtual void TakeCameraImage()
        {

        }

        public void SpawnImageInScene(Texture2D image, SimpleTransform simpleTransform)
        {
            ObjectSpawner.SpawnImage(image, simpleTransform, textureShader, imageSpawnDistance, transform);
            LogText("Image spawned in the scene");
        }

        public void SaveImage(Texture2D image, SimpleTransform simpleTransform)
        {
            if (useCaptureSession && captureSessionManager)
            {
                captureSessionManager.SaveImage(simpleTransform, image);
                LogText("Image saved by the capture sessionManager");
            }
            else
            {
                ImageSaver.SaveImage(image, customSavePath);
                LogText("Image saved @ custom save path");
            }
        }

        void LogText(string message, bool reset = false)
        {
            if (text)
            {
                text.text = (reset? "":text.text) + "\n" + message;
            }
            else
            {
                Debug.Log((reset ? "" : text.text) + "\n" + message);
            }
        }
    }
}
