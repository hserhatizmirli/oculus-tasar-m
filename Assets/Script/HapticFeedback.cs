using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// XRVariablesSO sýnýfý - ScriptableObject olarak tanýmlanýr
[CreateAssetMenu(fileName = "XRVariables", menuName = "XR/XR Variables")]
public class XRVariablesSO : ScriptableObject
{
    public FeedbackManager feedbackManager;
}

// FeedbackManager sýnýfý - Haptic feedback'leri yönetecek
public class FeedbackManager : MonoBehaviour
{
    public void ReceiveFeedback(float duration, float impulse, XRBaseController controller)
    {
        if (controller != null)
        {
            controller.SendHapticImpulse(impulse, duration);
            Debug.Log($"Haptic feedback sent to {controller.name} with impulse {impulse} and duration {duration}");
        }
        else
        {
            Debug.LogWarning("Controller is null. Feedback not sent.");
        }
    }
}

// HapticFeedback sýnýfý - XRGrabInteractable ve GrabInspector üzerinden geribildirim gönderir
public class HapticFeedback : MonoBehaviour
{
    public float duration = 0.5f; // Varsayýlan süre
    [Range(0f, 1f)]
    public float impulse = 0.5f; // Varsayýlan impulse deðeri
    public XRVariablesSO xrVariables; // XRVariablesSO referansý
    public XRGrabInteractable grab; // XRGrabInteractable referansý
    public GrabInspector grabInspector; // GrabInspector referansý

    [ContextMenu("Test feedback")]
    public void GiveFeedback()
    {
        // GrabInspector üzerinden etkileþimdeki kontrolcülere geribildirim gönder
        if (grabInspector != null)
        {
            var interactingControllers = grabInspector.GetInteractingControllers();
            if (interactingControllers != null && interactingControllers.Count > 0)
            {
                foreach (var controller in interactingControllers)
                {
                    SendFeedback(controller);
                }
            }
            else
            {
                Debug.LogWarning("GrabInspector does not contain any interacting controllers.");
            }
        }
        else
        {
            Debug.LogWarning("GrabInspector is not assigned.");
        }

        // XRGrabInteractable üzerinden etkileþimdeki kontrolcülere geribildirim gönder
        if (grab != null)
        {
            foreach (var interactor in grab.interactorsSelecting)
            {
                var controller = interactor.transform.gameObject.GetComponent<XRBaseController>();
                if (controller != null)
                {
                    SendFeedback(controller);
                }
                else
                {
                    Debug.LogWarning("Interactor does not have an XRBaseController component.");
                }
            }
        }
        else
        {
            Debug.LogWarning("XRGrabInteractable is not assigned.");
        }
    }

    private void SendFeedback(XRBaseController controller)
    {
        if (xrVariables != null && xrVariables.feedbackManager != null)
        {
            xrVariables.feedbackManager.ReceiveFeedback(duration, impulse, controller);
        }
        else
        {
            Debug.LogError("XRVariables or FeedbackManager is not assigned.");
        }
    }
}
