using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : MonoBehaviour
{
    [SerializeField] private Hedgehog turnRightHedghehog;
    [SerializeField] private Hedgehog turnLeftHedgehog;
    public Transform StartPoint;
    private List<object> gameObjectsInsideHedgehog;

    private void Awake()
    {
        gameObjectsInsideHedgehog = new List<object>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == AllTags.RodePoints.ToString()) return;
            Debug.Log("something hit hedgehog");
        gameObjectsInsideHedgehog.Add(other);
        if (other.tag == AllTags.Player.ToString())
        {
            gameObjectsInsideHedgehog.Add(other.GetComponent<Transform>());
        }
        else if (other.tag == AllTags.Car.ToString())
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameObjectsInsideHedgehog.Remove(other);
    }

    public bool canGo()
    {
        //if (gameObjectsInsideHedgehog.Count != 1) return false;
        //if (turnLeftHedgehog.somethingWithIn()) return false;
        //if (turnRightHedghehog.somethingWithIn()) return false;
        return true;
    }

    public bool somethingWithIn()
    {
        return gameObjectsInsideHedgehog.Count > 0;
    }
}



