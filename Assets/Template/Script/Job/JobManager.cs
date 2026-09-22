using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    public bool istaken { get; private set; }

    public virtual bool cantake()
    {
        return !istaken;
    }

    public virtual void takejob()
    {
        istaken = true;
    }

    public virtual void finishjob()
    {
        istaken = false;
    }
}
