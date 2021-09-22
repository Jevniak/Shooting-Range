using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Target
{
    public class TargetMover : Target
    {
        private Vector3 _startPosition;

        [SerializeField] private float speed = 0.2f;
        
        [Serializable]
        private class Boundaries
        {
            public float rightX;
            public float leftX;
        }
        [SerializeField] private Boundaries boundaries;

        [SerializeField]
        private bool moveToRight;

        protected override void Start()
        {
            base.Start();
            moveToRight = Random.Range(0, 2) == 0;
            _startPosition = ThisTransform.position;
            boundaries.rightX = _startPosition.x + Random.Range(0.2f,1f);
            boundaries.leftX = _startPosition.x - Random.Range(0.2f,1f);
        }


        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            // что-то мне подсказывает что это говнокод (как и скорее всего большая часть кода) но как это сделать лучше в голову не приходит 
            if (moveToRight && ThisTransform.position.x < boundaries.rightX)
            {
                ThisTransform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
                if (ThisTransform.position.x >= boundaries.rightX)
                {
                    moveToRight = false;
                }
            }
            else if (!moveToRight && ThisTransform.position.x > boundaries.leftX)
            {
                ThisTransform.Translate(Vector3.left * speed * Time.fixedDeltaTime);
                if (ThisTransform.position.x <= boundaries.leftX)
                {
                    moveToRight = true;
                }
            }
        }
    }
}
