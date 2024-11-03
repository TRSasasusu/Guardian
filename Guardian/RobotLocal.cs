using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Guardian {
    public class RobotLocal : MonoBehaviour {
        public List<Transform> _points;

        float _speed = 50;
        int _index;
        Transform _sphere;

        void Start() {
            if (_points != null) {
                transform.position = _points[0].position;
            }
        }

        void Update() {
            if(_sphere) {
                _sphere.position += (transform.parent.position - _sphere.position).normalized * Time.deltaTime * _speed;
                if(Vector3.Distance(_sphere.position, transform.parent.position) < 1) {
                    _sphere.gameObject.SetActive(false);
                    _sphere = null;
                }
            }

            if(_points == null || _points.Count <= _index) {
                return;
            }

            transform.rotation = _points[_index].rotation;
            transform.position += (_points[_index].position - transform.position).normalized * Time.deltaTime * _speed;
            if(Vector3.Distance(transform.position, _points[_index].position) < 1) {
                _index++;
                if(_index == 2) {
                    _sphere = transform.Find("HiddenObjs/smooth_sphere");
                    _sphere.transform.parent = transform.parent;
                }
            }
        }
    }
}
