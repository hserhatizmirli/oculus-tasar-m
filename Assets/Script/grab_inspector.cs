using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class GrabInspector : MonoBehaviour
{
    public List<XRBaseController> interactors = new List<XRBaseController>();
    private XRGrabInteractable grab;

    private void Awake()
    {
        grab = gameObject.GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (grab == null)
        {
            return;
        }
        grab.selectEntered.AddListener(AddInteractingController);
        grab.selectExited.AddListener(RemoveInteractingController);

    }

    private void OnDisable()
    {
        grab.selectEntered.RemoveListener(AddInteractingController);
        grab.selectExited.RemoveListener(RemoveInteractingController);
    }

    private void AddInteractingController(SelectEnterEventArgs args)
    {
        interactors.Add(args.interactorObject.transform.gameObject.GetComponent<XRBaseController>());

    }

    private void RemoveInteractingController(SelectExitEventArgs args)
    {
        interactors.Remove(args.interactorObject.transform.gameObject.GetComponent<XRBaseController>());
    }

    public List<XRBaseController> GetInteractingControllers()
    {
        return interactors.ToList();
    }
}
