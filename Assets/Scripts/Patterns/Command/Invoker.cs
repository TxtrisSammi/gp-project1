using UnityEngine;
using System.Collections.Generic;

public class Invoker : MonoBehaviour
{
    // Recording state
    private bool _isRecording;
    private bool _isReplaying;
    private float _replayTime;
    private int _replayIndex;

    // Command storage with timestamps
    private List<(ICommand command, float timestamp)> _recordedCommands;

    void Start()
    {
        _recordedCommands = new List<(ICommand, float)>();
    }

    public void StartRecording()
    {
        _recordedCommands.Clear();
        _isRecording = true;
        Debug.Log("[Invoker] Started Recording");
    }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        if (_isRecording)
            _recordedCommands.Add((command, Time.time));
    }
    
    public void Undo(ICommand command) 
    {
        command.Undo();
        Debug.Log($"[Invoker: {command}] Undo Called");
    }
    
    // Continuing Invoker class...

    public void StopRecording()
    {
        _isRecording = false;
        Debug.Log("[Invoker] Stopped Recording");

    }

    public void StartReplay()
    {
        if (_recordedCommands.Count == 0) return;

        _isReplaying = true;
        _replayIndex = 0;
        _replayTime = Time.time;
    }
    
    void Update()
    {
        if (_isReplaying && _replayIndex < _recordedCommands.Count)
        {
            var (command, timestamp) = _recordedCommands[_replayIndex];
            float elapsedTime = Time.time - _replayTime;

            if (elapsedTime >= timestamp - _recordedCommands[0].timestamp)
            {
                command.Execute();
                _replayIndex++;
            }

            if (_replayIndex >= _recordedCommands.Count)
                _isReplaying = false;
        }
    }
}
