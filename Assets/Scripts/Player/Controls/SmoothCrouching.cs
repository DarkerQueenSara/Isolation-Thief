using UnityEngine;
using System.Collections;

namespace Assets
{
    public class SmoothCrouching
    {

        public float crouchedHeightFactor = 0.75f;
        public float interpolationTime = 0.75f;

        private CharacterController playerController;
        private Collider playerCollider;
        private float initialHeight;
        private float crouchedHeight;
        private bool isCrouching;

        private int interpolationFrames;
        private int currentCountDown;

        public SmoothCrouching(CharacterController playerController, Collider playerCollider)
        {
            this.playerController = playerController;
            this.playerCollider = playerCollider;
            isCrouching = false;
            initialHeight = playerCollider.transform.localScale.y;
            crouchedHeight = initialHeight * crouchedHeightFactor;

            interpolationFrames = (int) (1 / Time.deltaTime * interpolationTime);
            currentCountDown = interpolationFrames;
        }

        public void setCrouching(bool isCrouching)
        {
            if(currentCountDown <= interpolationFrames)
            {
                currentCountDown = interpolationFrames - currentCountDown;
            }
            else
            {
                currentCountDown = 0;
            }
            this.isCrouching = isCrouching;
        }

        public bool isPlayerCrouching()
        {
            return isCrouching;
        }

        private bool equalFloats(float a, float b)
        {
            return Mathf.Abs(a - b) < 0.005;
        }

        //precisa de ser chamado a cada frame
        public void Update()
        {
            if(currentCountDown <= interpolationFrames)
            {
                Vector3 playerScale = playerCollider.transform.localScale;
                Vector3 crouchedScale = new Vector3(playerScale.x, crouchedHeight, playerScale.z);
                Vector3 initialScale = new Vector3(playerScale.x, initialHeight, playerScale.z);
                float interpolationRatio = (float)currentCountDown / interpolationFrames;
                if (isCrouching)
                {
                    playerCollider.transform.localScale = Vector3.Lerp(initialScale, crouchedScale, interpolationRatio);
                }
                else
                {
                    playerCollider.transform.localScale = Vector3.Lerp(crouchedScale, initialScale, interpolationRatio);
                }
                currentCountDown++;
            }
        }
    }
}