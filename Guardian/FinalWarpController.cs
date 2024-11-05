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

        GameObject _coverBlock;
        GameObject _brokenSound;

        public void Initialize() {
            _coverBlock = transform.Find("Cube").gameObject;
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
                _comet.transform.position = transform.position;
                _comet.transform.eulerAngles = transform.eulerAngles + new Vector3(0, 90, 0);
                _brokenSound.SetActive(true);
                _coverBlock.SetActive(false);
                _soundReveal.SetActive(true);
                _seeReveal.SetActive(true);
                while(true) {
                    yield return null;
                    time += Time.deltaTime;
                    if(_comet) {
                        _comet.transform.position = transform.position;
                        _comet.transform.eulerAngles = transform.eulerAngles + new Vector3(0, 90, 0);
                    }
                    if(time > 19 && _brokenSound.activeSelf) {
                        _brokenSound.SetActive(false);
                    }
                }
            }
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
