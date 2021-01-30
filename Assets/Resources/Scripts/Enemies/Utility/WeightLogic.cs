using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeightLogic:MonoBehaviour
{
    
    private const int directionsNumber = 36;
    public string logic;
    private Vector3[] directions;
    private float[] weights;
    private float[] additions;
    private void Awake()
    {
        InitializeDirections();
        weights = new float[directionsNumber];
        additions = new float[directionsNumber];
        logic = "Ranged";
    }
    private void Update()
    {
        CalculateWeights();
        ApplyAdditions();
        Vector3 nextDirection = FindDirection();
        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.TransformDirection(directions[i]), out hit);
            if (nextDirection.Equals(directions[i]))
            {
                Debug.DrawLine(transform.position, hit.point, Color.blue);
            }
            else
            {
                if (weights[i] < 0)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
                else if (weights[i] == 0)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.yellow);
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.green);
                }
            }
        }
    }
    private void InitializeDirections()
    {
        directions = new Vector3[directionsNumber];
        for (int i = 0; i < directionsNumber; i++)
        {
            if (i < directionsNumber * 0.25f)
            {
                directions[i] = new Vector3((1f / directionsNumber) * i, 0, (1f / directionsNumber) * (directionsNumber * 0.25f - i));
            }
            else if (i < directionsNumber * 0.5f)
            {
                directions[i] = new Vector3((1f / directionsNumber) * (directionsNumber * 0.5f - i), 0, -(1f / directionsNumber) * (i - directionsNumber * 0.25f));
            }
            else if (i < directionsNumber * 0.75f)
            {
                directions[i] = new Vector3(-(1f / directionsNumber) * (i - directionsNumber * 0.5f), 0, -(1f / directionsNumber) * (directionsNumber * 0.75f - i));
            }
            else if (i < directionsNumber)
            {
                directions[i] = new Vector3(-(1f / directionsNumber) * (directionsNumber - i), 0, (1f / directionsNumber) * (i - directionsNumber * 0.75f));
            }
        }
    }
    private void CalculateWeights()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.TransformDirection(directions[i]), out hit);
            if (hit.transform != null)
            {
                float hitDistance = Vector3.Distance(transform.position, hit.transform.position);
                if (hit.transform.tag.Equals("Player"))
                {
                    if (logic.Equals("Ranged"))
                    {
                        float enemyRange = gameObject.GetComponent<Ranged>().current_range;
                        if (hitDistance >= enemyRange)
                        {
                            weights[i] = (hitDistance - enemyRange) / hitDistance;
                            additions[i] = 1;
                            additions[(i+directionsNumber/2)%directionsNumber] = -1;
                        }
                        else
                        {
                            weights[i] = (hitDistance - enemyRange) / enemyRange;
                            additions[i] = -1;
                            additions[(i + directionsNumber / 2) % directionsNumber] = 1;
                        }
                    }
                }
                else if (hit.transform.tag.Equals("Wall"))
                {
                    if (logic.Equals("Ranged"))
                    {
                        if (hitDistance > 4)
                        {
                            weights[i] = (hitDistance - 4) / hitDistance;
                        }
                        else
                        {
                            weights[i] = (hitDistance - 4) / 4;
                        }
                    }

                }
            }
        }
    }

    private void ApplyAdditions()
    {
        for(int i = 0; i < directionsNumber; i++)
        {
            weights[i] += additions[i];
            additions[i] = 0;
        }

    }
    private Vector3 FindDirection()
    {
        float max = weights.Max();
        return directions[weights.ToList().IndexOf(max)];
    }
}
