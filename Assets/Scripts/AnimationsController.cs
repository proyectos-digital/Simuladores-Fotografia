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

    // Método para activar una animación mediante un trigger en el componente Animator
    public void Animations(string animation)
    {
        animatorController.SetTrigger(animation);
    }

    // Método para resetear la animación a un estado idle
    public void ResetAnimation(bool value)
    {
        animatorController.SetBool("Idle",value);
    }
}
