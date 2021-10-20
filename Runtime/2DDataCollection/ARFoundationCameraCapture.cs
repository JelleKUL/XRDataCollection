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
            // Get information about the device camera image.
            if (cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            {
                var format = TextureFormat.RGBA32;
                if (cameraTexture == null || cameraTexture.width != image.width || cameraTexture.height != image.height)
                {
                    cameraTexture = new Texture2D(image.width, image.height, format, false);
                }
                // Choose an RGBA format.
                // See XRCpuImage.FormatSupported for a complete list of supported formats.
                var conversionParams = new XRCpuImage.ConversionParams(image, format);
                // If successful, launch a coroutine that waits for the image
                // to be ready, then apply it to a texture.
                image.ConvertAsync(conversionParams, ProcessImage);

                // It's safe to dispose the image before the async operation completes.
                image.Dispose();
            }
        }

        private void ProcessImage(XRCpuImage.AsyncConversionStatus status, XRCpuImage.ConversionParams conversionParams, NativeArray<byte> data)
        {
            if (status != XRCpuImage.AsyncConversionStatus.Ready)
            {
                Debug.LogErrorFormat("Async request failed with status {0}", status);
                return;
            }

            // Copy the image data into the texture.
            cameraTexture.LoadRawTextureData(data);
            cameraTexture.Apply();

            if (saveImage) SaveImage(cameraTexture, new SimpleTransform(Camera.main.transform));
            if (spawnInScene) SpawnImageInScene(cameraTexture, new SimpleTransform(Camera.main.transform));
        }
    }
}
