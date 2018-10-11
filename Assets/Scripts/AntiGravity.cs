using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravity : MonoBehaviour
{
    GameObject player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        PrefabBlockPhysics physics = other.GetComponent<PrefabBlockPhysics>();
        PrefabBlockTexture texture = other.GetComponent<PrefabBlockTexture>();
        if (physics != null && texture != null)
        {
            physics.gravityMultiplier *= -1;
            if (physics.gravityMultiplier > 0)
            {
                texture.upsideDown = false;
            }
            else
            {
                texture.upsideDown = true;
            }

        }

    }

}
