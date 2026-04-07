using UnityEngine;

/// <summary>
/// Base Manager used to class manager implement singleton pattern
/// </summary>
/// <typeparam name="T">Type parameter</typeparam>

public abstract class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get { return ManagerHelper.instance; } }

    private static class ManagerHelper
    {
        internal static readonly T instance;
        static ManagerHelper()
        {
            GameObject obj = GameObject.Find(typeof(T).Name);
            if (obj == null)
            {
                obj = new GameObject(typeof(T).Name);
                instance = obj.AddComponent<T>();
            }
            else
            {
                instance = obj.GetComponent<T>();
            }
            DontDestroyOnLoad(obj);
        }
    }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
