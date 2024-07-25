using System;
using UnityEngine;
using Unity.Netcode;

namespace DefaultNamespace
{
    public class ServerManager : MonoBehaviour
    {
        private NetworkManager netMan;

        private void Awake()
        {
            netMan = GetComponent<NetworkManager>();
        }

        
    }
}