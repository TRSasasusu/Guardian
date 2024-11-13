using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using IEnumerator = System.Collections.IEnumerator;

namespace Guardian {
    public class FinalWarpController : MonoBehaviour {
        public PlasmaWarp _plasmaWarp;
        public GameObject _comet;
        public SphereItem _sphereItem;
        public GameObject _soundReveal;
        public GameObject _seeReveal;

        GameObject _cometDummy;
        Transform _coverBlock;
        List<Transform> _brokenPlanes;
        List<Transform> _brokenPlanePoints;
        List<Transform> _cometPoints;
        GameObject _brokenSound;

        public void Initialize() {
            //_coverBlock = transform.Find("Cube").gameObject;
            _coverBlock = transform.Find("broken_block");
            _brokenPlanes = new List<Transform>();
            _brokenPlanePoints = new List<Transform>();
            foreach(Transform child in _coverBlock.Cast<Transform>().OrderBy(x => x.GetSiblingIndex())) {
                if(child.name.Contains("Plane")) {
                    child.localPosition = Vector3.zero;
                    child.localEulerAngles = new Vector3(-90, 0, 0);
                }
                else if(child.name.Contains("point")) {
                    _brokenPlanePoints.Add(child);
                    _brokenPlanes.Add(_coverBlock.Find($"Plane.{child.name.Substring(6)}"));
                }
            }

            foreach(Transform child in _plasmaWarp.transform) {
                if(child.name == "Core" || child.name.Contains("PlasmaBeam")) {
                    child.gameObject.SetActive(false);
                }
            }
            _plasmaWarp._inactive = true;

            _brokenSound = transform.parent.Find("audio_brokensound").gameObject;
            _brokenSound.SetActive(false);

            StartCoroutine(ControlComet());

            _soundReveal.SetActive(false);
            _seeReveal.SetActive(false);

            _cometDummy = transform.Find("DummyComet").gameObject;
            _cometDummy.SetActive(false);
            _cometPoints = new List<Transform>();
            foreach(Transform child in transform.Cast<Transform>().OrderBy(x => x.GetSiblingIndex())) {
                if(child.name.Contains("InterloperPoint")) {
                    _cometPoints.Add(child);
                }
            }
        }

        IEnumerator ControlComet() {
            float time = 0;
            while(true) {
                time += Time.deltaTime;
                if(time > 60 * 19 + 45) {
                    break;
                }
                yield return null;
            }

            time = 0;
            if(_comet) {
                _comet.SetActive(false);
            }
            _cometDummy.SetActive(true);
            _cometDummy.transform.position = _cometPoints[0].position;
            for (var i = 1; i < _cometPoints.Count; i++) {
                for (time = 0; time < 5; time += Time.deltaTime) {
                    _cometDummy.transform.position = Vector3.Lerp(_cometPoints[i - 1].position, _cometPoints[i].position, time / 5);
                    if(i == _cometPoints.Count - 1) {
                        if (time == 0) {
                            _brokenSound.SetActive(true);
                            _soundReveal.SetActive(true);
                            _seeReveal.SetActive(true);
                        }
                        for(var j = 0; j < _brokenPlanes.Count; ++j) {
                            _brokenPlanes[j].position = Vector3.Lerp(Vector3.zero, _brokenPlanePoints[j].position, time / 5);
                        }
                    }
                    yield return null;
                }
            }
            _brokenSound.SetActive(false);

            //time = 0;
            //if(_comet) {
            //    _comet.transform.position = transform.position;
            //    _comet.transform.eulerAngles = transform.eulerAngles + new Vector3(0, 90, 0);
            //    _brokenSound.SetActive(true);
            //    _coverBlock.SetActive(false);
            //    _soundReveal.SetActive(true);
            //    _seeReveal.SetActive(true);
            //    while(true) {
            //        yield return null;
            //        time += Time.deltaTime;
            //        if(_comet) {
            //            _comet.transform.position = transform.position;
            //            _comet.transform.eulerAngles = transform.eulerAngles + new Vector3(0, 90, 0);
            //        }
            //        if(time > 19 && _brokenSound.activeSelf) {
            //            _brokenSound.SetActive(false);
            //        }
            //    }
            //}
        }

        void Update() {
            if(!_plasmaWarp || !_plasmaWarp._inactive) {
                return;
            }
            if(_sphereItem && Vector3.Distance(_sphereItem.transform.position, _plasmaWarp.transform.position) < 30) {
                foreach(Transform child in _plasmaWarp.transform) {
                    if(child.name == "Core" || child.name.Contains("PlasmaBeam")) {
                        child.gameObject.SetActive(true);
                    }
                    if(child.name == "LowCore") {
                        child.gameObject.SetActive(false);
                    }
                }
                _plasmaWarp._inactive = false;

                if(TimeLoop.GetSecondsRemaining() < 180) {
                    TimeLoop.SetSecondsRemaining(180);
                }
            }
        }
    }
}
