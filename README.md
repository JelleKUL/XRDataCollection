# XRDataCollection
Tools to collect sensory data from XR devices in the Unity engine
> This is still under active development and future versions might introduce breaking changes

```cs
 namespace JelleKUL.XRDataCollection
```
<!-- @import "[TOC]" {cmd="toc" depthFrom=2 depthTo=6 orderedList=false} -->

<!-- code_chunk_output -->

- [Installation](#installation)
- [2D Data Collection](#2d-data-collection)
  - [Scripts](#scripts)
    - [LocationCamera](#locationcamera)
    - [ImageSaver](#imagesaver)
    - [TransformSerializer](#transformserializer)
- [3D Data Collection](#3d-data-collection)
  - [Scripts](#scripts-1)
    - [MeshingController](#meshingcontroller)
    - [ObjExporter](#objexporter)
- [Data Capture Management](#data-capture-management)
- [Data Reconstruction](#data-reconstruction)
- [Licensing](#licensing)

<!-- /code_chunk_output -->

## Installation

This can be imported as a UnityPackage in any existing Unity project through the [Package manager](https://docs.unity3d.com/Manual/Packages.html) with the Git url.

## 2D Data Collection

Images can be saved, both from a virtual camera of a physical camera using an AR platform.
If the AR platform tracks its transform in the scene, the transform data will also be saved in the session folder

### Scripts

#### LocationCamera

Takes a snapshot of the devices main camera, currently using the `Windows.webcam` namespace
> **todo** add cross platform support && merge windows and AR foundation scripts

#### ImageSaver

Saves a `RenderTexture2D` to a file given a specified path

#### TransformSerializer

Transforms in Unity are not serializable and would contain to much data anyways. 
A `SimpleTransform` only contains the necessary 

```cs
public class SimpleTransform
{
    public string id = "";

    public string px = "";
    public string py = "";
    public string pz = "";
    
    public string rx = "";
    public string ry = "";
    public string rz = "";
    public string rw = "";

    public int fov = 0;
}
```

## 3D Data Collection

3D data collection uses the meshing capabilities of the device. This is currently only supported on the Hololens and Magicleap devices.

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
    "imageTransforms": [
        {
            "id": "img-yyyy-mm-dd hh-mm-ss",
            "px": "0",
            "py": "0",
            "pz": "0",
            "rx": "0",
            "ry": "0",
            "rz": "0",
            "rw": "0",
            "fov": 0
        },
        {
            "id": "img-yyyy-mm-dd hh-mm-ss",
            "px": "0",
            "py": "0",
            "pz": "0",
            "rx": "0",
            "ry": "0",
            "rz": "0",
            "rw": "0",
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

## Licensing

The code in this project is licensed under MIT license.
