using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UnityのDebugLogをUI.Textに反映させる
/// </summary>
public class DebugLogText : MonoBehaviour
{
    /// <summary>
    /// Logと一緒にTimestampも表示する
    /// </summary>
    public bool ShowTimestamp = false;

    /// <summary>
    /// Logと一緒にStackTraceも表示する
    /// </summary>
    public bool ShowStackTrace = false;

    [SerializeField]
    Text text;

    bool logEventAdded = false;

    void Awake()
    {
        // Textの参照がない場合、GameObjectから取得を試みる
        if (!text)
        {
            text = GetComponent<Text>();
        }

        // それでもTextの参照がない場合、動作しない
        if (!text)
        {
            enabled = false;
            return;
        }

        text.text = null;

        // なるべく早い段階でイベント登録を行う
        OnEnable();
    }

    void OnEnable()
    {
        if (!logEventAdded)
        {
            Application.logMessageReceived += LogReceived;
            logEventAdded = true;
        }
    }

    void OnDisable()
    {
        if (logEventAdded)
        {
            logEventAdded = false;
            Application.logMessageReceived -= LogReceived;
        }
    }

    void LogReceived(string condition, string stackTrace, LogType type)
    {
        string timeStamp = null;
        if (ShowTimestamp)
        {
            timeStamp = DateTime.Now.ToString("[yyyy/MM/dd hh:mm:ss.fff]") + Environment.NewLine;
        }

        if (!ShowStackTrace)
        {
            stackTrace = null;
        }

        string log = timeStamp + condition + Environment.NewLine + stackTrace;

        switch (type)
        {
            case LogType.Error:
            case LogType.Assert:
            case LogType.Exception:
                // red
                log = "<color=#ff0000>" + log + "</color>";
                break;

            case LogType.Warning:
                
                // yellow
                log = "<color=#ffff00>" + log + "</color>";
                break;

            default:
                break;
        }

        text.text = log + Environment.NewLine + text.text;
    }
}