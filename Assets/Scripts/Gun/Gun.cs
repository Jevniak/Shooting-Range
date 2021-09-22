using System;
using System.Collections;
using UnityEngine;

namespace Gun
{
    public class Gun : MonoBehaviour
    {
        [Serializable]
        protected class GunStatistic
        {
            // количество патронов до перезарядки
            public int amount;

            // время перезарядки
            public float reloadTime;

            // задержка между выстрелами
            public float shotCooldown;
        }

        protected Camera mainCamera;
        protected static Vector3 shotPosition;
        protected Transform thisTransform;

        protected virtual void Awake()
        {
            mainCamera = Camera.main;
            thisTransform = transform;
        }

        protected virtual void Shot()
        {
        }

        protected virtual IEnumerator Reload()
        {
            yield return null;
        }
    }
}