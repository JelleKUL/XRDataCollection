using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.Collections;

namespace JelleKUL.XRDataCollection
{
    public class ARCameraCapture : MonoBehaviour
    {
        [SerializeField]
        private ARCameraManager cameraManager;
        [SerializeField]
        private bool saveImage = false;
        [SerializeField]
        private CaptureSessionManager captureSessionManager;

        private Texture2D cameraTexture;

        public void GetImageAsync()
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

        void ProcessImage(XRCpuImage.AsyncConversionStatus status, XRCpuImage.ConversionParams conversionParams, NativeArray<byte> data)
        {
            if (status != XRCpuImage.AsyncConversionStatus.Ready)
            {
                Debug.LogErrorFormat("Async request failed with status {0}", status);
                return;
            }

            // Copy the image data into the texture.
            cameraTexture.LoadRawTextureData(data);
            cameraTexture.Apply();

            if (saveImage)
            {
                captureSessionManager.SaveImage(new SimpleTransform(Camera.main.transform), cameraTexture);
            }
        }

    }
}
