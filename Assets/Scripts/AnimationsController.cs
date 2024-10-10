using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] Animator animatorController;

    void Start()
    {
        // Obtiene el componente Animator al inicio
        animatorController = GetComponent<Animator>();
    }

    // M�todo para activar una animaci�n mediante un trigger en el componente Animator
    public void Animations(string animation)
    {
        animatorController.SetTrigger(animation);
    }

    // M�todo para resetear la animaci�n a un estado idle
    public void ResetAnimation(bool value)
    {
        animatorController.SetBool("Idle",value);
    }
}
