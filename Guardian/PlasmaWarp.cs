using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using IEnumerator = System.Collections.IEnumerator;

namespace Guardian {
    public class PlasmaWarp : MonoBehaviour {
        List<Transform> _points;
        List<bool> _warpIndices;
        List<ParticleSystem> _beams;
        List<GameObject> _disabledObjs;
        List<GameObject> _enabledObjs;
        Transform _core;
        float _speed;

        public bool _inactive { get; private set; }

        public void Initialize(IEnumerable<Transform> points, IEnumerable<int> warpIndices = null, IEnumerable<GameObject> disabledObjs = null, IEnumerable<GameObject> enabledObjs = null) {
            _core = transform.Find("Core");

            _points = points.ToList();
            if(warpIndices != null) {
                _warpIndices = _points.Select((x, i) => warpIndices.Contains(i)).ToList();
            }

            if(disabledObjs != null) {
                _disabledObjs = disabledObjs.ToList();
            }
            if(enabledObjs != null) {
                _enabledObjs = enabledObjs.ToList();
            }

            _beams = new List<ParticleSystem>();
            var plasmaBeam = transform.Find("PlasmaBeam").gameObject;
            _beams.Add(plasmaBeam.GetComponent<ParticleSystem>());
            _speed = plasmaBeam.GetComponent<ParticleSystem>().main.startSpeed.constant;
            for(var i = 1; i < _points.Count; i++) {
                var copyPlasmaBeam = Instantiate(plasmaBeam, transform);
                _beams.Add(copyPlasmaBeam.GetComponent<ParticleSystem>());
            }

            if(transform.Find("LowCore")) {
                foreach(Transform child in transform) {
                    if(child.name == "Core" || child.name.Contains("PlasmaBeam")) {
                        child.gameObject.SetActive(false);
                    }
                }
                _inactive = true;
            }
        }

        void Update() {
            for(var i = 0; i < _beams.Count; i++) {
                if(i > 0) {
                    _beams[i].transform.position = _points[i - 1].transform.position;
                }

                if (_warpIndices != null && _warpIndices[i]) {
                    continue;
                }
                _beams[i].transform.LookAt(_points[i]);
                var mainModule = _beams[i].main;
                mainModule.startLifetime = Vector3.Distance(_beams[i].transform.position, _points[i].transform.position) / _speed;
            }
        }

        void OnTriggerEnter(Collider other) {
            if(_inactive) {
                if(SphereItem.PickedUpSphereItem) {
                    foreach(Transform child in transform) {
                        if(child.name == "Core" || child.name.Contains("PlasmaBeam")) {
                            child.gameObject.SetActive(true);
                        }
                        if(child.name == "LowCore") {
                            child.gameObject.SetActive(false);
                        }
                    }
                    _inactive = false;
                    SphereItem.PickedUpSphereItem.Remove(gameObject.GetComponentInParent<Sector>());
                }
                else {
                    return;
                }
            }
            if(other.gameObject == Locator._playerBody.gameObject) {
                Guardian.Log("player enters plasmawarp");
                StartCoroutine(MovePlayer(other.transform));
            }
        }

        Vector3 PointVelocity(Transform point) {
            var parentOWRigidbody = point.GetComponentInParent<OWRigidbody>();
            return parentOWRigidbody.GetVelocity() + parentOWRigidbody.GetPointTangentialVelocity(point.position);
        }

        IEnumerator MovePlayer(Transform other) {
            Patch._canBeDamaged = false;
            GUIMode.SetRenderMode(GUIMode.RenderMode.Hidden);
            other.GetComponent<Collider>().enabled = false;
            other.Find("Traveller_HEA_Player_v2").gameObject.SetActive(false);

            if(_disabledObjs != null) {
                foreach (var obj in _disabledObjs) {
                    obj.SetActive(false);
                }
            }

            var owRigidbody = Locator._playerBody;
            //var prevRotation = transform.rotation;
            Guardian.Log("start player moving");
            for(var i = 0; i < _points.Count; i++) {
                if (_warpIndices != null && _warpIndices[i]) {
                    yield return new WaitForSeconds(0.1f);
                    //other.position = _points[i].position;
                    owRigidbody.WarpToPositionRotation(_points[i].position, _points[i].rotation);
                    owRigidbody.SetVelocity(PointVelocity(_points[i]));
                }
                else {
                    while(true) {
                        yield return null;
                        //other.position += (_points[i].position - other.position).normalized * _speed * Time.deltaTime;
                        owRigidbody.WarpToPositionRotation(owRigidbody.GetPosition() + (_points[i].position - other.position).normalized * _speed * 10 * Time.deltaTime, _beams[i].transform.rotation);
                        owRigidbody.SetVelocity(PointVelocity(_points[i]));
                        if (Vector3.Distance(_points[i].position, other.position) < 1) {
                            break;
                        }
                    }
                }

                //prevRotation = _points[i].rotation;
            }
            Guardian.Log("end player moving");

            Patch._canBeDamaged = true;
            GUIMode.SetRenderMode(GUIMode.RenderMode.FPS);
            other.GetComponent<Collider>().enabled = true;
            other.Find("Traveller_HEA_Player_v2").gameObject.SetActive(true);

            if(_enabledObjs != null) {
                foreach(var obj in _enabledObjs) {
                    obj.SetActive(true);
                }
            }
        }
    }
}
