using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerAutoDrive : MonoBehaviour
{
    private Prometho2 prometeoCarController;
    [SerializeField] private float waitTimeWhenObstacle;
    
    private Transform objectToMoveTo = null;
    
    private float shouldWaitFloat;

    private float threshold = 0.1f;
    private float rotationSpeed = 90f; // Degrees per second
    private float elapsedTime = 0f; // Time elapsed since rotation started

    private Vector3 positionLastPointHitCar;

    private float timeToGoStraightRegular = 0.15f;
    private float timeToGoStraightTimer;


    private float thresholdNew = 1f;

    private float timeWhenHitIdealLineRegular = 0.15f;
    private float timeWhenHitIdealLineCounter;
    [SerializeField] private Transform next;
    void Start()
    {
        prometeoCarController = GetComponent<Prometho2>();
    }

    // Update is called once per frame
    void Update()
    {
        prometeoCarController.shouldGoFordward = true;
        prometeoCarController.shouldStop = false;
        if (!objectToMoveTo)
        {
            //try to get in lane
            //prometeoCarController.TurnCenter();
            
             if (transform.rotation.eulerAngles.y + 90f > 90f)
            {
                Debug.Log(transform.rotation.eulerAngles);
                prometeoCarController.shouldTurnLeft = true;
                prometeoCarController.shouldTurnRight = false;
            }
             else if (transform.localRotation.eulerAngles.y > 60f)
                         {
                             Debug.Log("right");
                             prometeoCarController.shouldTurnRight = true;
                             prometeoCarController.shouldTurnLeft = false;
                         }

            return;
        }
        //prometeoCarController.shouldTiresCenter = false;
       Debug.DrawRay(objectToMoveTo.position,objectToMoveTo.forward * 20,Color.magenta); 
        timeToGoStraightTimer -= Time.deltaTime;
        timeWhenHitIdealLineCounter -= Time.deltaTime;
        
        shouldWaitFloat -= Time.deltaTime;
        if (shouldWaitFloat > 0f)
        {
            Debug.Log("stopping");
            prometeoCarController.shouldGoFordward = false;
            return;
        }

        if (checkIfSomeoneInFront())
        {
            Debug.Log("stopping");
            stopCar();
            
            return;
        }

        if ((objectToMoveTo.position - transform.position).magnitude < 4)
        {
            Debug.Log("is closer switchign to next node");
            if (objectToMoveTo.TryGetComponent(out DirectionPoint directionPoint))
            {
                if (directionPoint.IsNextBlockSet())
                {
                    objectToMoveTo = directionPoint.NextBlock;
                }
                else
                {
                    Debug.Log("finished parkour");
                    //prometeoCarController.shouldTiresCenter = true;
                    prometeoCarController.TurnCenter();
                    objectToMoveTo = null;
                    return;
                }
            }
            else
            {
                Debug.Log("something off");
            }
            
        }

        if (timeToGoStraightTimer > 0f)
        {
            return;
        }
        Vector3 nextPos = new Vector3(objectToMoveTo.position.x, 0f, objectToMoveTo.position.z);
        Vector3 ownPos = new Vector3(transform.position.x, 0f, transform.position.z);
            Vector3 dir = ( nextPos - ownPos);
            Vector3 dirNorm = dir.normalized;
            Debug.DrawRay(transform.position,dir * 200, Color.cyan);
            var i = new Vector3(transform.forward.x, 0f, transform.forward.z);
            var f =i.normalized;
            var t = Vector3.Dot(f, dirNorm);
            if (t >= 0.97f)
            {
                if (timeWhenHitIdealLineCounter < 0f)
                {
                    timeWhenHitIdealLineCounter = timeWhenHitIdealLineRegular;
                    //Debug.Log("setting ideal line counter");
                    return;
                }
                else if(timeWhenHitIdealLineCounter > 0.1f)
                {
                  //  Debug.Log("still ideal line counter");
                    return;
                }
                //Debug.Log("ideal time still going on");
                timeToGoStraightTimer = timeToGoStraightRegular;
                prometeoCarController.shouldTurnRight = false;
                prometeoCarController.shouldTurnLeft = false;
                //Debug.Log("stay on course");
                //prometeoCarController.shouldTiresCenter = true;
                prometeoCarController.TurnCenter();
                return;
            }
        Debug.Log(t.ToString() + ": " + f);
        /*
        if (dir.z < thresholdNew && dir.z > -threshold)
        {
            prometeoCarController.shouldTiresCenter = true;
            return;
        }
*/
        //prometeoCarController.shouldTiresCenter = false;
        if (f.x > dirNorm.x )
        {
            prometeoCarController.shouldTurnRight = true;
            prometeoCarController.shouldTurnLeft = false;
            Debug.Log("go right");
        }
        else
        {
            prometeoCarController.shouldTurnLeft = true;
            prometeoCarController.shouldTurnRight = false;
        }
        //parentObj.position += parentObj.transform.forward * forwardSpeed * Time.deltaTime;
        prometeoCarController.shouldStop = false;
        //prometeoCarController.shouldGoFordward = true;


    }

    private bool checkIfSomeoneInFront()
    {
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == AllTags.RodePoints.ToString())
        {
            Debug.Log("soemthing elsesett");
            return;
        }
        Debug.Log("hit soemthing car");
        //Hedgehog hedgehog = new Hedgehog();
        var res = other.TryGetComponent(out Hedgehog hedgehog);
        if(!res) return;
            Debug.Log("so far workjed");
            if (!hedgehog.canGo())
            {
                Debug.Log("it cant go");
                stopCar();
                return;
            }

           Debug.Log("setting objectmoveto");
            objectToMoveTo = hedgehog.StartPoint;
            Debug.Log("setted");
            elapsedTime = 0f;
            prometeoCarController.shouldTurnLeft = false;
            prometeoCarController.shouldTurnRight = false;
            prometeoCarController.shouldGoFordward = false;
            prometeoCarController.shouldGoBackward = false;
            prometeoCarController.shouldStop = true;
    }

    private void stopCar()
    {
        shouldWaitFloat = waitTimeWhenObstacle;
        prometeoCarController.shouldTurnLeft = false;
        prometeoCarController.shouldTurnRight = false;
        prometeoCarController.shouldGoFordward = false;
        prometeoCarController.shouldGoBackward = false;
        prometeoCarController.shouldStop = true;
    }
}
