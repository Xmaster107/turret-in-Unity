using UnityEngine;
public class Turret : MonoBehaviour
{
    public GameObject player;
    public GameObject raycast;
    public GameObject hinge;
    public float rayLength = 10f;
    public bool raytrigger = false;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(raycast.transform.position, raycast.transform.forward * rayLength);
    }
    void Update()
    {
        player = GameObject.Find("Player");
        Ray ray = new Ray(raycast.transform.position, raycast.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider.gameObject == player)
            {
                raytrigger = true;
            }
            else
            {
                raytrigger = false;
            }
        }

        float distance = Mathf.Sqrt(
    Mathf.Pow(player.transform.position.x - transform.position.x, 2) +
    Mathf.Pow(player.transform.position.y - transform.position.y, 2) +
    Mathf.Pow(player.transform.position.z - transform.position.z, 2));
        raycast.transform.LookAt(player.transform);
        if (distance < 10f)
        {
            if (raytrigger)
            {
                hinge.transform.LookAt(player.transform);
                Debug.Log("shoot");

            }
        }
    }
}