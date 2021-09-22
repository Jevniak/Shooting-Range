using UnityEngine;

namespace Target
{
    public class Target : MonoBehaviour
    {
        protected Transform ThisTransform;
        protected static int CountActiveTargets;
        protected static int CountActiveBadTargets;
        
        protected virtual void Start()
        {
            ThisTransform = transform;
        }

        protected void DisableTarget(Transform target, string valueTag)
        {
            target.GetComponent<TargetDisableTimer>().enabled = false;
            target.rotation = Quaternion.Euler(180, 0, 0);
            target.tag = "Untagged";
            if (valueTag == "God")
                CountActiveTargets--;
            else
                CountActiveBadTargets--;
        }
        
        protected void EnableTarget(Transform target, string valueTag)
        {
            
            target.rotation = Quaternion.Euler(90, 0, 0);
            target.tag = valueTag;
            target.GetComponent<TargetDisableTimer>().enabled = true;
        }
    }
}
