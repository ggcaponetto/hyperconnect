using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class HyperConnect : MonoBehaviour
{
    // Start is called before the first frame update
    public string hyconName;

    private Process myProcess;
    void Start()
    {
        UnityEngine.Debug.Log(hyconName + " started.");
        StartProcess();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;
        
        // hyperconnectOut = myProcess.StandardOutput.ReadLine();
        // UnityEngine.Debug.Log(hyconName + " got message: ("+deltaTime+")" + hyperconnectOut);
        // UnityEngine.Debug.Log(hyconName + " update: ( " + deltaTime + "s )");
    }

    private void StartProcess()
    {
        try
        {
            myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo.RedirectStandardError = true;
            myProcess.StartInfo.FileName = "C:\\Users\\ggcap\\IdeaProjects\\chat-example\\build\\hyperconnect.exe";
            myProcess.EnableRaisingEvents = true;
            myProcess.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            myProcess.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            myProcess.Start();
            myProcess.BeginOutputReadLine();
            myProcess.BeginErrorReadLine();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(hyconName + " exited with error " + e.ToString());
        }
    }

    private void OnApplicationQuit()
    {
        UnityEngine.Debug.Log("OnApplicationQuit");
        try
        {
            myProcess.Kill();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("OnApplicationPause error: " + e.ToString());
        }
    }

    void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
    {
        UnityEngine.Debug.Log("OutputHandler update: " + outLine.Data);
    }
}
