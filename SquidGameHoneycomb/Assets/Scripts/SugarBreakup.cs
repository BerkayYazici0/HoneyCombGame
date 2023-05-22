using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SugarBreakup : Singleton<SugarBreakup>
{
    public PathCreator path;
    [SerializeField] public GameObject currentPath;
    [SerializeField] public GameObject[] sugars;
    [SerializeField] public GameObject[] levelObject;
    [SerializeField] public GameObject[] brokenSugars;
    [SerializeField] public GameObject[] brokenLevelObject;
    [SerializeField] public List<Vector3> pathPoints = new List<Vector3>();
    [SerializeField] public GameObject currentLevel;
    public AnimationCurve animCurve;
    [HideInInspector] public int i = 0;
    private int trianglePart;
    private float timer = 0f;
    private float smooth = 6f;

    private void Start()
    {
        FindLevelObjects(GameManager.Instance.currentLevelIndex);
        for (int i = 0; i < brokenSugars.Length; i++)
        {
            brokenSugars[i].transform.gameObject.SetActive(false);
        }
        for (int i = 0; i < brokenLevelObject.Length; i++)
        {
            brokenLevelObject[i].transform.gameObject.SetActive(false);
        }
        for (int i = 0; i < path.path.NumPoints; i++)
        {
            pathPoints.Add(path.path.GetPoint(i));
        }
    }
    void Update()
    {
        Debug.Log(i);
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if(timer >= 1f)
            {
                var rotationAngle = Quaternion.Euler(new Vector3(0, 0, Random.Range(-20, 20)));
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationAngle, Time.deltaTime * smooth * animCurve.Evaluate(timer));
            }
            if(timer >= 2f)
            {                
                brokenLevelObject[trianglePart].transform.gameObject.SetActive(true);
                sugars[trianglePart].transform.gameObject.SetActive(false);
                GameManager.Instance.Fail();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            timer = 0;
        }
                
        if(GameManager.Instance.currentLevelIndex == 0)
        {
            SugarsBreakup();
        }
        else if(GameManager.Instance.currentLevelIndex == 1)
        {
            i = 0;
            
            StarBreakup();
        }
        
    }

    void SugarsBreakup()
    {
        if(i < 6)
        {
            if (Vector3.Distance(transform.position, pathPoints[i + 1]) <= .01f)
            {
                levelObject[i].transform.gameObject.SetActive(false);
                brokenSugars[i].transform.gameObject.SetActive(true);
                i++;
            }

            if (i > 1)
                trianglePart = 1;
            else
                trianglePart = 0;
        }
        else
        {
            GameManager.Instance.Success();
            
        }
    }

    void StarBreakup()
    {
        
        i = 0;
        if(i < 11)
        {
            if (Vector3.Distance(transform.position, pathPoints[1]) <= .01f)
            {
                levelObject[i].transform.gameObject.SetActive(false);
                brokenSugars[i].transform.gameObject.SetActive(true);
                i++;
            }
            else if (Vector3.Distance(transform.position, pathPoints[5]) <= .01f)
            {
                levelObject[i].transform.gameObject.SetActive(false);
                brokenSugars[i].transform.gameObject.SetActive(true);
                i++;
            }
            else if(Vector3.Distance(transform.position, pathPoints[6]) <= .01f)
            {
                levelObject[i].transform.gameObject.SetActive(false);
                brokenSugars[i].transform.gameObject.SetActive(true);
                i++;
            }
            else if (Vector3.Distance(transform.position, pathPoints[9]) <= .01f)
            {
                levelObject[i].transform.gameObject.SetActive(false);
                brokenSugars[i].transform.gameObject.SetActive(true);
                i++;
            }
            else if (Vector3.Distance(transform.position, pathPoints[11]) <= .01f)
            {
                levelObject[i].transform.gameObject.SetActive(false);
                brokenSugars[i].transform.gameObject.SetActive(true);
                i++;
            }
            else
            {
                i++;
            }


            if (i > 6)
                trianglePart = 1;
            else
                trianglePart = 0;
        }
        else
        {
            GameManager.Instance.Success();
        }
    }
    public void FindLevelObjects(int level)
    {
        if(level == 0)
        {
            levelObject = GameObject.FindGameObjectsWithTag("Sugar");
            sugars = GameObject.FindGameObjectsWithTag("Triangle");
            brokenSugars = GameObject.FindGameObjectsWithTag("Broken");
            brokenLevelObject = GameObject.FindGameObjectsWithTag("TriangleBroken");
            currentPath = GameObject.FindGameObjectWithTag("Path");
            path = currentPath.GetComponent<PathCreator>();
        }        
    }
}
