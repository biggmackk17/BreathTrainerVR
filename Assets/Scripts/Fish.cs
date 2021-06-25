using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
   public float _speed=2;
    float _rotationSpeed =2f;
    float _detectRadius = 5;
    float _minDistance =.5f;
    Vector3 _avgHeading;
    Vector3 _avgPosition;
    Vector3 _avgAvoidence;
    float _distanceFromOrigin = 10;
    WaitForSeconds wait;
   // Vector3 _avgVelocity;


    //rules of boids. 
    // Separation - steer to avoid crowding local flockmates DONE
    //alignment steer towards the average heading of local flockmates
    // cohesion, steer to move toards the average position of local flockmates
    private void OnEnable()
    {
        _speed = Random.Range(1, 3f);
        wait = new WaitForSeconds(.05f);
    }


    private void Start()
    {
        StartCoroutine(Flocking());
    }

    // Start is called before the first frame update
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);

      
    }


    IEnumerator Flocking()
    {
        while (true)
        {
          
            var _detected = Physics.OverlapSphere(transform.position, _detectRadius);
            _avgHeading = Vector3.zero;


            for (int i = 0; i < _detected.Length; i++)
            {
                if (_detected[i].transform != transform)
                {
                    #region Separation
                    var distance = Vector3.Distance(transform.position, _detected[i].ClosestPointOnBounds(transform.position));
                    if (distance < _minDistance)
                    {

                        //direction = (transform.position - _detected[i].transform.position).normalized;
                        //direction = (transform.position - _detected[i].transform.position).normalized;

                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - _detected[i].ClosestPointOnBounds(transform.position)), _rotationSpeed * 4 * Time.deltaTime);
                        // transform.rotation = Quaternion.LookRotation(transform.position - _detected[i].transform.position);

                    }
                    #endregion
                    else
                    {
                        _avgHeading += _detected[i].transform.forward;
                        _avgPosition += _detected[i].transform.position;
                    }




                }
            }
            //calculate averages
            if (_avgHeading != Vector3.zero && _detected.Length != 0)

            {

                _avgHeading = _avgHeading / (_detected.Length - 1);
                _avgPosition = _avgPosition / (_detected.Length - 1);

                #region Alignment

                if (Random.Range(0, 20) == 1)
                {

                    if (_avgHeading != Vector3.zero)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_avgHeading), _rotationSpeed * Time.deltaTime);
                    }


                    #endregion

                    #region Cohesion
                    var postionDir = _avgPosition - transform.position;
                    if (postionDir != Vector3.zero)
                    {
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(postionDir), _rotationSpeed * Time.deltaTime);
                    }

                }
                #endregion
            }


            if (Vector3.Distance(transform.position, Vector3.zero) > _distanceFromOrigin)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.zero - transform.position), _rotationSpeed * Time.deltaTime);
            }

            if (transform.position.y < 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.up), _rotationSpeed * Time.deltaTime);
            }


            yield return wait;
        }
    }


}
