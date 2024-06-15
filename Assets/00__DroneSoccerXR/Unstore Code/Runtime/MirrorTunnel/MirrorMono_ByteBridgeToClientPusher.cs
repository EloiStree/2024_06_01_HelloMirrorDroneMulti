using UnityEngine;

public class MirrorMono_ByteBridgeToClientPusher: MonoBehaviour
{

    public byte[] m_bytesPushed;

    public void PushByteEventToClient( byte[] bytes )
    {
        m_bytesPushed = bytes;
        MirrorMono_ByteBridgeToClient.PlayerSingleton.PushByteEventToClient(bytes);
    }
}
