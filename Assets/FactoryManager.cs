using System;
using UnityEngine;
using System.Net.Sockets;

public class FactoryManager : MonoBehaviour
{
    public Animator lightAnimator;
    public GameObject smoke;

    
    public string host = "127.0.0.1";
    public int port = 12321;
    TcpClient client;
    NetworkStream stream;
    private void Start()
    {
        ConnentToServer();
    }

    void ConnentToServer()
    {
        client= new TcpClient(host, port);
        stream = client.GetStream();
        Debug.Log("Connect to "+host + ":" + port);
    }

    private void Update()
    {
        ReceiveMessage();
    }

    void ReceiveMessage()
    {
        if (stream.DataAvailable)
        {
            byte[] data = new byte[1024];
            int bytesRead = stream.Read(data, 0, data.Length);
            string text = System.Text.Encoding.UTF8.GetString(data,0,bytesRead);
            if (text == "s")
            {
                StartSmoke();
            }

            if (text == "e")
            {
                StopSmoke();
            }
        }
    }

    public void StartSmoke()
    {
        smoke.SetActive(true);
        Invoke(nameof(LightDanger),1f);
    }

    public void StopSmoke()
    {
        smoke.SetActive(false);
        Invoke(nameof(LightSafe),1f);
    }

    public void LightSafe()
    {
        lightAnimator.SetBool("IsDanger", false);
    }

    public void LightDanger()
    {
        lightAnimator.SetBool("IsDanger", true);
    }
}
