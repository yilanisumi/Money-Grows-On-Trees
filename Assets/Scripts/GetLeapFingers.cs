using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;

public class GetLeapFingers : MonoBehaviour
{
    public GameObject text;
    HandModel hand_model;
    Hand leap_hand;
    bool isLeft;
    GameObject cam;
    public bool RotateLeft = false;
    public bool RotateRight = false;
    public bool MoveForward = false;
    public bool Jump = false;
    public bool Pinch = false;
    public RaycastHit hit;
    FingerModel finger0;
    FingerModel finger1;
    FingerModel finger2;
    Modify modify;
    public LineRenderer lineRenderer;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 5f;

    /*
     public float laserWidth = 0.1f;
     public float laserMaxLength = 5f;
 
     void Start() {
         Vector3[] initLaserPositions = new Vector3[ 2 ] { Vector3.zero, Vector3.zero };
         laserLineRenderer.SetPositions( initLaserPositions );
         laserLineRenderer.SetWidth( laserWidth, laserWidth );
     }
 
     void Update() 
     {
         if( Input.GetKeyDown( KeyCode.Space ) ) {
             ShootLaserFromTargetPosition( transform.position, Vector3.forward, laserMaxLength );
             laserLineRenderer.enabled = true;
         }
         else {
             laserLineRenderer.enabled = false;
         }
     }
 

    */
    void Start()
    {
        GameObject gameObj = GameObject.Find("OVRCameraRig");
        if(gameObj != null)
        {
            modify = gameObj.GetComponent<Modify>();
        }

        cam = GameObject.Find("CenterEyeAnchor");
        if (this.name.Contains("L"))
        {
            isLeft = true;
        }
        else
        {
            isLeft = false;
        }
        hand_model = GetComponent<HandModel>();
        leap_hand = hand_model.GetLeapHand();
        finger0 = hand_model.fingers[0];
        finger1 = hand_model.fingers[1];
        finger2 = hand_model.fingers[2];
        if (leap_hand == null) Debug.LogError("No leap_hand founded");
        if (!isLeft)
        {
            lineRenderer = GetComponent<LineRenderer>();
          //  Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
          //  lineRenderer.SetPositions(initLaserPositions);
            //lineRenderer.SetWidth(laserWidth, laserWidth);
            lineRenderer.enabled = true;
        }

        ClearDirection();
    }
    void ShootFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
        }

        lineRenderer.SetPosition(0, targetPosition);
        lineRenderer.SetPosition(1, endPosition);
    }
    int GetDirection(FingerModel finger0)
    {
        float AngleUp = Vector3.Angle(finger0.GetRay().direction, cam.transform.up);
        float AngleLeft = Vector3.Angle(finger0.GetRay().direction, cam.transform.right * -1);
        float AngleRight = Vector3.Angle(finger0.GetRay().direction, cam.transform.right);
       // Debug.Log("UP: " + AngleUp);
      //  Debug.Log("Left: " + AngleLeft);
      //  Debug.Log("Right: " + AngleRight);

        if (AngleLeft < AngleUp && AngleLeft < AngleRight)
        {
            return -1;
        }
       else if(AngleUp < AngleLeft && AngleUp < AngleRight)
        {
            return 0;
        }
        else
        {
            return 1;
        }
      
    }
    void Update()
    {
        ClearDirection();
        //for (int i = 0; i < HandModel.NUM_FINGERS; i++)
        //{
        Transform p = hand_model.palm;
        //finger0 = hand_model.fingers[0];
        //finger = hand_model.fingers[1];
        // draw ray from finger tips (enable Gizmos in Game window to see)
        Debug.DrawRay(finger0.GetTipPosition(), finger0.GetRay().direction, Color.red);
        
        Debug.DrawRay(p.position, p.transform.up, Color.red);
        //Debug.Log(p.transform.up);
        //Debug.Log(finger.GetTipPosition());
        //Debug.Log(Vector3.Distance(finger0.GetTipPosition(), finger.GetTipPosition()));
       

        int direction = GetDirection(finger0);

      //  if (palmx > -0.2f && palmx < 0.2f && palmy < -0.8f && isLeft)
     //   {
     //       Debug.Log("Left");
     //   }
    //    if(palmx > -0.2f && palmx < 0.2f && palmy > 0.8f && isLeft)
    //    {
    //        Debug.Log("Right");
   //     }
        bool flag = true;
        bool forward = true;
        bool error = false;
        for(int i = 1; i < 5; i++)
        {
            FingerModel f = hand_model.fingers[i];
            if(Vector3.Distance(f.GetTipPosition(), p.position) > 0.15)
            {
                if (!error)
                {
                    error = true;
                }
                else
                {
                    flag = false;
                    break;
                }
               
            }
        }
        for (int i = 1; i < 5; i++)
        {
            FingerModel f = hand_model.fingers[i];
            if(i == 1)
            {
                if (Vector3.Distance(f.GetTipPosition(), p.position) < 0.2)
                {
                    forward = false;
                    break;
                }
            }
            else
            {
                if (Vector3.Distance(f.GetTipPosition(), p.position) > 0.2)
                {
                    forward = false;
                    break;
                }
            }
           
        }
        ClearDirection();
        if (isLeft)
        {
           
            if (forward)
            {
                //           Debug.Log("forward");
                MoveForward = true;
                if(direction == 0)
                {
                    Jump = true;
                }
            }
            else if (flag)
            {
                if (direction == -1)
                {
                    RotateLeft = true;
       //             Debug.Log("Left");
                }
                else if (direction == 0)
                {
                    Jump = true;
         //           Debug.Log("Jump");
                }
                else
                {
                    RotateRight = true;
      //              Debug.Log("Right");
                }
             //   Debug.Log("Forward");
             //   float fx = finger0.GetRay().direction.x;
             //   float fy = finger0.GetRay().direction.y;
                //Debug.Log(finger0.GetRay().direction);
               // if (fx > -0.2f && fx < 0.2f && fy > 0.8f)
               // {
                //    Debug.Log("Jump");
               // }
            }
            else
            {
          //      Debug.Log("Neutral");
            }
        }
       
        

        if (Vector3.Distance(finger0.GetTipPosition(), finger2.GetTipPosition()) < 0.02 && !isLeft)
        {
           
            Pinch = true;
            GetRaycast();


        }
        else
        {
            Pinch = false;
            if (modify != null)
            {
                modify.pinch = Pinch;
            }
        }
        if (!isLeft)
        {
            ShootFromTargetPosition(finger1.GetTipPosition(), finger1.GetRay().direction, laserMaxLength);
        }
        
       
        //}
    }
    public RaycastHit GetRaycast()
    {
        RaycastHit hit;
        Vector3 fwd = finger1.GetRay().direction;
        if (Physics.Raycast(finger1.GetTipPosition(), fwd, out hit, 5f))
        {
            Debug.Log(hit.collider.name);
            if (modify != null)
            {
                modify.pinch = true;
                modify.hit = hit;
            }
        }
        else
        {
            if (modify != null)
            {
                modify.pinch = false;
            }
            Debug.Log("Miss");
            
        }
        return hit;
    }

    void ClearDirection()
    {
        MoveForward = false;
        Jump = false;
        RotateLeft = false;
        RotateRight = false;
    }
}