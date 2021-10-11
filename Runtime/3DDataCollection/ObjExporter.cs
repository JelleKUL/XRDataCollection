using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;


namespace JelleKUL.XRDataCollection
{
    /// <summary>
    /// Methods to save obj's
    /// </summary>
    public class ObjExporter
    {
        /// <summary>
        /// Converts a mesh to an obj formatted string.
        /// </summary>
        /// <param name="m">the mesh to format</param>
        /// <returns>a formatted string</returns>
        public static string MeshToString(Mesh m)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("g ").Append(m.name).Append("\n");
            foreach (Vector3 v in m.vertices)
            {
                sb.Append(string.Format("v {0} {1} {2}\n", v.x, v.y, v.z));
            }
            sb.Append("\n");
            foreach (Vector3 v in m.normals)
            {
                sb.Append(string.Format("vn {0} {1} {2}\n", v.x, v.y, v.z));
            }
            sb.Append("\n");
            foreach (Vector3 v in m.uv)
            {
                sb.Append(string.Format("vt {0} {1}\n", v.x, v.y));
            }
            return sb.ToString();
        }

        /// <summary>
        /// saves a mesh to an .obj file
        /// </summary>
        /// <param name="m">the mesh to convert</param>
        /// <param name="filename">the filename and path to save the mesh to.</param>
        public static void MeshToFile(Mesh m, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(MeshToString(m));
            }
        }
    }
}
