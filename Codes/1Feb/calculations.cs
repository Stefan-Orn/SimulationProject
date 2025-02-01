using UnityEngine;

// Hér held ég utan um allar reikniaðgerðir sem ég geri frá grunni
public static class calculations
{
    // Gott að normalize'a vector til að geta fengið "unit-vector" áttar (vector m. lengd 1)
    public static Vector3 normalizeVector(Vector3 distanceVector)
    {
        return distanceVector/vectorNorm(distanceVector);
    }
    
    // Norm er bara lengd vectors (pythagoras á allt) sem er hentugt í margt
    public static float vectorNorm(Vector3 distanceVector)
    {
        return Mathf.Sqrt(vectorDot(distanceVector));
    }

    // Dot er bara summa hverst staks vectora margfaldað við hvort annað. Hér er ég samt
    // með sértilvik þar sem ég er að margfalda vector með sjálfum sér, svo, þarf bara
    // eitt input
    public static float vectorDot(Vector3 distanceVector)
    {
        return distanceVector.x * distanceVector.x + distanceVector.y * distanceVector.y + distanceVector.z * distanceVector.z;
    }
}
