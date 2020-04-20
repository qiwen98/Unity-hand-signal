using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    bool collided = false;
    protected Vector3 m_grabbedObjectPosOff;
    protected Quaternion m_grabbedObjectRotOff;
    float speed = 3f;
    float lerprate = 40f;
    [SerializeField]
    public GameObject handcollider;
    Vector3 currentpos;
    Vector3 targetpos;
    Vector3 offset = new Vector3(0, 1, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (collided)
        {
            MoveObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
        //if collide with the hand collider 
        if (other.gameObject.name == "coll_hands:b_l_index3")
        {
            //stop this object collider 
            this.GetComponent<SphereCollider>().isTrigger = false;
           // this.GetComponentInParent<SphereCollider>().isTrigger = false;
            //indicate it is collided and stop the collision
            collided = true;
            Debug.Log("stop detect collision");
            this.GetComponentInParent<Rigidbody>().isKinematic = true;
            this.GetComponentInParent<Rigidbody>().useGravity= false;
            

        }
       
    }

    void MoveObject()
    {
        Debug.Log("object is moving");
        Debug.Log(this.transform.parent.position);
         currentpos = this.transform.parent.position;
        targetpos = handcollider.transform.position;
        currentpos = Vector3.LerpUnclamped(currentpos, targetpos, lerprate * Time.deltaTime);
        this.transform.parent.position = currentpos;
    }

  
}
