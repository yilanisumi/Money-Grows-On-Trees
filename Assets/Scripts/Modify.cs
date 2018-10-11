using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using Leap;
using Leap.Unity;
public class Modify : MonoBehaviour
{
    Vector2 rot;
    public float speed = 2.5f;
    const float destroyDist = 4;
    //GameObject throwBlock;
    public GameObject particles;
    float timer = 0;
    public bool pinch = false;
    public RaycastHit hit;
    StockInventory inventory;
    float pinchTimer = 1;
    float pinchWait = 2;
    ///GetLeapFingers leapFinger;
    void Start()
    {
        inventory = GameObject.Find("StockDatabase").GetComponent<StockInventory>();
      //    leapFinger = GameObject.Find("RigidRoundHand_R").GetComponent<GetLeapFingers>();
    //    leapFinger = null;
       // throwBlock = null;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
      //  if (Input.GetMouseButtonDown(0))
     // if(leapFinger != null && leapFinger.Pinch && throwBlock == null)
         //if(pinch && throwBlock == null)
         if(pinch && timer > pinchTimer)
            {
            //   RaycastHit hit = leapFinger.GetRaycast();
           // RaycastHit hit;
            //Vector3 fwd = transform.TransformDirection(Vector3.forward);
          //  if (Physics.Raycast(transform.position, fwd, out hit, destroyDist) && timer > 0.1)
         //   {
                if (hit.collider.name.Contains("Chunk"))
                {
                
                    timer = 0;
                    Block block = EditTerrain.GetBlock(hit);
                    string blockName = block.ToString();
                    if(blockName != "Air Block")
                    {
                      //Debug.Log(block.ToString());
                        string firstWord = blockName.IndexOf(" ") > -1
                        ? blockName.Substring(0, blockName.IndexOf(" "))
                        : blockName;
                        if(firstWord == "Stock")
                        {
                        Debug.Log(blockName);

                            blockName = blockName.Substring(blockName.IndexOf(" ") + 1);
                            string companyName = blockName.Substring(0,blockName.IndexOf(" "));
                        Debug.Log(companyName);
                        if (!block.selling)
                        {
                            if (!inventory.AddToInventory(companyName))
                            {
                                return;
                            }
                        }
                        else
                        {
                            inventory.RemoveFromInventory(companyName);
                        }
                     
                    }
                    else
                    {
                        return;
                    }
                        //create block to hold
                //        throwBlock = (GameObject)Instantiate(Resources.Load(blockName), transform.position + transform.forward * 2 + transform.up * 0.5f, Quaternion.identity);
                //        particles.SetActive(true);
                        //float gravityMultiplier = GameObject.Find("Player").GetComponent<FirstPersonController>().m_GravityMultiplier;
                      //  throwBlock.GetComponent<PrefabBlockPhysics>().gravityMultiplier = gravityMultiplier;
                      //  if (gravityMultiplier < 0)
                     //   {
                      //      throwBlock.GetComponent<PrefabBlockTexture>().upsideDown = true;
                      //  }                          
                        
                        //delete block
                        EditTerrain.SetBlock(hit, new BlockAir());

                    }                 

                }
            //    else if (hit.collider.name.Contains("Block"))
            //    {

               //     throwBlock = (GameObject)Instantiate(hit.collider.gameObject, transform.position + transform.forward * 2, Quaternion.identity);
               //     throwBlock.GetComponent<PrefabBlockPhysics>().Reset();

              //        if (hit.collider.gameObject.GetComponent<PrefabBlockPhysics>().gravityMultiplier < 0)
             //        {
              //           throwBlock.GetComponent<PrefabBlockTexture>().upsideDown = true;
              //        }
             //       Destroy(hit.collider.gameObject);
            //    }


           //// }


        }
     //   if (throwBlock != null && !throwBlock.GetComponent<PrefabBlockPhysics>().thrown)
     //   {
     //       throwBlock.transform.position = Vector3.MoveTowards(throwBlock.transform.position, transform.position + transform.forward * 2f + transform.up * 0.5f, 10 * Time.deltaTime);
     //       particles.transform.position = throwBlock.transform.position;
    //    }
        //if (Input.GetMouseButtonDown(1))
      //  if(pinch == false)
      //  {
         //   if (throwBlock != null && !throwBlock.GetComponent<PrefabBlockPhysics>().thrown)
         //   {
         //       throwBlock.GetComponent<PrefabBlockPhysics>().thrown = true;
        //        Rigidbody rb = throwBlock.GetComponent<Rigidbody>();
         //       if (rb != null)
         //       {
                    // rb.useGravity = true;
       //             rb.isKinematic = false;
        //            rb.AddForce(transform.forward * 1000 + transform.up * 500);
      //              particles.SetActive(false);
       //         }
      //          throwBlock = null;
      //      }
           
       // }
        //moving controls
        /*
         * 
        rot = new Vector2(rot.x + Input.GetAxis("Mouse X") * speed, rot.y + Input.GetAxis("Mouse Y") * speed);

        transform.localRotation = Quaternion.AngleAxis(rot.x, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rot.y, Vector3.left);

        transform.position += transform.forward * speed * Input.GetAxis("Vertical");
        transform.position += transform.right * speed * Input.GetAxis("Horizontal");
         * */
    }

}

