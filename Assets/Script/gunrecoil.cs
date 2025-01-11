using UnityEngine;

public class TriggerOuterAnimation : MonoBehaviour
{
    public Animator outerAnimator; // Dýþ objenin Animator bileþeni

    public void PlayOuterAnimation()
    {
        if (outerAnimator != null)
        {
            outerAnimator.SetTrigger("Play"); // Dýþ animasyonu tetiklemek için bir trigger
        }
    }
}
