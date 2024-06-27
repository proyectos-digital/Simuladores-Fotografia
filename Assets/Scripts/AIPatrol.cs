using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public Transform[] points;
    //public NavMeshAgent
    private int destPoint = 0;
    private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] bool nosferatu = false;
    [SerializeField] bool waitRecord = false;
    [SerializeField] AnimationsController chicaNosferatu;
    [SerializeField] AnimationsController chicoNosferatu;
    Vector3 originalPosition = Vector3.zero;
    bool prepare = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
        originalPosition = transform.position;
        if (!waitRecord)
            GotoNextPoint();
    }
    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }
    //Inicia la actriz a caminar hacia el punto indicado
    void StartAnimation()
    {
        waitRecord = false;
        GotoNextPoint();
        chicaNosferatu.ResetAnimation(waitRecord);
        chicoNosferatu.ResetAnimation(waitRecord);
        chicaNosferatu.Animations("Walk");
    }
    //Se Teletransporta la posición original de actriz y...
    //Reinicio de condiciones para el estado de animaciones
    void ResetAnimation()
    {
        agent.Warp(originalPosition);
        waitRecord =true;
        prepare = false;
        chicaNosferatu.ResetAnimation(waitRecord);
        chicoNosferatu.ResetAnimation(waitRecord);
    }
    // Update is called once per frame
    void Update()
    {
        //Cuando se presiona Tecla N --> Temporal se usara TvController o VideoCapture para iniciar animaciones
        if(Input.GetKeyUp(KeyCode.N))
        {
            StartAnimation();
        }
        //Tecla M --> Temporal se usara TvController o VideoCapture para resetear posiciones y animaciones
        if (Input.GetKeyUp(KeyCode.M))
        {
            ResetAnimation();
        }
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (!nosferatu)
            {
                //Segundo if Nosferatu falso para las demas escenas
                GotoNextPoint();
            }
            // Only one point y waitRecord es falso
            //Camina la actriz hacia Nosferatu
            if (points.Length == 1 && !waitRecord)
            {
                if (!prepare)
                {   
                    //Cambia la animación para sincronizarse con la mordida
                    chicaNosferatu.Animations("Prepared");
                    chicoNosferatu.Animations("Prepared");
                    prepare = true;
                }
            }
        }
    }
}
