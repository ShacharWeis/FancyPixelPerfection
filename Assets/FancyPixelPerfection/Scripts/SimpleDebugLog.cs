using TMPro;
using UnityEngine;

public class SimpleDebugLog : MonoBehaviour {
    public TextMeshProUGUI tmp;

    void Start() {
        tmp.text = "";
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type is LogType.Warning or LogType.Error) {
            tmp.text += logString + "\n";
        }
    }
}
