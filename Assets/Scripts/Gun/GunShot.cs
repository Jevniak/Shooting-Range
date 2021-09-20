using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : Gun
{
    [SerializeField, Header("Префаб пули")] private GameObject bullet;

    [SerializeField] private GunStatistic _gunStatistic;

    private int amount;
    
    private bool reload;
    private bool shotCooldown;

    protected override void Awake()
    {
        base.Awake();
        amount = _gunStatistic.amount;
    }

    protected override void Shot()
    {
        if (amount == 0)
        {
            reload = true;
            StartCoroutine(Reload());
        }

        GameObject newBullet = Instantiate(bullet, thisTransform);
        newBullet.transform.SetParent(null);
        // TODO: Shot logic

    }

    protected override IEnumerator Reload()
    {
        yield return new WaitForSeconds(_gunStatistic.reloadTime);
        amount = _gunStatistic.amount;
        reload = false;
    }

    private IEnumerator ShotCooldown()
    {
        shotCooldown = true;
        yield return new WaitForSeconds(_gunStatistic.shotCooldown);
        shotCooldown = false;
    }

    private void Update()
    {
        print(shotPosition);
        if (!reload && !shotCooldown && Input.GetMouseButtonDown(0))
        {
            print("SHOOT!");
            Shot();
            StartCoroutine(ShotCooldown());
        }
    }
}
