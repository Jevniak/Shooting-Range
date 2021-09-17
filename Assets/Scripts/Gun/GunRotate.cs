using UnityEngine;

public class GunRotate : Gun
{
    private Camera mainCamera;
    private Transform thisTransform;

    [SerializeField] private float offset { get; }

    private void Awake()
    {
        mainCamera = Camera.main;
        thisTransform = transform;
    }


    private void Update()
    {
        // TODO: create rotate gun
    }



}
