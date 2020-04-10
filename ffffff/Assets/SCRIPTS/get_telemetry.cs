using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class get_telemetry : MonoBehaviour
{
    string[] args = System.Environment.GetCommandLineArgs();
    string serverPort = "3000";
    string serverIP = "127.0.0.1"; 
    public bool isAtStartup = true;
    NetworkClient myClient;
    // Create a server and listen on a port

    void Update()
    {
        if (isAtStartup)
        {
            print("hello");
            //SetupServer();
            SetupClient();

        }
    }

public void SetupServer()
    {
        NetworkServer.Listen(3000);
        isAtStartup = false;
    }

    // Create a client and connect to the server port
    public void SetupClient()
    {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.Connect("127.0.0.1", 3000);
        isAtStartup = false;
    }

    // Create a local client and connect to the local server
    public void SetupLocalClient()
    {
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        isAtStartup = false;
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        print("Connected to server");
        print(netMsg);
    }

}