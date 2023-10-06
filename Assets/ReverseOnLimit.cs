using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReverseOnLimit : MonoBehaviour
{
    HingeJoint2D m_Joint;
    JointMotor2D m_motor;

    void Start()
    {
        m_Joint = GetComponent<HingeJoint2D>();
        m_motor = m_Joint.motor;
    }

    // Updates with the Physics
    void FixedUpdate()
    {
        if (m_Joint.jointAngle >= 5 || m_Joint.jointAngle <= -5)
        {
            m_motor.motorSpeed *= -1;
            m_Joint.motor = m_motor;
        }
    }
}
