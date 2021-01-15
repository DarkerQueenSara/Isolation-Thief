using UnityEngine;
using System.Collections;

namespace Assets
{
    public class SmoothProning
    {

        public float pronedHeightFactor = 0.3f;
        public float interpolationTime = 0.75f;

        private CharacterController playerController;
        private Collider playerCollider;
        private float initialHeight;
        private float pronedHeight;
        private bool isProning;

        private int interpolationFrames;
        private int currentCountDown;

        public SmoothProning(CharacterController playerController, Collider playerCollider)
        {
            this.playerController = playerController;
            this.playerCollider = playerCollider;
            isProning = false;
            initialHeight = playerCollider.transform.localScale.y;
            pronedHeight = initialHeight * pronedHeightFactor;

            interpolationFrames = (int) (1 / Time.deltaTime * interpolationTime);
            currentCountDown = interpolationFrames;
        }

        public void setProning(bool isProning)
        {
            if(currentCountDown <= interpolationFrames)
            {
                currentCountDown = interpolationFrames - currentCountDown;
            }
            else
            {
                currentCountDown = 0;
            }
            this.isProning = isProning;
        }

        public bool isPlayerProning()
        {
            return isProning;
        }

        private bool equalFloats(float a, float b)
        {
            return Mathf.Abs(a - b) < 0.005;
        }

        //precisa de ser chamado a cada frame
        public void Update()
        {
            if (currentCountDown <= interpolationFrames)
            {
                Vector3 playerScale = playerCollider.transform.localScale;
                Vector3 crouchedScale = new Vector3(playerScale.x, pronedHeight, playerScale.z);
                Vector3 initialScale = new Vector3(playerScale.x, initialHeight, playerScale.z);
                float interpolationRatio = (float)currentCountDown / interpolationFrames;
                if (isProning)
                {
                    playerCollider.transform.localScale = Vector3.Lerp(initialScale, crouchedScale, interpolationRatio);

                    #region hardcoded stuff
                    if (interpolationFrames - 4 == currentCountDown)
                    {
                        playerController.Move(new Vector3(0, 0.025f, 0));
                    }
                    if (interpolationFrames == currentCountDown) {
                        playerController.Move(new Vector3(0, 0.025f, 0));
                    }
                    #endregion
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