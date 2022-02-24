using System;
using System.Collections;
using UnityEngine;

namespace Majingari.Game.ActorComponent {
    public class Locomotion {
        private Transform myTransform;
        public event Action OnMoveComplete;
        private float speed;

        public Locomotion(Transform transform, float speedValue) {
            myTransform = transform;
            speed = speedValue;
        }

        /// <summary>
        /// Using Courotine to move down the tile, so we can control the update. and also for future use with job system
        /// </summary>
        /// <param name="targetYPosition"></param>
        /// <returns></returns>
        public IEnumerator UpdateLerp(float targetYPosition) {
            var waitEndOfFrame = new WaitForEndOfFrame();

            var targetPosition = new Vector3(myTransform.localPosition.x, targetYPosition, myTransform.localPosition.z);
            var journeyLength = Vector3.Distance(myTransform.position, targetPosition);
            var startTime = Time.time;

            while (myTransform.position != targetPosition) {
                var distCovered = Time.time - startTime;
                var fractionOfJourney = distCovered / journeyLength;

                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, fractionOfJourney * speed);
                yield return waitEndOfFrame;
            }

            OnMoveComplete?.Invoke();
        }
    }
}
