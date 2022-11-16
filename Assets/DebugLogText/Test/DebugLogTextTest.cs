using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1•b‚¨‚«‚ÉLog‚ð—¬‚·
/// </summary>
public class DebugLogTextTest : MonoBehaviour
{
    float logIntervalSec = 1;

    void Start()
    {
        StartCoroutine(LogLoop());
    }

    IEnumerator LogLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(logIntervalSec);
            Debug.Log("Debug Log Called.");

            yield return new WaitForSeconds(logIntervalSec);
            Debug.LogWarning("Debug LogWarning Called.");

            yield return new WaitForSeconds(logIntervalSec);
            Debug.LogAssertion("Debug LogAssertion Called.");

            yield return new WaitForSeconds(logIntervalSec);
            Debug.LogException(new System.Exception("Debug LogException Called."));

            yield return new WaitForSeconds(logIntervalSec);
            Debug.LogError("Debug LogError Called.");
        }
    }
}
