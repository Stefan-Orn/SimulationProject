// 28 Jan, 2025
using UnityEngine;

public class physics : MonoBehaviour
{
    public Vector3 initialVel = new Vector3(1.0f, 0.0f, 0.0f);
    //public GameObject cube;
    //public GameObject ground;

    public float mass1 = 1.0f;
    public float mass2 = 10.0f;
    public float rotationSpeed = 10.0f;
    public float orbitalVel = 10.0f;

    private Vector3 cubePosition;
    private Vector3 groundPosition;
    private Vector3 accel1;
    private Vector3 vel1;
    private Vector3 accel2;
    private Vector3 vel2;
    private float E;
    private GameObject[] sunObj;
    private GameObject[] planetObj;
    private float[] masses = {9e10f,1.0f};
    private bool debugupdater_disabled = false;
    
    public Vector3 force(GameObject object1, GameObject object2, float mass1, float mass2)
    {
        Vector3 obj1pos = object1.transform.position;
        Vector3 obj2pos = object2.transform.position;
            // Nú vill ég reyna fá öll gildin fyrir universal gravitation
        float distance = myDistance(obj1pos, obj2pos);
        Debug.Log("Distance a milli " + object1.name + " og " + object2.name + " er: " + distance);

        float distanceSquared = distance*distance;

        float G = 6.67f*Mathf.Pow(10,-11); // Þetta er bara fasti, gravitational constant

        float force = G*((mass1*mass2)/distanceSquared); // 
            // Til þess að breyta þessu í vektor þarf ég að gera eftirfarandi
        Vector3 forceVector = force*(obj1pos-obj2pos);

        return forceVector;
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
        planetObj = GameObject.FindGameObjectsWithTag("plantes");
        sunObj = GameObject.FindGameObjectsWithTag("sun");
        foreach(GameObject obj1 in planetObj)
        {
            Debug.Log(obj1);
        }
        foreach(GameObject obj2 in sunObj)
        {
            Debug.Log(obj2);
        }

    }

    void everyOneFeels()
    {
        for (int i = 0; i < sunObj.Length; i++)
        {
            Debug.Log("Skoða frá: " + sunObj[i]);
            //Með 1 obj
            for (int j = 0; j < planetObj.Length; j++)
            {
                //Eitt obj vs all
                if(i == j){
                    Debug.Log("Skoða ekki mig!");
                }
                else
                {
                    Debug.Log(sunObj[i] + " vs " +planetObj[j]);
                    Vector3 forceid = force(sunObj[i],planetObj[j],masses[i],masses[j]);
                    accel1 = forceid*1/masses[i];
                    vel1 += accel1 * Time.deltaTime;
                    sunObj[i].transform.position += vel1*Time.deltaTime;

                    accel2 = forceid*1/masses[j];
                    vel2 += accel2 * Time.deltaTime;
                    planetObj[j].transform.position += vel2*Time.deltaTime;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Þetta hérna er nánast fullkomið, þarf ekki meira, allt hegðar ser
        // eins og það á að gera
        // accel1 = force()*1/cubeMass;
        // vel1 += accel1 * Time.deltaTime;
        // cube.transform.position += vel1*Time.deltaTime;
        // E += 0.5f*cubeMass*Mathf.Pow(vel1.x,2);

        // accel2 = force()*1/groundMass;
        // vel2 += accel2 * Time.deltaTime;
        // ground.transform.position += vel2*Time.deltaTime;
        
        everyOneFeels();
    }
}
