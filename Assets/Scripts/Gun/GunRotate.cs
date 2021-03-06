using UI;
using UnityEngine;

namespace Gun
{
    public class GunRotate : Gun
    {
        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            if (UIGameTimer.GameStarted)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 point = hit.point;
                    thisTransform.LookAt(point);
                    shotPosition = point;
                }
            }
        }
    }
}