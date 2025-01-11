using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ScriptGun
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class GrabInspector : MonoBehaviour
    {
        private XRGrabInteractable grabInteractable;
        private List<XRBaseController> interactors = new List<XRBaseController>();

        private void Awake()
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
            if (grabInteractable == null)
            {
                Debug.LogError("XRGrabInteractable component is missing on this GameObject.");
            }
        }

        private void OnEnable()
        {
            if (grabInteractable == null) return;

            grabInteractable.selectEntered.AddListener(OnSelectEntered);
            grabInteractable.selectExited.AddListener(OnSelectExited);
        }

        private void OnDisable()
        {
            if (grabInteractable == null) return;

            grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
            grabInteractable.selectExited.RemoveListener(OnSelectExited);
        }

        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            var controller = args.interactorObject.transform.GetComponent<XRBaseController>();
            if (controller != null && !interactors.Contains(controller))
            {
                interactors.Add(controller);
            }
        }

        private void OnSelectExited(SelectExitEventArgs args)
        {
            var controller = args.interactorObject.transform.GetComponent<XRBaseController>();
            if (controller != null && interactors.Contains(controller))
            {
                interactors.Remove(controller);
            }
        }

        public List<XRBaseController> GetInteractingControllers()
        {
            return new List<XRBaseController>(interactors);
        }
    }
}
