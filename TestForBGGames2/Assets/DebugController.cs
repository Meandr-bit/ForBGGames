using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public void ForceLevelComplete()
    {
        StartCoroutine(UIController.instance.LevelCompleted());
    }
}
