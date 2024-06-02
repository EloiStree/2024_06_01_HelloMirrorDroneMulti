using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class AutoLoadServerClientOnPublicIP : MonoBehaviour
{

    public bool m_autoLoadServerIfIpMatched;
    public string[] m_ipOfServer= new string[] { "168.168.1.250", "81.240.94.97" };

    public string m_currentPublicIP;
    public string [] m_currentLocalIP;
    public UnityEvent m_onIsServer;
    public UnityEvent m_onIsClient;
    private void Start()
    {
        StartCoroutine(FetchPublicIP());
    }

    private IEnumerator FetchPublicIP()
    {
        FetchLocalIp();
        using (WebClient client = new WebClient())
        {
            try
            {
                string response = client.DownloadString("https://api.ipify.org");
                Debug.Log("Public IP: " + response);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to fetch public IP: " + e.Message);
            }
        }

        yield return null;
    }

    public void FetchLocalIp()
    {
        List<string> ipAddresses = new List<string>(); // Declare a list to store IP addresses

        // Get all network interfaces
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

        foreach (NetworkInterface iface in interfaces)
        {
            // Check if the interface is up and not a loopback or virtual interface
            if (iface.OperationalStatus == OperationalStatus.Up &&
                iface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            {
                // Get all IP addresses associated with the interface
                IPInterfaceProperties properties = iface.GetIPProperties();
                foreach (UnicastIPAddressInformation address in properties.UnicastAddresses)
                {
                    // Check if the address is IPv4 and not a link-local or loopback address
                    if (address.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                        !IPAddress.IsLoopback(address.Address) &&
                        !address.Address.IsIPv6LinkLocal)
                    {
                        ipAddresses.Add(address.Address.ToString()); // Add the IP address to the list
                    }
                }
            }
        }

        m_currentLocalIP = ipAddresses.ToArray();
    }
}
