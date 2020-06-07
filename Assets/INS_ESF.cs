using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INS_ESF : MonoBehaviour
{
    public static GameObject esfera;
    public GameObject esfera_original;
    public static List<GameObject> objetos = new List<GameObject>();
    int I;
    public Vector3 posicion_e1, posicion_e2;
    public float[]  vz, vx;
    float V1x, V1z, V1P, V1N, Vc1, Vc1x, Vc1z, V2x, V2z, V2P, V2N, Vc2, Vc2x, Vc2z, θ;
    float X, Z, xi, zi;
    float e = 0.9f, dx, dz, M1 = 1f, M2 = 1f;
    float Rad = 5f;
    // Start is called before the first frame update
    void Start()
    {
        I = 15;
        vz = new float[I];
        vx = new float[I];
  
        float R = 10f;
        float D = Mathf.Abs(Mathf.Sqrt(Mathf.Pow((2 * R), 2) - Mathf.Pow(R, 2)));
        for (int n = 5; n > 0; n--)
        {
            xi = -R * (n - 1);
            for (int i = 0; i < n; i++)
            {

                X = xi + (i * R * 2);
                Z = D * (n - 1);
                Vector3 position = new Vector3(X, 5, Z); //posición en triangulo 
                esfera = Instantiate(esfera_original, position, Quaternion.identity);//se instancia una partícula de aire.
                objetos.Add(esfera);//se añade la instancia generada anteriormente a la lista de instancias
                vx[n] = 0f;
                vz[n] = 0f;
            }

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int n = 0; n < I; n++)
        {

            posicion_e1 = objetos[n].gameObject.GetComponent<Transform>().position;
            V1x = vx[n];
            V1z = vz[n];


            if (Mathf.Abs(posicion_e1.x) >= 95)
            { V1x = -e * V1x; }
            if (Mathf.Abs(posicion_e1.z) >= 95)
            { V1z = -e * V1z; }


            vx[n] = V1x;
            vz[n] = V1z;
            posicion_e1.x += Time.fixedDeltaTime * V1x;
            posicion_e1.z += Time.fixedDeltaTime * V1z;
            objetos[n].gameObject.GetComponent<Transform>().position = posicion_e1;


            if (objetos[n].gameObject.tag == "activo")
            {
                for (int k = 0; k < I; k++)
                {
                     posicion_e2 = objetos[k].gameObject.GetComponent<Transform>().position;
                    if (Mathf.Abs((posicion_e1 - posicion_e2).magnitude) <= 2f * Rad && k != n)
                    {
                       
                        V2x = vx[k];
                        V2z = vz[k];
                        dx = posicion_e1.x - posicion_e2.x; dz = posicion_e1.z - posicion_e2.z;
                        θ = Mathf.Atan(dz/dx);

                        V1P = V1x * Mathf.Cos(θ) + V1z * Mathf.Sin(θ); V2P = V2x * Mathf.Cos(θ) + V2z * Mathf.Sin(θ);
                        V1N = -V1x * Mathf.Sin(θ) + V1z * Mathf.Cos(θ); V2N = -V2x * Mathf.Sin(θ) + V2z * Mathf.Cos(θ);


                        Vc1 = ((M1 - e * M2) / (M1 + M2)) * V1P + (((1 + e) * M2) / (M1 + M2)) * V2P;

                        Vc1x = (Vc1 * Mathf.Cos(θ)) - (V1N * Mathf.Sin(θ));
                        Vc1z = (Vc1 * Mathf.Sin(θ)) - (V1N * Mathf.Cos(θ));



                        Vc2 = (((1 + e) * M1) / (M1 + M2)) * V1P + ((M2 - (e * M1)) / (M1 + M2)) * V2P;

                        Vc2x = (Vc2 * Mathf.Cos(θ)) - (V2N * Mathf.Sin(θ));
                        Vc2z = (Vc2 * Mathf.Sin(θ)) - (V2N * Mathf.Cos(θ));

                        V1x = Vc1x; V1z = Vc1z;
                        vx[n] = Vc1x; vz[n] = Vc1z;
                        vx[k] = Vc2x; vz[k] = Vc2z;
                        objetos[k].gameObject.tag = "activo";
                    }
                    
                }

             

            }
         

        }

    }
}

