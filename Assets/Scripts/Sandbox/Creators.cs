using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creators : Action
{
    public GameObject original;
    public FloatData speed;
    public FloatData damping;
    public FloatData density;
    public FloatData size;
    public FloatData restitution;
    bool action { get; set; } = false;
    bool oneTime;
    public BodyEnumData bodyType;

    public override void StartAction()
    {
        action = true;
        oneTime = true;
    }

    public override void StopAction()
    {
        action = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (action && (oneTime || Input.GetKey(KeyCode.LeftControl)))
        {
            oneTime = false;
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject gameobject = Instantiate(original, position, Quaternion.identity);


            if (gameobject.TryGetComponent<Body>(out Body body))
            {                
                body.shape.size = size;
                body.damping = damping;
                body.shape.density = density;
                body.restitution = restitution;
                body.type = (Body.eType)bodyType.value;
                Vector2 force = Random.insideUnitSphere.normalized * speed;
                body.AddForce(force, Body.eForceMode.Velocity);
                World.Instance.bodies.Add(body);
            }
        }
    }
}
