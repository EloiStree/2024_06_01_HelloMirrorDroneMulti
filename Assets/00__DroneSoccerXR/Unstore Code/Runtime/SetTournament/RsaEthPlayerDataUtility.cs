using System.Collections.Generic;
using System.Linq;

public class RsaEthPlayerDataUtility
{ 
    public static void GetListRsaOf( out List<string> rsa, out List<string>ehterAddress, params RsaEthPlayerData[] players) {
        rsa= players.Select(x=>x.m_rsaPublicKey).ToList();
        ehterAddress= players.Select(x=>x.m_ethAddressToTarget).ToList();
    }
}
