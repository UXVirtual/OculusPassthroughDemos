﻿using DilmerGames.Core.Singletons;
using UnityEngine;

public class SurfaceProjectionManager : Singleton<SurfaceProjectionManager>
{
    [SerializeField]
    private GameObject passthroughLayerUserDefinedPrefab;

    private GameObject passthroughLayerUserDefinedGameObject;

    [SerializeField]
    private GameObject passthroughLayerReconstructuredPrefab;

    private GameObject passthroughLayerReconstructuredGameObject;

    private OVRPassthroughLayer passthroughLayerUserDefined;

    private OVRPassthroughLayer passthroughLayerReconstructed;

    [SerializeField]
    private Transform tilesRoot;

    public OVRPassthroughLayer PassthroughLayer => passthroughLayerUserDefined;

    public Transform TilesRoot => tilesRoot;

    // default is reconstructed
    private bool reconstructedPassthrough = false;

    private SurfacePassthroughTile[] surfacePassthroughTiles;

    private void Awake()
    {
        surfacePassthroughTiles = tilesRoot.GetComponentsInChildren<SurfacePassthroughTile>();
        ToggleSurfaceType(randomizeTiles: false);
    }

    public void ProjectionToTilesAction(bool addProjection = true, bool randomizeTiles = false)
    {
        foreach (var tile in surfacePassthroughTiles)
        {
            if (addProjection)
                tile.AddProjection(randomizeTiles);
            else
                tile.RemoveProjection();
        }
    }

    public void Update()
    {
        // A Button
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            ToggleSurfaceType(randomizeTiles: true);
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            ToggleSurfaceType(randomizeTiles: false);
        }
    }

    private void ToggleSurfaceType(bool randomizeTiles)
    {
        Logger.Instance.LogInfo("Toggling passthrough projection surface type");

        reconstructedPassthrough = !reconstructedPassthrough;

        if (reconstructedPassthrough)
        {
            Logger.Instance.LogInfo("Toggle passthrough set to reconstructed");

            EnableProjectionSurfaceType(OVRPassthroughLayer.ProjectionSurfaceType.Reconstructed);
        }
        else
        {
            Logger.Instance.LogInfo("Toggle passthrough set to user defined");

            EnableProjectionSurfaceType(OVRPassthroughLayer.ProjectionSurfaceType.UserDefined);

            ProjectionToTilesAction(addProjection: true, randomizeTiles);
        }
    }

    private void EnableProjectionSurfaceType(OVRPassthroughLayer.ProjectionSurfaceType projectionSurfaceType 
        = OVRPassthroughLayer.ProjectionSurfaceType.Reconstructed)
    {
        if (passthroughLayerReconstructuredGameObject != null)
            DestroyImmediate(passthroughLayerReconstructuredGameObject);
        if (passthroughLayerUserDefinedGameObject != null)
            DestroyImmediate(passthroughLayerUserDefinedGameObject);

        if (projectionSurfaceType == OVRPassthroughLayer.ProjectionSurfaceType.Reconstructed) 
        {
            passthroughLayerReconstructuredGameObject = Instantiate(passthroughLayerReconstructuredPrefab);
            passthroughLayerReconstructed = passthroughLayerReconstructuredGameObject.GetComponent<OVRPassthroughLayer>();
        }
        else
        {
            passthroughLayerUserDefinedGameObject = Instantiate(passthroughLayerUserDefinedPrefab);
            passthroughLayerUserDefined = passthroughLayerUserDefinedGameObject.GetComponent<OVRPassthroughLayer>();
        }
    }
}
