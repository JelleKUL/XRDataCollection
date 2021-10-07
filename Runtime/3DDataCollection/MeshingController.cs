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
        // Start is called before the first frame update
        void Start()
        {
        }

        /// <summary>
        /// Reset the mesh observer
        /// </summary>
        public void ResetScan()
        {
            if (!CheckObserver()) return;
            //observer.Reset();
        }
        /// <summary>
        /// Start the mesh observer
        /// </summary>
        public void StartScanning()
        {
            if (!CheckObserver()) return;
            //observer.Resume();
        }

        /// <summary>
        /// Pause the mesh Observer
        /// </summary>
        public void PauseScanning()
        {
            if (!CheckObserver()) return;
            //observer.Suspend();
        }

        /// <summary>
        /// Set the level of detail of the mesh observer
        /// </summary>
        /// <param name="lod">-1: custom, 0: Coarse, 1: Medium, 2: Fine, 255: Unlimited</param>
        /// <param name="customResolution">if lod = custom, this set the triangles per cubic meter</param>
        public void SetLevelOfDetail(int lod, int customResolution = 0)
        {
            if (!CheckObserver()) return;
            /*
            switch (lod)
            {
                case -1:
                    observer.LevelOfDetail = SpatialAwarenessMeshLevelOfDetail.Custom;
                    observer.TrianglesPerCubicMeter = customResolution;
                    break;
                case 0:
                    observer.LevelOfDetail = SpatialAwarenessMeshLevelOfDetail.Coarse;
                    break;

                case 1:
                    observer.LevelOfDetail = SpatialAwarenessMeshLevelOfDetail.Medium;
                    break;
                case 2:
                    observer.LevelOfDetail = SpatialAwarenessMeshLevelOfDetail.Fine;
                    break;
                case 255:
                    observer.LevelOfDetail = SpatialAwarenessMeshLevelOfDetail.Unlimited;
                    break;
                default:
                    observer.LevelOfDetail = SpatialAwarenessMeshLevelOfDetail.Coarse;
                    break;
            }
            */
        }

        /// <summary>
        /// Get the full mesh of all loaded observer meshes
        /// </summary>
        /// <returns>a list of all the meshes</returns>
        public List<Mesh> GetSpacialMeshList()
        {
            if (!CheckObserver()) return null;
            List<Mesh> meshList = new List<Mesh>();
            /*
            foreach (SpatialAwarenessMeshObject meshObject in observer.Meshes.Values)
            {
                meshList.Add(meshObject.Filter.mesh);
            }
            */
            return meshList;
        }

        /// <summary>
        /// Get the full mesh of all loaded observer meshes combined into one mesh
        /// </summary>
        /// <returns>One combined mesh</returns>
        public Mesh GetSpacialMesh()
        {
            if (!CheckObserver()) return null;
            /*
            CombineInstance[] combine = new CombineInstance[observer.Meshes.Count];

            for (int i = 0; i < observer.Meshes.Count; i++)
            {
                combine[i].mesh = observer.Meshes[i].Filter.mesh;
                combine[i].transform = observer.Meshes[i].Filter.transform.localToWorldMatrix;
            }
            */
            Mesh newMesh = new Mesh();
            //newMesh.CombineMeshes(combine);

            return newMesh;
        }

        /// <summary>
        /// check if the observer is active in the scene
        /// </summary>
        /// <returns>the existance of the observer in the scene</returns>
        bool CheckObserver()
        {
            /*
            if (observer != null)
            {
                return true;
            }
            else
            {
                Debug.LogWarning("*MeshingController*: No mesh observer active");
                return false;
            }
            */
            return false;
        }

    }
}
