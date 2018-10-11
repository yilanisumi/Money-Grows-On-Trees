using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PrefabBlockPhysics : MonoBehaviour
{
    public bool thrown = false;
    Rigidbody rb;
    public float gravityMultiplier = 1f;
    float timer = 0;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (Vector3.Magnitude(rb.velocity) > 0.001f)
        {
            timer = 0;
        }

        //Debug.Log(rb.velocity);
       // Debug.Log(timer);
        if (thrown)
        {
            rb.AddForce(Physics.gravity * rb.mass * gravityMultiplier);
            timer += Time.deltaTime;
            if (timer > 5)
            {
                gameObject.SetActive(false);
                GameObject.Find("World").GetComponent<World>().SetBlock(EditTerrain.GetBlockPos(transform.position).x, EditTerrain.GetBlockPos(transform.position).y, EditTerrain.GetBlockPos(transform.position).z, CreateBlock());
            }
        }

    }
    public void Reset()
    {
        thrown = false;
        timer = 0;
    }
    Block CreateBlock()
    {
        string blockType = GetComponent<PrefabBlockTexture>().blockType;
        Block block;

        switch (blockType)
        {
            case "Stone":
                block = new Block();
                break;
            case "Grass":
                block = new BlockGrass();
                break;
            case "BrownWood":
                block = new BlockBrownWood();
                break;
            case "Lava":
                block = new BlockLava();
                break;
            case "GreenLeaves":
                block = new BlockGreenLeaves();
                break;
            case "OrangeLeaves":
                block = new BlockOrangeLeaves();
                break;
            case "StoneSnow":
                block = new BlockStoneSnow();
                break;
            case "Ice":
                block = new BlockIce();
                break;
            case "Glass":
                block = new BlockGlass();
                break;
            case "WhiteWood":
                block = new BlockWhiteWood();
                break;
            case "Apple":
                block = new BlockApple();
                break;
            case "Alphabet":
                block = new BlockAlphabet();
                break;
            case "Amazon":
                block = new BlockAmazon();
                break;
            case "Facebook":
                block = new BlockFacebook();
                break;
            case "IBM":
                block = new BlockIBM();
                break;
            case "Intel":
                block = new BlockIntel();
                break;
            case "Microsoft":
                block = new BlockMicrosoft();
                break;
            case "Netflix":
                block = new BlockNetflix();
                break;
            case "Twitter":
                block = new BlockTwitter();
                break;
            case "Yahoo":
                block = new BlockYahoo();
                break;
            
            default:
                block = new BlockAir();
                break;
        }
        block.upsideDown = GetComponent<PrefabBlockTexture>().upsideDown;
        return block;
    }
}
