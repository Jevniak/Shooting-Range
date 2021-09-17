using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : Gun
{
    protected override void Shot()
    {
        base.Shot();
        // TODO: Shot logic
    }

    protected override void Reload()
    {
        base.Reload();
        // TODO: reload logic
    }

    private IEnumerator WaitingReload()
    {
        // TOD: waiting reload coroutine
        yield return null;
    }
}
