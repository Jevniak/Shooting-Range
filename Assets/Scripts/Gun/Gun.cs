using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    protected Transform thisTransform;
    protected GameObject prefabBullet;    

    protected virtual void Awake()
    {
        thisTransform = transform;
    }

    protected virtual void Shot()
    {

    }

    protected virtual void Reload()
    {

    }
}
