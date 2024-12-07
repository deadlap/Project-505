using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace PieHaptics
{
    public class AddedHaptics : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float intensity = 0.2f;
        [SerializeField, Min(0f), Tooltip("Arbitrary small value, should be longer than the average frame")] private float duration = 0.1f;

        [SerializeField] private bool isDanCourse = false;

        [SerializeField] private XRBaseController leftController;
        [SerializeField] private XRBaseController rightController;

        public void TriggerHaptic(bool leftHand)
        {
            if (!isDanCourse) return;

            // Get which controller we want to do haptic feedback to
            XRBaseController controllerToHaptic = leftHand ? leftController : rightController;

            controllerToHaptic.SendHapticImpulse(intensity, duration);
        }

        public void EndHaptic(bool leftHand)
        {
            if (!isDanCourse) return;

            // Get which controller we want to do haptic feedback to
            XRBaseController controllerToHaptic = leftHand ? leftController : rightController;

            controllerToHaptic.SendHapticImpulse(0f, duration);
        }
    }
}
