using UnityEngine;

/// <summary>
/// Custom default class MonoBehaviour of unity which used to
/// LoadComponent a class has
/// </summary>
public abstract class MyBehaviour : MonoBehaviour
{
    //* Function Awake
    // Call LoadComponents Function
    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    //* Function OnEnable
    protected virtual void OnEnable()
    {
        // For override
    }

    //* Function Start
    protected virtual void Start()
    {
        // For override
    }

    //* Function Reset
    // Call LoadComponents, ResetValue
    protected virtual void Reset()
    {
        this.LoadComponents();
        this.ResetValue();
    }

    //* Function OnDisable
    protected virtual void OnDisable()
    {
        // For override
    }

    //* Function LoadComponents
    protected virtual void LoadComponents()
    {
        // For override
    }

    //* Function ResetValues
    protected virtual void ResetValue()
    {
        // For override
    }

    public interface IController
    {
    }
}
