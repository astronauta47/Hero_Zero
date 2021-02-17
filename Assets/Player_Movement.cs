using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour
{

    [SerializeField] float speed;
    bool isGrabbing;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Grab();
        else if (Input.GetMouseButtonUp(0))
            UnGrab();

        transform.position += Vector3.up * speed * Time.deltaTime * Input.GetAxis("Vertical");
        transform.position += Vector3.right * speed * Time.deltaTime * Input.GetAxis("Horizontal");

        

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rSpeed = Time.deltaTime * 300;

        if (isGrabbing)
            rSpeed /= 2;

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), rSpeed);
    }

    void Grab()
    {
        
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, transform.up, .5f);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Number"))
            {
                isGrabbing = true;
                hit[i].collider.transform.SetParent(transform);
            }
        }

    }

    void UnGrab()
    {
        if (isGrabbing)
            isGrabbing = false;
        else return;

        transform.GetChild(2).SetParent(null);
    }
}