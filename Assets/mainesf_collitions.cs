using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainesf_collitions : MonoBehaviour
{
    private Vector3 PosEsf1;
    private GameObject Sphere;
    float Lado = 100f;
    float Rad = 5f;
    float V1x, V1z, V1P, V1N, Vc1, Vc1x, Vc1z , V2x, V2z, V2P, V2N, Vc2, Vc2x, Vc2z, θ;
    public MovEsf esfera1;
    public INS_ESF esfera2;
    public List<GameObject> objs = INS_ESF.objetos; // trae la lista objetos del codigo INS_ESF, se puede directamente por ser  public static 
    public GameObject esf = INS_ESF.esfera;
    float e = 1, dx, dz, M1=1.5f,M2=1f;
    public int num = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        V1x = esfera1.V1x;
        V1z = esfera1.V1z;
        PosEsf1 = gameObject.GetComponent<Transform>().position;
        if (Mathf.Abs(PosEsf1.x) >= Lado - Rad)
        { V1x = -e * V1x; esfera1.V1x = V1x; }
        if (Mathf.Abs(PosEsf1.z) >= Lado - Rad)
        { V1z = -e * V1z; esfera1.V1z = V1z; }

        foreach (GameObject esf in objs)
        {
            Vector3 posicion_oesf = esf.gameObject.GetComponent<Transform>().position;
            if (Mathf.Abs((PosEsf1 - posicion_oesf).magnitude) <= 2*Rad) //capa de verificacion de colision 1
            {
              
                V2x = esfera2.vx[num]; V2z = esfera2.vz[num];
                dx = PosEsf1.x - posicion_oesf.x; dz = PosEsf1.z - posicion_oesf.z;
                θ = Mathf.Atan(dz / dx);
                V1P = V1x * Mathf.Cos(θ) + V1z * Mathf.Sin(θ);  V2P = V2x * Mathf.Cos(θ) + V2z * Mathf.Sin(θ);
                V1N = -V1x * Mathf.Sin(θ) + V1z * Mathf.Cos(θ); V2N = -V2x * Mathf.Sin(θ) + V2z * Mathf.Cos(θ);


                Vc1 = ((M1 - e * M2) / (M1 + M2)) * V1P + (((1 + e) * M2) / (M1 + M2)) * V2P;

                Vc1x = (Vc1 * Mathf.Cos(θ)) - (V1N * Mathf.Sin(θ));
                Vc1z = (Vc1 * Mathf.Sin(θ)) - (V1N * Mathf.Cos(θ));



                Vc2 = (((1 + e) * M1) / (M1 + M2)) * V1P + ((M2 - (e * M1)) / (M1 + M2)) * V2P;

                Vc2x = (Vc2 * Mathf.Cos(θ)) - (V2N * Mathf.Sin(θ));
                Vc2z = (Vc2 * Mathf.Sin(θ)) - (V2N * Mathf.Cos(θ));


                V1x = Vc1x;   V1z = Vc1z;
                esfera1.V1x = Vc1x; esfera1.V1z = Vc1z;
                esfera2.vx[num] = Vc2x; esfera2.vz[num] = Vc2z;
                esf.gameObject.tag = "activo";
            }
            num++;
        }

        num = 0;


          PosEsf1.x = PosEsf1.x + Time.fixedDeltaTime * V1x;
        PosEsf1.z = PosEsf1.z + Time.fixedDeltaTime * V1z;
        gameObject.GetComponent<Transform>().position = PosEsf1;
    }
}
