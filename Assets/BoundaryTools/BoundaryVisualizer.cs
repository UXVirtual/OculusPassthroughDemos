using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

namespace BoundaryTools
{
    public class BoundaryVisualizer : MonoBehaviour
    {
        public GameObject wallMarker;

        // Start is called before the first frame update
        private void Start()
        {
            //Check if the boundary is configured
            bool configured = OVRManager.boundary.GetConfigured();
            List<Vector3> playAreaPoints;

            if (configured)
            {
                //Grab all the boundary points. Setting BoundaryType to OuterBoundary is necessary
                playAreaPoints = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea).ToList();

                //Generate a bunch of tall thin cubes to mark the outline
                foreach (Vector3 pos in playAreaPoints)
                {
                    GameObject boundaryPoint = Instantiate(wallMarker, pos, Quaternion.identity);
                }
            }
            else
            {
                Debug.LogWarning(
                    "Oculus boundary not configured. You may be running in Link mode which is not supported.");
                // TODO: use placeholder boundary data instead
                playAreaPoints = GeneratePlayAreaPlaceholder();
                Debug.Log(playAreaPoints);
            }
        }

        public List<Vector3> GeneratePlayAreaPlaceholder()
        {
            List<Vector3> playArea = new List<Vector3>();

            Vector3 point1 = new Vector3(0, 0, 0);
            Vector3 point2 = new Vector3(0, 0, 3);
            Vector3 point3 = new Vector3(3, 0, 3);
            Vector3 point4 = new Vector3(3, 0, 0);

            int lineLength = 3;
            int segments = 10;

            playArea.AddRange(GenerateLinePoints(point1, point2, lineLength, segments));
            playArea.AddRange(GenerateLinePoints(point2, point3, lineLength, segments));
            playArea.AddRange(GenerateLinePoints(point3, point4, lineLength, segments));
            playArea.AddRange(GenerateLinePoints(point4, point1, lineLength, segments));

            return playArea;
        }

        public List<Vector3> GenerateLinePoints(Vector3 a, Vector3 b, int lineLength, int segments)
        {
            List<Vector3> points = new List<Vector3>();

            for (int i = 0; i < segments; i++)
            {
                float position = lineLength / segments * (i + 1);
                points.Add(LerpByDistance(a, b, position));
            }

            return points;
        }

        public Vector3 LerpByDistance(Vector3 a, Vector3 b, float x)
        {
            Vector3 point = x * Vector3.Normalize(b - a) + a;
            return point;
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}