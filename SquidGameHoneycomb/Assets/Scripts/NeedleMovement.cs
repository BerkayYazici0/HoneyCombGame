using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class NeedleMovement : Singleton<NeedleMovement>
{
    public PathCreator pathCreator;
    [SerializeField] private float speed = .2f;
    float distanceTravelled;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Loop);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Loop);
        }

        if(GameManager.Instance.currentLevelIndex == 1)
        {
            pathCreator = SugarBreakup.Instance.path;
        }

    }
}
