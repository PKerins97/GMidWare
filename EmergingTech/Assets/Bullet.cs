using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;


    private void OnCollisionEnter(Collision collision)
    {
        
        Destroy(gameObject);
    }


}