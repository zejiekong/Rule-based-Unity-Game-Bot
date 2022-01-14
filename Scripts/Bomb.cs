using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        Respawn();
    }
    void Update()
    {
        if (transform.position.y < -10)
        {
            Respawn();
        }
    }
    private void Respawn()
    {
        var horizontal = UnityEngine.Random.Range(-10, 10);
        var vertical = UnityEngine.Random.Range(12, 15);
        transform.position = new Vector3(horizontal, vertical);
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
    }
}
