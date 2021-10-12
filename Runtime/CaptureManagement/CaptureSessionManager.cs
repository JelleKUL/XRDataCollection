using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace JelleKUL.XRDataCollection
{
    /// <summary>
    /// Manage a bunch of assets to save in one place during a session
    /// </summary>
    public class CaptureSessionManager : MonoBehaviour
    {
        private CaptureSession assetSession;

        public void SaveImage(SimpleTransform simpleTransform, Texture2D imageTexture, int quality = 75)
        {
            CheckSession();
            assetSession.SaveImage(simpleTransform, imageTexture, quality);
        }

        public void SaveMesh(Mesh mesh)
        {
            CheckSession();
            assetSession.SaveMesh(mesh);
        }

        public void CreateNewSession()
        {
            assetSession = new CaptureSession();
        }


        void CheckSession()
        {
            if(assetSession == null)
            {
                assetSession = new CaptureSession();
                
            }
        }
    }
}
