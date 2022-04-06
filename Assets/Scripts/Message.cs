using System;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    private static Message instance;
    private Dictionary<string, List<Action>> events;

    #region Monobehavior
    void Start()
    {
        Init();
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    private void Init()
    {
        instance.events = new Dictionary<string, List<Action>>();
    }

    public bool RegisterEvent(string name, Action handler)
    {
        var value = false;

        try
        {
            if (!events.ContainsKey(name))
                instance.events.Add(name, new List<Action>());

            instance.events[name].Add(handler);

            value = true;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        return value;
    }

    public bool TriggerEvent(string name)
    {
        var value = false;

        try
        {
            foreach(var function in instance.events[name])
                function.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        return value;
    }
}
