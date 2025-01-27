# 27 Jan, 2025
using UnityEngine;

public class physics : MonoBehaviour
{
    public Vector3 initialVel = new Vector3(1.0f, 0.0f, 0.0f);
    public GameObject cube;
    public GameObject ground;

    public float cubeMass = 1.0f;
    public float groundMass = 10000000.0f;
    public float rotationSpeed = 10.0f;
    public float orbitalVel = 10.0f;

    private Vector3 cubePosition;
    private Vector3 groundPosition;
    private Vector3 accel1;
    private Vector3 vel1;
    private Vector3 accel2;
    private Vector3 vel2;
    private float E;


    
    public Vector3 force()
    {
        if(ground == null){
            return Vector3.zero;
        }
        else{

            cubePosition = cube.transform.position;
            groundPosition = ground.transform.position;
            // Nú vill ég reyna fá öll gildin fyrir universal gravitation
            float distance = myDistance(cubePosition, groundPosition);

            float distanceSquared = distance*distance;

            float G = 6.67f*Mathf.Pow(10,-11); // Þetta er bara fasti, gravitational constant

            float force = G*(cubeMass*groundMass)/distanceSquared; // kg * m/s^2
            // Til þess að breyta þessu í vektor þarf ég að gera eftirfarandi
            Vector3 forceVector = force*(groundPosition-cubePosition);

            return forceVector;
        }
    }


    // Hérna nota ég dot product og norm til að reikna fjarlægð.
    // Fæ nákvæmelega það sama og úr Unity Distance fallinu
    public float dot(Vector3 x, Vector3 y)
    {
        return x.x * y.x  +  x.y * y.y  +  x.z * y.z;
    }

    // Hérna er ég að reikna fjarlægðina með norm. Nota dot product til að reikna dot product
    // vektorsins með sjálfum sér
    public float myDistance(Vector3 x, Vector3 y)
    {
        return Mathf.Sqrt(dot(x,x) - 2*dot(x,y) + dot(y,y));
    }

    // Tók út hraða og hröðunar draslið það sem ég á ekki að þurfa að reikna það til þess
    // að simmið virki

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Þetta hérna er nánast fullkomið, þarf ekki meira, allt hegðar ser
        // eins og það á að gera
        accel1 = force()*1/cubeMass;
        vel1 += accel1 * Time.deltaTime;
        cube.transform.position += vel1*Time.deltaTime;
        E += 0.5f*cubeMass*Mathf.Pow(vel1.x,2);

        accel2 = force()*1/groundMass;
        vel2 += accel2 * Time.deltaTime;
        ground.transform.position += vel2*Time.deltaTime;
        
    }
}
