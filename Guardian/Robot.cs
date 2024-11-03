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
        public float _timeReachingHatch = 420;
        public Transform _coreSunSector;

        float _currentTimeReachingHatch;
        GameObject _sphere;
        public GameObject _robotLocal;

        public void Initialize() {
            _dummyInitialPos.position = _hatchCylinder.position + _hatchCylinder.forward * _initialDistance;
            transform.position = _dummyInitialPos.position;
            transform.LookAt(_hatchCylinder);

            _sphere = transform.Find("Sector/robot/HiddenObjs/smooth_sphere").gameObject;
            _sphere.SetActive(true);
            _robotLocal = transform.Find("Sector/robot").gameObject;
        }

        void Update() {
            if(!_hatchCylinder) {
                return;
            }
            transform.LookAt(_hatchCylinder);
            //transform.position += transform.forward * _speed * Time.deltaTime;
            _currentTimeReachingHatch += Time.deltaTime;
            transform.position = Vector3.Lerp(_dummyInitialPos.position, _hatchCylinder.position, Mathf.Min(_currentTimeReachingHatch / _timeReachingHatch, 1));

            if(_currentTimeReachingHatch >= _timeReachingHatch) {
                _robotLocal.transform.parent = _coreSunSector;
                _robotLocal.AddComponent<RobotLocal>()._points = new List<Transform> {
                    _coreSunSector.Find("SunCoreStructure/WaitingArea/RobotMovePoint (4)"),
                    _coreSunSector.Find("SunCoreStructure/WaitingArea/RobotMovePoint (3)"),
                    _coreSunSector.Find("SunCoreStructure/WaitingArea/RobotMovePoint (2)"),
                    _coreSunSector.Find("SunCoreStructure/WaitingArea/RobotMovePoint (1)"),
                    _coreSunSector.Find("SunCoreStructure/WaitingArea/RobotMovePoint (5)"),
                    _coreSunSector.Find("SunCoreStructure/WaitingArea/RobotMovePoint"),
                };
                gameObject.SetActive(false);
            }
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
