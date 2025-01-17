using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public static class Core
{

    public static GameObject FindGameObjectByNameAndTag(string name, string tag)
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag(tag);
        GameObject returnObject = null;
        foreach (GameObject taggedOne in go)
        {
            if (taggedOne.name == name)
            {
                returnObject = taggedOne;
            }
        }

        return returnObject;
    }

    public static GameObject FindHiddenGameObjectByNameAndTag(string name, string tag)
    {
        List<GameObject> go = GetAllObjectsInScene();
        GameObject returnObject = null;
        foreach (GameObject taggedOne in go)
        {
            if (taggedOne.name == name)
            {
                returnObject = taggedOne;
            }
        }

        return returnObject;
    }

    public static List<GameObject> GetAllObjectsInScene()
    {
        List<GameObject> objectsInScene = new List<GameObject>();

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.hideFlags != HideFlags.None)
                continue;
            if (PrefabUtility.GetPrefabType(go) == PrefabType.Prefab || PrefabUtility.GetPrefabType(go) == PrefabType.ModelPrefab)
                continue;
            objectsInScene.Add(go);
        }
        return objectsInScene;
    }

    public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.GetComponent<T>();
            }
        }
        return null;
    }

    public static T[] FindComponentsInChildrenWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }
        List<T> list = new List<T>(parent.GetComponentsInChildren<T>(forceActive));
        if (list.Count == 0) { return null; }

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].CompareTag(tag) == false)
            {
                list.RemoveAt(i);
            }
        }
        return list.ToArray();
    }

    public static bool ContainsAny(this string haystack, params string[] needles)
    {
        foreach (string needle in needles)
        {
            if (haystack.Contains(needle))
                return true;
        }

        return false;
    }

    public static bool ContainsAll(string[] haystacks, string[] needles)
    {
        bool foundAll = new bool();
        foundAll = false;

        foreach (string needle in needles)
        {
            foundAll = false;
            foreach (string haystack in haystacks)
            {
                MonoBehaviour.print(haystack);
                if (haystack == needle)
                {
                    MonoBehaviour.print("we true");
                    foundAll = true;
                }
            }
            if (!foundAll)
            {
                MonoBehaviour.print("heredude");
                foundAll = false;
            }
        }

        return foundAll;
    }

    public static GameObject GetFirstParentWithTag(GameObject gameObject, string parentTag) 
    {
        int debugIndex = 0;
        GameObject currentGO = gameObject;
        while (currentGO.tag != parentTag)
        {
            Console.WriteLine(currentGO.tag);
            currentGO = currentGO.transform.parent.gameObject;

            debugIndex += 1;
            if (debugIndex > 50)
            {
                Debug.LogWarning("Could not find parent with tag " + parentTag + " after 50 loops");
                return null;
            }
        }

        return currentGO;
    }

    public static bool Contains(Array a, object val)
    {
        return Array.IndexOf(a, val) != -1;
    }

    public static int GetIndex(Array a, object val)
    {
        return Array.IndexOf(a, val);
    }
}