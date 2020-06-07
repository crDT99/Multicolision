using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovEsf : MonoBehaviour
{
    private Vector3 PosEsf1;
    private GameObject Sphere;
    float LimU = 100f;
    float LimD = -100f;
    float LimR = 100f;
    float LimL = -100f;
    float Rad = 5f;
    public float V1x, V1z;

    void Start()
    {

        V1x = 0; V1z = 0;
    }
    void FixedUpdate()
    {
        PosEsf1 = gameObject.GetComponent<Transform>().position;

        if (Input.GetKey(KeyCode.UpArrow) && (PosEsf1.z + Rad) <= LimU)
        {
            
                if (V1z < 100f)
                {
               
                    V1z += 1f;
                }
                else
                {
                    V1z= 100f;
                }
        }else
        if (Input.GetKey(KeyCode.DownArrow) && (PosEsf1.z - Rad) >= LimD)
        {

            if (V1z > -100f)
            {
          
                V1z -= 1f;
            }
            else
            {
                V1z = -100f;
            }
        }else
        if (Mathf.Abs(V1z) > 0)
        {
            if (V1z > 0)
            {
                V1z -= 1f;
            }
            else
            {
                if (V1z < 0)
                {
                    V1z += 1f;
                }
                else
                {
                    V1z = 0;
                }
            }
        }




        if (Input.GetKey(KeyCode.RightArrow) && (PosEsf1.x + Rad) <= LimR)
        {

            if (V1x < 100f)
            {

                V1x += 1f;
            }
            else
            {
                V1x = 100f;
            }
        }
        else
      if (Input.GetKey(KeyCode.LeftArrow) && (PosEsf1.x - Rad) >= LimL)
        {

            if (V1x > -100f)
            {

                V1x -= 1f;
            }
            else
            {
                V1x = -100f;
            }
        }
        else
      if (Mathf.Abs(V1x) > 0)
        {
            if (V1x > 0)
            {
                V1x -= 1f;
            }
            else
            {
                if (V1x < 0)
                {
                    V1x += 1f;
                }
                else
                {
                    V1x = 0;
                }
            }
        }

    }
}
