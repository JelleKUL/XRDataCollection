using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace JelleKUL.XRDataCollection
{
    public class ARFoundationCameraCapture : BaseXRCameraCapture
    {
        [SerializeField]
        private ARCameraManager cameraManager;

        public override void TakeCameraImage()
        {
            GetImageAsync();
        }

        private void GetImageAsync()
        {
            Debug.Log("Starting photo capture");
            // Get information about the device camera image.
            if (cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            {
                Debug.Log("Got an image from the GPU");
                var format = TextureFormat.RGBA32;
                if (cameraTexture == null || cameraTexture.width != image.width || cameraTexture.height != image.height)
                {
                    cameraTexture = new Texture2D(image.width, image.height, format, false);
                }
                // Choose an RGBA format.
                // See XRCpuImage.FormatSupported for a complete list of supported formats.
                // If successful, launch a coroutine that waits for the image
                // to be ready, then apply it to a texture.
                //image.ConvertAsync(conversionParams, ProcessImage);
                StartCoroutine(ProcessImage(image));

                // It's safe to dispose the image before the async operation completes.
                image.Dispose();
            }
        }

        IEnumerator ProcessImage(XRCpuImage image)
        {
            Debug.Log("processing the image");

            // Create the async conversion request.
            var request = image.ConvertAsync(new XRCpuImage.ConversionParams
            {
                // Use the full image.
                inputRect = new RectInt(0, 0, image.width, image.height),

                outputDimensions = new Vector2Int(image.width, image.height),

                // Color image format.
                outputFormat = TextureFormat.RGBA32,

                transformation = XRCpuImage.Transformation.MirrorX,
            });

            // Wait for the conversion to complete.
            while (!request.status.IsDone())
                yield return null;

            if (request.status != XRCpuImage.AsyncConversionStatus.Ready)
            {
                Debug.LogErrorFormat("Async request failed with status {0}", request.status);
                request.Dispose();
                yield break;
            }
            Debug.Log("the image is ready");
            // Copy the image data into the texture
            cameraTexture.LoadRawTextureData(request.GetData<byte>());
            cameraTexture.Apply();

            image.Dispose();

            Debug.Log("the image loaded to the texture");
            Debug.Log(cameraTexture + "size: " + cameraTexture.width + " x " + cameraTexture.height);

            SimpleTransform newTransform = new SimpleTransform(cameraManager.transform);
            Debug.Log("made the new simple transform: " + newTransform.ToString());

            if (saveImage) SaveImage(cameraTexture, newTransform); //new SimpleTransform(cameraManager.transform));
            if (spawnInScene) SpawnImageInScene(cameraTexture, newTransform); //new SimpleTransform(cameraManager.transform));
        }

    }
}
