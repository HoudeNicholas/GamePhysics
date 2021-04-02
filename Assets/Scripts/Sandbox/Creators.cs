using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creators : MonoBehaviour
{
    public GameObject original;
    public float speed = 100;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject gameobject = Instantiate(original, position, Quaternion.identity);

            if (gameobject.TryGetComponent<Body>(out Body body))
            {
                Vector2 force = Random.insideUnitSphere.normalized * speed;

                body.AddForce(force);
                World.Instance.bodies.Add(body);
            }
        }
    }
}
