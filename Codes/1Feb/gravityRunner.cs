using UnityEngine;
using System.Collections.Generic;

public class gravityRunner : MonoBehaviour
{
    // Hér eru hlutir sem hægt er að breyta snögglega í Unity forritinu
    public Vector3 initialVelocity = new Vector3(10.0f, 0.0f, 0.0f);
    public GameObject orbiter;
    public int[] masses = {100,200000};
    public float minDistance = 0.01f;
    // - - - - 
    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();

    // Hér get ég stillt hvernig línan lítur út. Litirnir virðast samt ekki vera að virka
    private void LineSettings(LineRenderer lineRenderer)
    {
        lineRenderer.startWidth = 1.0f;
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
    }

    void Start()
    {
        // Þarf hér að festa svona Line Renderer component á alla hlutina í scenuni
        // sem eru simObjects
        GameObject[] simObjects = GameObject.FindGameObjectsWithTag("simObjects");
        foreach(GameObject simObj in simObjects)
        {
            lineRenderer = simObj.AddComponent<LineRenderer>();
            LineSettings(lineRenderer);
        }
    }

    void Update()
    {
        GameObject[] simObjects = GameObject.FindGameObjectsWithTag("simObjects");
        // Hér fer siðan allt draslið yfir, upphafsskilyrði og Line Renderer
        betterGravity.ApplyGravity(simObjects, initialVelocity, masses, lineRenderer, minDistance);
        initialVelocity = initialVelocity*0; // Núlla svo ég sé ekki alltaf að bæta við

    }
}
