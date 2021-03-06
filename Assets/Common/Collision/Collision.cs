using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Collision
{
    public static void CreateContacts(List<Body> bodies, out List<Contact> contacts)
    {
        contacts = new List<Contact>();

        for (int i = 0; i < bodies.Count - 1; i++)
        {
            for (int j = i + 1; j < bodies.Count; j++)
            {
                Body bodyA = bodies[i];
                Body bodyB = bodies[j];

                if(bodyA.type == Body.eType.Static && bodyB.type == Body.eType.Static)
                {
                    continue;
                }

                Circle circleA = new Circle(bodyA.position, ((CircleShape)bodyA.shape).radius);
                Circle circleB = new Circle(bodyB.position, ((CircleShape)bodyB.shape).radius);

                if (circleA.Contains(circleB))
                {
                    Contact contact = new Contact() { bodyA = bodyA, bodyB = bodyB };                    
                    float distance = (circleA.center - circleB.center).magnitude;
                    contact.depth = circleA.radius + circleB.radius - distance;
                    contact.normal = (circleA.center - circleB.center).normalized;
                    contacts.Add(contact);
                }
            }
        }
    }

    
}
