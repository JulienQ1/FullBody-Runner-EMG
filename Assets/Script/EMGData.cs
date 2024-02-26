using System;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMGData : MonoBehaviour
{

    private TcpClient client;
    private NetworkStream stream;
    public PlayerController player;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private Text EmgVal;

    // Start is called before the first frame update
    void Start()
    {
        client = new TcpClient("localhost", 12345);
        stream = client.GetStream();

        StartCoroutine(ReadMessages());
    }
    IEnumerator ReadMessages()
    {
        byte[] data = new byte[1024];

        while (true)
        {
            if (stream.DataAvailable)
            {
                int bytesRead = stream.Read(data, 0, data.Length);
                string message = Encoding.UTF8.GetString(data, 0, bytesRead);

                string[] val = message.Split(',');
                foreach (var number in val)
                {
                    Debug.Log(number);
                    double walk = try_double(number);
                    if(walk > 0.005 )
                    {
                        player.IsWalking = true;
                    }
                    else
                    {
                        player.IsWalking = false;
                    }
                    // EmgVal.text = "EMG value : "+jump;
                }

                
            }
            yield return new WaitForSeconds(5);
            yield return null;
        }
       
    }

    private double try_double(string v)
    {
        try
        {
            return double.Parse(v, System.Globalization.CultureInfo.InvariantCulture);
        } 
        catch
        {
            return 0.0;
        }
        
    }
        

    void OnApplicationQuit()
    {
        client.Close();
    }

   
}
