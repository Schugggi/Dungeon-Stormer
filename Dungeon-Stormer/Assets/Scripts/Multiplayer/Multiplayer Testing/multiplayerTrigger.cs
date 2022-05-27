using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class multiplayerTrigger : NetworkBehaviour
{
    public GameObject spawnOnTrigger;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        CmdSpawnBlock(Random.Range(10,-10), Random.Range(-10,10));
    }

    [Command(requiresAuthority = false)]
    private void CmdSpawnBlock(int x, int y)
    {
        GameObject placeHolder = Instantiate(spawnOnTrigger, new Vector3(x, y, 0), Quaternion.identity);
        NetworkServer.Spawn(placeHolder);
    }
}
