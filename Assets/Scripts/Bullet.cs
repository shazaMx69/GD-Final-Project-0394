using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTranform = collision.transform;
        if(hitTranform.CompareTag("Player"))
        {
            Debug.Log("Hit Player.");
            hitTranform.GetComponent<PlayerHealth>().TakeDamage(10);
        }
        Destroy(gameObject);
    }
}
