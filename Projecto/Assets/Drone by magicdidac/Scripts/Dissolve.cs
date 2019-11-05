using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{

    [HideInInspector] private List<Renderer> renderers = null;

    [HideInInspector] private float cutOffValue = 0;

    [HideInInspector] private bool isDisappeared = false;
    [HideInInspector] private bool isDisappearing = false;

    private void Awake()
    {
        renderers = SelectDissolveRenders();
    }

    private void Update()
    {
        if (isDisappearing && cutOffValue < 1)
        {
            cutOffValue = Vector2.MoveTowards(new Vector2(cutOffValue, 0), Vector2.right, Time.deltaTime).x;

        }else if (!isDisappearing && cutOffValue > 0)
        {
            cutOffValue = Vector2.MoveTowards(new Vector2(cutOffValue, 0), Vector2.zero, Time.deltaTime).x;
        }

        if (cutOffValue == 1)
            isDisappeared = true;
        else
            isDisappeared = false;

        foreach (Renderer r in renderers)
        {
            r.material.SetFloat("_Cutoff", cutOffValue);
        }
    }

    public List<Renderer> SelectDissolveRenders()
    {
        return SelectRenderers(gameObject);
    }

    private List<Renderer> SelectRenderers(GameObject obj)
    {

        if (obj.transform.childCount != 0)
        {
            List<Renderer> n = new List<Renderer>();

            for (int i = 0; i < obj.transform.childCount; i++)
            {
                n.AddRange(SelectRenderers(obj.transform.GetChild(i).gameObject));
            }

            if (obj.GetComponent<Renderer>())
                n.Add(obj.GetComponent<Renderer>());

            return n;

        }
        else
        {
            if (obj.GetComponent<Renderer>())
            {
                List<Renderer> r = new List<Renderer>();
                r.Add(obj.GetComponent<Renderer>());
                return r;
            }
        }

        return null;

    }

    public void Dissappear() => isDisappearing = true;

    public void Appear() => isDisappearing = false;

    public bool IsDisappeared()
    {
        return isDisappeared;
    }

}
