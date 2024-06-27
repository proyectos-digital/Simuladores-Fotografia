using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] Animator animatorController;
    // Start is called before the first frame update
    void Start()
    {
        animatorController = GetComponent<Animator>();
    }
    public void Animations(string animation)
    {
        animatorController.SetTrigger(animation);
    }
    public void ResetAnimation(bool value)
    {
        animatorController.SetBool("Idle",value);
    }
}
