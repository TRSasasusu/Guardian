using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Guardian {
    public class Robot : MonoBehaviour {
        public Transform _dummyInitialPos;
        public Transform _hatchCylinder;
        public float _initialDistance = 20000;
        //public float _speed = 2;
        public float _timeReachingHatch = 300;

        float _currentTimeReachingHatch;
        GameObject _sphere;

        public void Initialize() {
            _dummyInitialPos.position = _hatchCylinder.position + _hatchCylinder.forward * _initialDistance;
            transform.position = _dummyInitialPos.position;
            transform.LookAt(_hatchCylinder);

            _sphere = transform.Find("Sector/robot/HiddenObjs/smooth_sphere").gameObject;
            _sphere.SetActive(true);
        }

        void Update() {
            if(!_hatchCylinder) {
                return;
            }
            transform.LookAt(_hatchCylinder);
            //transform.position += transform.forward * _speed * Time.deltaTime;
            _currentTimeReachingHatch += Time.deltaTime;
            transform.position = Vector3.Lerp(_dummyInitialPos.position, _hatchCylinder.position, Mathf.Min(_currentTimeReachingHatch / _timeReachingHatch, 1));
        }

        void LateUpdate() {
            if(!_hatchCylinder) {
                return;
            }
            transform.LookAt(_hatchCylinder);
            transform.position = Vector3.Lerp(_dummyInitialPos.position, _hatchCylinder.position, Mathf.Min(_currentTimeReachingHatch / _timeReachingHatch, 1));
        }

        void FixedUpdate() {
            if(!_hatchCylinder) {
                return;
            }
            transform.LookAt(_hatchCylinder);
            transform.position = Vector3.Lerp(_dummyInitialPos.position, _hatchCylinder.position, Mathf.Min(_currentTimeReachingHatch / _timeReachingHatch, 1));
        }
    }
}
