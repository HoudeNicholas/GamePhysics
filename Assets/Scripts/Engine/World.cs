using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

    public BoolData simulate;
    public FloatData gravity;
    public StringData fpsText;
    public FloatData fixedFPS;
    public FloatData gravitation;
    public float timeAccumulator = 0.0f;
    public BoolData collision;
    public BoolData Wrap;
    private Vector2 size;

    static World instance;
    public static World Instance { get { return instance; } }
    public float fixedDeltaTime { get { return 1.0f / fixedFPS.value; } }
    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    public List<Body> bodies { get; set; } = new List<Body>();
    public List<Spring> springs { get; set; } = new List<Spring>();

    void Awake()
    {
        size = Camera.main.ViewportToWorldPoint(Vector2.one);
        instance = this;
    }
    void Update()
    {
        float dt = Time.deltaTime;
        float fps = (1.0f / dt);
        if (!simulate.value) return;
        timeAccumulator += dt;

        int rand = Random.Range(0, 4);

        //if (rand > 2)
        //{
        //    bodies.ForEach(body => body.shape.color = Color.white);
        //} else if (rand > 1)
        //{
        //    bodies.ForEach(body => body.shape.color = Color.blue);
        //} else
        //{
        //    bodies.ForEach(body => body.shape.color = Color.red);
        //}

        fpsText.value = $"FPS: {fps}";

        if (!simulate.value) return;

        GravitationalForce.ApplyForce(bodies, gravitation.value);
        springs.ForEach(spring => spring.ApplyForce());
        //bodies.ForEach(body => Integrator.SemiImplicitEuler(body, dt));

        while (timeAccumulator >= fixedDeltaTime) 
        { 
            bodies.ForEach(body => body.Step(fixedDeltaTime));
            bodies.ForEach(body => Integrator.ExplicitEuler(body, fixedDeltaTime));

            bodies.ForEach(body => body.shape.color = Color.white);

            if (collision)
            {
                Collision.CreateContacts(bodies, out List<Contact> contacts);
                contacts.ForEach(contact => { if (rand > 2) { contact.bodyA.shape.color = Color.red; contact.bodyB.shape.color = Color.red; } else if (rand > 1) { contact.bodyA.shape.color = Color.white; contact.bodyB.shape.color = Color.white; } else { contact.bodyA.shape.color = Color.blue; contact.bodyB.shape.color = Color.blue; } });

                ContactSolver.Resolve(contacts);
            }

            timeAccumulator = timeAccumulator - fixedDeltaTime; 
        }

        if (Wrap)
        {
            bodies.ForEach(body => body.position = Utilities.Wrap(body.position, -size, size));
        }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);

    }
}
