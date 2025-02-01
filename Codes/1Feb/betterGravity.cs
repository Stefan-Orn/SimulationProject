using UnityEngine;
using System.Collections.Generic;


public static class betterGravity
{
    private static float G =  0.01f; //6.6743f*Mathf.Pow(10,-11); // real shit
    //private static int[] masses = {100,200000};
    private static Vector3 velocityA;
    private static Vector3 velocityB;
    private static Dictionary<Transform, List<Vector3>> movements = new Dictionary<Transform, List<Vector3>>();

    public static void DrawLine(Transform objTransform, LineRenderer lineRenderer, float minDistance)
    {

        // Tók þennan kóða af netinu en hann er til þess að teikna línu á eftir hlut
        // nota víst ehv. Unity library fyrir það(Line renderer)
        // Væri gaman að reyna að gera það frá grunni

        // Tékk hvort hlutur sé til í dictinu núþegar. Ef ekki, bæti ég honum inn með
        // viðeigandi staðsetningar vector
        if(!movements.ContainsKey(objTransform))
        {
            movements[objTransform] = new List<Vector3>();
        }
        // Hér uppfæri ég staðsetningar vector fyrir hluti sem eru núþegar til í dictinu
        List<Vector3> points = movements[objTransform];

        // Hér tékka ég hvort hlutur hefur færst ehv. ákveðna vegalend sem ég skilgreini
        // með minDistance. Veit ekki alveg afhverju the point.Count == 0 er en ef
        // ég tek það í burtu kemur ehv. indexing error svo þetta er líklegast til
        // að hunsa ehv. upphafsskilyrði
        if(points.Count == 0 || Vector3.Distance(points[points.Count - 1], objTransform.position) > minDistance)
        {
            // Hér er bara kóði sem notfærir sér þetta library lineRenderer
            points.Add(objTransform.position);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
    }

    public static void ApplyGravity(GameObject[] simObjects, Vector3 initialVelocity, int[] masses, LineRenderer lineRenderer, float minDistance)
    {
        for(int i = 0; i < simObjects.Length; i++)
        {
            for(int j = i + 1; j < simObjects.Length; j++)
            {
                GameObject objA = simObjects[i];
                GameObject objB = simObjects[j];

                // Finn áttun á kraftinum
                Vector3 distance = objB.transform.position - objA.transform.position;
                float Rsquared = Mathf.Pow(calculations.vectorNorm(distance),2);
                
                // Byrja á að hraða hlut A en, þar sem að objA er dregið "FRÁ" objB er áttun
                // distance vectorsins frá "objA til objB"
                //Debug.Log("Force: " + G*(masses[i]*masses[j])/Rsquared);
                Vector3 accelerationA = calculations.normalizeVector(distance)*(G*masses[i]/Rsquared);
                velocityA += accelerationA*Time.deltaTime;
                objA.transform.position += (velocityA)*Time.deltaTime;
                DrawLine(objA.transform, objA.GetComponent<LineRenderer>(), minDistance);

                // Hér er ég að gera basically það sama nema að hef áttina í "-" við
                // það sem var reiknað
                Vector3 accelerationB = -(calculations.normalizeVector(distance)*(G*masses[j]/Rsquared));
                velocityB += initialVelocity + accelerationB*Time.deltaTime;
                objB.transform.position += (velocityB)*Time.deltaTime;
                DrawLine(objB.transform, objB.GetComponent<LineRenderer>(), minDistance);
            }

            
        }
    }
}
