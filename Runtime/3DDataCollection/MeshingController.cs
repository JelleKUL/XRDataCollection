using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

namespace JelleKUL.XRDataCollection
{
    /// <summary>
    /// Controls all aspects about the spacial meshing from the mrtk
    /// </summary>
    public class MeshingController : MonoBehaviour
    {
        [SerializeField]
        private ARMeshManager arMeshManager;

        [Header("Bounding Volume")]
        [SerializeField]
        private bool updateBoundingVolume = true;
        [SerializeField]
        private Vector3 boundingExtents = Vector3.one * 3;

        [Header("Visual Parameters")]
        [SerializeField]
        private Color activeOcclusionColor = Color.red;
        private bool occlusionActive = false;

        [Header("Saving Parameters")]
        [SerializeField]
        private AssetSessionManager assetSessionManager;


        private void Update()
        {
            if (updateBoundingVolume)
            {
                if (!CheckMeshManager()) return;
                arMeshManager.subsystem.SetBoundingVolume(Camera.main.transform.position, boundingExtents);
            }


        }

        /// <summary>
        /// Reset the Meshing
        /// </summary>
        public void ResetScan()
        {
            if (!CheckMeshManager()) return;
            StopScanning();
            StartScanning();
        }

        /// <summary>
        /// Start the Meshing
        /// </summary>
        public void StartScanning()
        {
            if (!CheckMeshManager()) return;
            arMeshManager.subsystem.Start();
        }

        /// <summary>
        /// Pause the Meshing
        /// </summary>
        public void StopScanning()
        {
            if (!CheckMeshManager()) return;
            arMeshManager.subsystem.Stop();
        }

        public void SetDensity(float value)
        {
            if (!CheckMeshManager()) return;

            value = Mathf.Clamp(value, 0, 1);
            arMeshManager.density = value;
        }

        public void ToggleOcclusionColor()
        {
            occlusionActive = !occlusionActive;

            Camera.main.backgroundColor = occlusionActive ? activeOcclusionColor : Color.black;
        }

        /// <summary>
        /// Get the full mesh of all loaded observer meshes
        /// </summary>
        /// <returns>a list of all the meshes</returns>
        public List<Mesh> GetSpacialMeshList()
        {
            if (!CheckMeshManager()) return null;
            List<Mesh> meshList = new List<Mesh>();

            foreach (MeshFilter meshObject in arMeshManager.meshes)
            {
                meshList.Add(meshObject.mesh);
            }
            
            return meshList;
        }

        /// <summary>
        /// Get the full mesh of all loaded observer meshes combined into one mesh
        /// </summary>
        /// <returns>One combined mesh</returns>
        public Mesh GetSpacialMesh()
        {
            if (!CheckMeshManager()) return null;
            
            CombineInstance[] combine = new CombineInstance[arMeshManager.meshes.Count];

            for (int i = 0; i < arMeshManager.meshes.Count; i++)
            {
                combine[i].mesh = arMeshManager.meshes[i].mesh;
                combine[i].transform = arMeshManager.meshes[i].transform.localToWorldMatrix;
            }
            
            Mesh newMesh = new Mesh();
            newMesh.CombineMeshes(combine);

            return newMesh;
        }

        public void SaveCombinedMesh()
        {
            if (!assetSessionManager)
            {
                Debug.Log("No AssetSessionManager set...");
                return;
            }
            Mesh meshToSave = GetSpacialMesh();

            assetSessionManager.SaveMesh(meshToSave);

            
        }

        /// <summary>
        /// check if the observer is active in the scene
        /// </summary>
        /// <returns>the existance of the observer in the scene</returns>
        bool CheckMeshManager()
        {
            if (arMeshManager != null)
            {
                return true;
            }
            else
            {
                Debug.LogWarning("*MeshingController*: No mesh manager active");
                return false;
            }
        }
    }
}
