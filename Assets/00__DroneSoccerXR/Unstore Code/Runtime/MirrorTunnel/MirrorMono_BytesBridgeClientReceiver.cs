using UnityEngine;
using UnityEngine.Events;

public class MirrorMono_BytesBridgeClientReceiver: MonoBehaviour
{

    public byte[] m_bytesRecevied;
    public UnityEvent<byte[]> m_onByteEventReceived;

    private void OnEnable()
    {
        MirrorMono_ByteBridgeToClient.m_receivedOnClientFromServer+=OnByteEventReceived;
    }
    public void OnDisable()
    {
        MirrorMono_ByteBridgeToClient.m_receivedOnClientFromServer-=OnByteEventReceived;
    }

    private void OnByteEventReceived(byte[] bytes)
    {
        m_bytesRecevied = bytes;
        m_onByteEventReceived.Invoke(bytes);
    }
}