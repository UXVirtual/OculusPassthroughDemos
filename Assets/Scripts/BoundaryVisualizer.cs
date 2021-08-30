using UnityEngine;

public class BoundaryVisualizer : MonoBehaviour
{
    public GameObject wallMarker;

    // Start is called before the first frame update
    private void Start()
    {
        //Check if the boundary is configured
        var configured = OVRManager.boundary.GetConfigured();

        if (configured)
        {
            //Grab all the boundary points. Setting BoundaryType to OuterBoundary is necessary
            var playAreaPoints = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);

            //Generate a bunch of tall thin cubes to mark the outline
            foreach (var pos in playAreaPoints)
            {
                var boundaryPoint = Instantiate(wallMarker, pos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}