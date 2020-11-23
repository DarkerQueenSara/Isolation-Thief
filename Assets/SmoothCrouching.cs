using UnityEngine;
using System.Collections;

namespace Assets
{
    public class SmoothCrouching
    {

        public float crouchedHeightFactor = 0.65f;

        private CharacterController playerController;
        private Collider playerCollider;
        private float initialHeight;
        private float crouchedHeight;
        private bool isCrouching;

        public SmoothCrouching(CharacterController playerController, Collider playerCollider)
        {
            this.playerController = playerController;
            this.playerCollider = playerCollider;
            isCrouching = false;
            initialHeight = playerCollider.transform.localScale.y;
            crouchedHeight = initialHeight * crouchedHeightFactor;
        }

        public void setCrouching(bool isCrouching)
        {
            this.isCrouching = isCrouching;
        }

        private bool equalFloats(float a, float b)
        {
            return Mathf.Abs(a - b) < 0.0005;
        }

        //precisa de ser chamado a cada frame
        public void Update()
        {
            Vector3 playerScale = playerCollider.transform.localScale;
            Vector3 playerPos = playerCollider.transform.localPosition;
            float oldHeight = playerScale.y;
            if (isCrouching && !equalFloats(playerScale.y, crouchedHeight))
            {
                Vector3 crouchedScale = new Vector3(playerScale.x, crouchedHeight, playerScale.z);
                playerCollider.transform.localScale = Vector3.Lerp(playerScale, crouchedScale, Time.deltaTime * 5f);
                float deltaHeight = oldHeight - playerCollider.transform.localScale.y;
                playerController.Move(new Vector3(0, -deltaHeight, 0));
            }
            else if (!isCrouching && !equalFloats(playerScale.y, initialHeight))
            {
                Vector3 initialScale = new Vector3(playerScale.x, initialHeight, playerScale.z);
                playerCollider.transform.localScale = Vector3.Lerp(playerScale, initialScale, Time.deltaTime * 5f);
                float deltaHeight = oldHeight - playerCollider.transform.localScale.y;
                playerController.Move(new Vector3(0, -deltaHeight, 0));
            }
        }
    }
}