using System.Collections;
using BoundaryTools;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BoundaryVisualizerTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void LerpByDistance_OneMeterDistance_PointAtTenPercent()
    {
        // Use the Assert class to test conditions
        BoundaryVisualizer boundaryVisualizer = new BoundaryVisualizer();
        Vector3 point = boundaryVisualizer.LerpByDistance(new Vector3(0, 0, 0), new Vector3(0, 0, 3), 0.1f);
        Assert.AreEqual(point, new Vector3(0, 0, 0.3f));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator BoundaryVisualizerTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}