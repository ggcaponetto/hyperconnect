using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UI;
using System.IO;
using System.ComponentModel;
using System;
using UnityEngine;
using System.Collections;

public class HyperConnect : UnityEngine.MonoBehaviour
{
    // Start is called before the first frame update
    public string hyconName;
    public Button button;
    public string hcClientPath = "C:\\Users\\Giuseppe\\intellij-workspace\\hyperconnect\\Assets\\chat-example\\build\\hyperconnect.exe";
    private StreamWriter myStreamWriter;

    private Process myProcess;
    void Start()
    {
        UnityEngine.Debug.Log(hyconName + " started.");
        button.onClick.AddListener(TaskOnClick);
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
            myProcess.StartInfo.RedirectStandardInput = true;

            myProcess.StartInfo.FileName = hcClientPath;
            myProcess.EnableRaisingEvents = true;
            myProcess.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            myProcess.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);

            myProcess.Start();
            myProcess.BeginOutputReadLine();
            myProcess.BeginErrorReadLine();

            myStreamWriter = myProcess.StandardInput;

        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(hyconName + " exited with error " + e.ToString());
        }
    }


    private void TaskOnClick()
    {
        UnityEngine.Debug.Log("TaskOnClick");
        try{
            myStreamWriter.WriteLine("command from unity");
        }catch (Exception e){
            UnityEngine.Debug.Log(hyconName + " TaskOnClick error " + e.ToString());
        }
    }

    private void OnApplicationQuit()
    {
        UnityEngine.Debug.Log("OnApplicationQuit");
        try
        {
            myProcess.Kill();
            myStreamWriter.Close();
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
