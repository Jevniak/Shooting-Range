using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class BulletFly : MonoBehaviour
    {
        private Transform thisTransform;
        private Rigidbody rb;
        
        private void Awake()
        {
            thisTransform = transform;
            rb = GetComponent<Rigidbody>();
            rb.AddForce(thisTransform.up * 20, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }
    }
}