using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjPool<T> where T : MonoBehaviour
{
    public T prefab { get; }
    public bool IsAutoExpand { get; set; }
    
    private Transform Container { get; }

    private List<T> _pool;
    

    public ObjPool(T prefab, int count)
    {
        this.prefab = prefab;
        this.Container = null;

        this.CreatePool(count);
    }

    public ObjPool(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.Container = container;

        this.CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this._pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            this.CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(this.prefab, this.Container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this._pool.Add(createdObject);
        return createdObject;
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var obj in _pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                element = obj;
                obj.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
            return element;

        if (this.IsAutoExpand)
            return CreateObject(true);
        throw new Exception($"There is no free elements of type{typeof(T)}");
    }
}
