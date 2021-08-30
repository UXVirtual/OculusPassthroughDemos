using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryVisualizer : MonoBehaviour
{
    public GameObject wallMarker;

    // Start is called before the first frame update
    void Start()
    {
        //Check if the boundary is configured
        bool configured = OVRManager.boundary.GetConfigured();

        if (configured)
        {
            //Grab all the boundary points. Setting BoundaryType to OuterBoundary is necessary
            Vector3[] playAreaPoints = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);

            //Generate a bunch of tall thin cubes to mark the outline
            foreach (Vector3 pos in playAreaPoints)
            {
                GameObject boundaryPoint = Instantiate(wallMarker, pos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
