using System;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    public delegate void ActionPerformed(int argument);
    private static Message instance;
    private ActionPerformed actions;

    #region Monobehavior

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

    public bool RegisterEvent(ActionPerformed action)
    {
        var value = false;

        try
        {
            if (instance.actions is null)
                actions = action;
            else
                actions += action;

            value = true;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        return value;
    }

    public bool TriggerEvent(string name, int arg)
    {
        var value = false;

        try
        {
            instance.actions?.Invoke(arg);
            value = true;
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        return value;
    }

}
