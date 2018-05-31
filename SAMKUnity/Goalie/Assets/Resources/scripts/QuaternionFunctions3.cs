using UnityEngine;
using System.Collections;

public class QuaternionFunctions3 : MonoBehaviour
{

    public int halfRotations, oldRotations;
    private double angle, x, y, z;
    public float W, X, Y, Z;
    public bool wait180, wait1G;
    private RotationUpdated mCallback;

    // Use this for initialization
    void Start()
    {

    }

    //This function normalizes the quaternion
    public Quaternion normalize(float angW, float angX, float angY, float angZ)
    {
        Quaternion lQuaternion;
        float norme = (float)Mathf.Sqrt(angW * angW + angX * angX + angY * angY + angZ * angZ);
        if (norme == 0.0)
        {
            angW = 1.0f;
            angX = angY = angZ = 0.0f;
        }
        else
        {
            float recip = 1.0f / norme;

            angX *= recip;
            angY *= recip;
            angZ *= recip;
            angW *= recip;
        }
        lQuaternion = new Quaternion(angW, angX, angY, angZ);
        return lQuaternion;

    }

    public Quaternion reciprical(Quaternion aQuaternion)
    {
        float norme = (float)Mathf.Sqrt(W * W + X * X + Y * Y + Z * Z);
        if (norme == 0.0)
            norme = 1.0f;

        float recip = 1.0f / norme;

        W = W * recip;
        X = -X * recip;
        Y = -Y * recip;
        Z = -Z * recip;
        aQuaternion = new Quaternion(W, X, Y, Z);
        return aQuaternion;
    }

    public Quaternion mult(Quaternion q, Quaternion qHelper)
    {
        float w = qHelper.w * q.w - (qHelper.x * q.x + qHelper.y * q.y + qHelper.z * q.z);

        float x = qHelper.w * q.x + q.w * qHelper.x + qHelper.y * q.z - qHelper.z * q.y;
        float y = qHelper.w * q.y + q.w * qHelper.y + qHelper.z * q.x - qHelper.x * q.z;
        float z = qHelper.w * q.z + q.w * qHelper.z + qHelper.x * q.y - qHelper.y * q.x;
        //convert to lowercase for C#(?)
        W = w;
        X = x;
        Y = y;
        Z = z;
        Quaternion qq = new Quaternion(W, X, Y, Z);
        return qq;
    }

    public float getQuaternionAngle(Quaternion q1, Quaternion q2)
    {

        Quaternion inv = reciprical(q1);
        Quaternion res = mult(inv, q2);
        //Quaternion res = inv.mult(q2);
        return (float)(Mathf.Acos(res.w) * 2.0F * (float)(180.0 / Mathf.PI));
    }

    public void countRotations_Premius(float angle)
    {

        //Log.i(TAG, "Angle: " + angle);

        if (wait180)
        {
            if (angle > 160 && angle < 200)
            {
                halfRotations += 1;
                wait180 = false;
                //mLockQuaternion = mQuaternion;
            }
        }
        else if (angle < 20)
        {
            halfRotations += 1;
            wait180 = true;
        }
        else if (angle > 330)
        {
            halfRotations += 1;
            wait180 = true;
        }


        // TODO: tähänkin muuttuja joka lasketaan halutun matkan mukaan
        if (halfRotations > (oldRotations + 5))
        {
            //Log.d(TAG, "Half Rot: " + halfRotations);
            oldRotations = halfRotations;
            mCallback.sendRotation(halfRotations);
        }
        //ShoulderR.transform.Rotate(Vector3.zero, angle);

    }
    public interface RotationUpdated { void sendRotation(int halfRotation); }

    public void set(Quaternion q1)
    {
        if (q1.w > 1) normalize(q1.w, q1.x, q1.y, q1.z);
        angle = 2 * Mathf.Acos(q1.w);
        double s = Mathf.Sqrt(1 - q1.w * q1.w);
        if (s < 0.001)
        {
            x = q1.x;
            y = q1.y;
            z = q1.z;
        }
        else
        {
            x = q1.x / s;
            y = q1.y / s;
            z = q1.z / s;
        }
    }
}
