# XRDataCollection {ignore=true}
Tools to collect sensory data from XR devices in the Unity engine


<!-- @import "[TOC]" {cmd="toc" depthFrom=1 depthTo=6 orderedList=false} -->

<!-- code_chunk_output -->

- [2D Data Collection](#2d-data-collection)
- [3D Data Collection](#3d-data-collection)
- [Saving Data](#saving-data)

<!-- /code_chunk_output -->


# 2D Data Collection

Images can be saved, both from a virtual camera of a physical camera using an AR platform.
If the AR platform tracks its transform in the scene, the transformdata will also be saved in the session folder

# 3D Data Collection

A mesh can be saved in the same session folder, 

# Saving Data
all the data can be saved to a single folder per session, managed by the ``AssetSessionManager``
a central Json file stores all the references to the images and meshes:

```
{
    "sessionId": "session-yyyy-mm-dd hh-mm-ss",
    "jsonid": "SessionData.json",
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
        },
        ...
    ],
    "meshIds": [
        "mesh-yyyy-mm-dd hh-mm-ss",
        "mesh-yyyy-mm-dd hh-mm-ss",
        ...
    ]
}
```
