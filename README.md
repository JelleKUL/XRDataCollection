# XRDataCollection
Tools to collect sensory data from XR devices in the Unity engine
> This is still under active development and future versions might introduce breaking changes

```cs
 namespace JelleKUL.XRDataCollection
```
<!-- @import "[TOC]" {cmd="toc" depthFrom=2 depthTo=6 orderedList=false} -->

<!-- code_chunk_output -->

- [XRDataCollection](#xrdatacollection)
  - [ToDo](#todo)
  - [Installation](#installation)
  - [2D Data Collection](#2d-data-collection)
    - [Scripts](#scripts)
      - [CameraCapture](#cameracapture)
      - [ImageSaver](#imagesaver)
      - [TransformSerializer](#transformserializer)
  - [3D Data Collection](#3d-data-collection)
    - [Scripts](#scripts-1)
      - [MeshingController](#meshingcontroller)
      - [ObjExporter](#objexporter)
  - [Data Capture Management](#data-capture-management)
  - [Data Reconstruction](#data-reconstruction)
    - [2D data reconstruction](#2d-data-reconstruction)
    - [3D data reconstruction](#3d-data-reconstruction)
  - [Server Connection](#server-connection)
  - [Licensing](#licensing)

<!-- /code_chunk_output -->

## ToDo

- [ ] Solve wrong placement of image planes
- [ ] Add server connectivity to send data
- [ ] think of more todo's

## Installation

This can be imported as a UnityPackage in any existing Unity project through the [Package manager](https://docs.unity3d.com/Manual/Packages.html) with the Git url.

## 2D Data Collection

Images can be saved, both from a virtual camera of a physical camera using an AR platform.
If the AR platform tracks its transform in the scene, the transform data will also be saved in the session folder

### Scripts

#### CameraCapture

Takes a snapshot of the devices main camera, Currently supprts both AR foundation and Windows.webcam.
Use:
```cs
public void TakeCameraImage()
```

- `ARFoundationCameraCapture`
- `WindowsCameraCapture`


#### ImageSaver

Saves a `RenderTexture2D` to a file given a specified path

#### TransformSerializer

Transforms in Unity are not serializable and would contain to much data anyways. 
A `SimpleTransform` only contains the necessary 

```cs
public class SimpleTransform
{
    public string id = "";

    public Vector3 pos = Vector3.zero;
    public Vector4 rot = Vector4.zero;

    public int fov = 0;
}
```

## 3D Data Collection

3D data collection uses the meshing capabilities of the device. This is currently only supported on the Hololens and Magicleap devices.
Meshes are not linked to a `SimpleTransform` because they are already geo-localised. 

### Scripts

#### MeshingController

Meshing is controlled using the OpenXR and AR foundation's [ARMeshManager](https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.1/manual/mesh-manager.html).
The `MeshingController` can control the bounding box and visibility as well as access the generated meshes.

#### ObjExporter

Exports meshes to an .obj format
A mesh can be saved in the same session folder, 

## Data Capture Management
All the data can be saved to a single folder per session, managed by the `CaptureSessionManager`.
This file can be stored on device at the [Platform specific persistent datapath](https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html).
A central Json file stores all the references to the images and meshes:

```json
{
    "sessionId": "session-yyyy-mm-dd hh-mm-ss",
    "jsonId": "SessionData.json",
    "globalPosition": {
        "x": 0,
        "y": 0,
        "z": 0
    },
    "globalRotation": {
        "x": 0,
        "y": 0,
        "z": 0,
        "w": 1
    },
    "imageTransforms": [
        {
            "id": "img-yyyy-mm-dd hh-mm-ss",
            "pos": {
                "x": 0.0,
                "y": 0.0,
                "z": 0.0
            },
            "rot": {
                "x": 0.0,
                "y": 0.0,
                "z": 0.0,
                "w": 0.0
            },
            "fov": 0
        },
        {
            "id": "img-yyyy-mm-dd hh-mm-ss",
            "pos": {
                "x": 0.0,
                "y": 0.0,
                "z": 0.0
            },
            "rot": {
                "x": 0.0,
                "y": 0.0,
                "z": 0.0,
                "w": 0.0
            },
            "fov": 0
        }
    ],
    "meshIds": [
        "mesh-yyyy-mm-dd hh-mm-ss",
        "mesh-yyyy-mm-dd hh-mm-ss"
    ]
}
```

## Data Reconstruction

The data can be reconstructed from a session.
The `ObjectSpawner` can spawn both the images and 3D models.

### 2D data reconstruction

The images are reconstructed using their `SimpleTransform` to be placed in the scene.

### 3D data reconstruction

Meshes can be simply spawned in the scene as .obj files.
If they are from the same session, the mesh cenxterpoint will be the same as the images.

## Server Connection

The whole `CaptureSession` can be send to a webserver using a post request. It is up to the server to interpret the data.
If the server responds with a calculated location, the App will store it as a reference.

## Licensing

The code in this project is licensed under MIT license.
