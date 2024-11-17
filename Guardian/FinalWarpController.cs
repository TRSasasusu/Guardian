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
        //public SphereItem _sphereItem;
        public GameObject _soundReveal;
        public GameObject _seeReveal;
        public GameObject _finalWarpFoundReveal;

        GameObject _coverCollider;
        GameObject _cometDummy;
        Transform _coverBlock;
        List<Transform> _brokenPlanes;
        List<Transform> _brokenPlanePoints;
        List<Transform> _cometPoints;
        GameObject _brokenSound;
        bool _addTime;
        bool _broken;

        //const float COMET_CRUSH_TIME = 180;
        const float COMET_CRUSH_TIME = 60 * 19 + 45;

        public void Initialize() {
            //_coverBlock = transform.Find("Cube").gameObject;
            _coverCollider = transform.Find("CoverCollider").gameObject;
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

            //foreach(Transform child in _plasmaWarp.transform) {
            //    if(child.name == "Core" || child.name.Contains("PlasmaBeam")) {
            //        child.gameObject.SetActive(false);
            //    }
            //}
            //_plasmaWarp._inactive = true;

            _brokenSound = transform.parent.Find("audio_brokensound").gameObject;
            _brokenSound.SetActive(false);

            StartCoroutine(ControlComet());

            _soundReveal.SetActive(false);
            _seeReveal.SetActive(false);

            //_cometDummy = transform.Find("DummyComet").gameObject;
            //_cometDummy.SetActive(false);
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
                if(time > COMET_CRUSH_TIME) {
                    break;
                }
                yield return null;
            }

            time = 0;
            if(_comet) {
                //_comet.SetActive(false);
                _comet.transform.position = _cometPoints[0].position;
                var pye = _comet.transform.Find("DeadNomai_Body");
                _cometDummy = _comet.transform.Find("Sector_CO").gameObject;
                if(pye) {
                    pye.transform.parent = _cometDummy.transform;
                }
                _cometDummy.transform.parent = transform;
                _cometDummy.transform.localEulerAngles = new Vector3(316.0919f, 107.2167f, 180);
                _cometDummy.transform.Find("Volumes_CO").gameObject.SetActive(false);
                _cometDummy.transform.Find("Sector_CometInterior/Geometry_CometInterior/Terrain_CO_Interior_NotIce/BatchedGroup/BatchedMeshColliders_0").GetComponent<Collider>().enabled = true;
                _cometDummy.transform.Find("Sector_CometInterior/Geometry_CometInterior/Terrain_CO_Interior_Ice/BatchedGroup/BatchedMeshColliders_0").GetComponent<Collider>().enabled = true;
                _cometDummy.transform.Find("Sector_CometInterior/Volumes_CometInterior/ZeroG_Volume_Comet_Inside").GetComponent<ZeroGVolume>()._attachedBody = GetComponentInParent<OWRigidbody>();
                _cometDummy.transform.Find("Sector_CometInterior/Volumes_CometInterior/ZeroG_Volume_Comet_Inside/EntrywayTrigger").GetComponent<Collider>().enabled = true;
                _cometDummy.transform.Find("Sector_CometInterior/Volumes_CometInterior/ZeroG_Volume_Comet_Inside/EntrywayTrigger (1)").GetComponent<Collider>().enabled = true;
                _cometDummy.transform.Find("Sector_CometInterior/Volumes_CometInterior/ZeroG_Volume_Comet_Inside/EntrywayTrigger (2)").GetComponent<Collider>().enabled = true;
                if(pye) {
                    foreach(var child in pye.GetComponentsInChildren<SkinnedMeshRenderer>()) {
                        child.enabled = true;
                    }
                    pye.Find("Prefab_NOM_Dead_Suit/Character_NOM_Dead_Suit/Nomai_Mesh:Mesh/Nomai_Mesh:Props_NOM_Mask_GearNew").gameObject.SetActive(true);
                }
                var poke = _cometDummy.transform.Find("Sector_CometInterior/Props_CometInterior/Prefab_NOM_Dead_Suit (1)/Character_NOM_Dead_Suit");
                if(poke) {
                    foreach(var child in poke.GetComponentsInChildren<SkinnedMeshRenderer>()) {
                        child.enabled = true;
                    }
                    poke.Find("Nomai_Mesh:Mesh/Nomai_Mesh:Props_NOM_Mask_GearNew").gameObject.SetActive(true);
                }
                var ambientLight = _comet.transform.Find("AmbientLight_CO");
                if(ambientLight) {
                    ambientLight.parent = _cometDummy.transform;
                }
            }
            _cometDummy.SetActive(true);
            _cometDummy.transform.position = _cometPoints[0].position;
            for (var i = 1; i < _cometPoints.Count; i++) {
                for (time = 0; time < 5; time += Time.deltaTime) {
                    _cometDummy.transform.position = Vector3.Lerp(_cometPoints[i - 1].position, _cometPoints[i].position, time / 5);
                    if(_comet) {
                        _comet.transform.position = _cometDummy.transform.position;
                    }
                    if(i == _cometPoints.Count - 1) {
                        if (time == 0) {
                            _brokenSound.SetActive(true);
                            _soundReveal.SetActive(true);
                            //_seeReveal.SetActive(true); // weirdly this causes reveal even in everywhere. i think it is caused by the interloper
                            _coverCollider.SetActive(false);
                            _broken = true;
                        }
                        for(var j = 0; j < _brokenPlanes.Count; ++j) {
                            _brokenPlanes[j].localPosition = Vector3.Lerp(Vector3.zero, _brokenPlanePoints[j].localPosition, time / 5);
                            _brokenPlanes[j].localRotation = Quaternion.Lerp(Quaternion.Euler(-90, 0, 0), _brokenPlanePoints[j].localRotation, time / 5);
                        }
                    }
                    yield return null;
                }
            }
            yield return new WaitForSeconds(15);
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
            if(!_broken) {
                return;
            }
            var player = Locator.GetPlayerBody();
            if(player && _seeReveal && !_seeReveal.activeSelf && Vector3.Distance(player.transform.position, _seeReveal.transform.position) <= 200) {
                _seeReveal.SetActive(true);
            }
            if(player && _finalWarpFoundReveal && !_finalWarpFoundReveal.activeSelf && Vector3.Distance(player.transform.position, _finalWarpFoundReveal.transform.position) <= 15) {
                _finalWarpFoundReveal.SetActive(true);
            }
            if(_addTime) {
                return;
            }
            if(!player || !_plasmaWarp) {
                return;
            }
            if(Vector3.Distance(player.transform.position, _plasmaWarp.transform.position) < 250) {
                if(SphereItem.PickedUpSphereItem) {
                    if(TimeLoop.GetSecondsRemaining() < 240) {
                        TimeLoop.SetSecondsRemaining(240);
                    }
                }
                else {
                    if(TimeLoop.GetSecondsRemaining() < 180) {
                        TimeLoop.SetSecondsRemaining(180);
                    }
                }
                _addTime = true;
            }

            //if(!_plasmaWarp || !_plasmaWarp._inactive) {
            //    return;
            //}
            //if(_sphereItem && Vector3.Distance(_sphereItem.transform.position, _plasmaWarp.transform.position) < 30) {
            //    foreach(Transform child in _plasmaWarp.transform) {
            //        if(child.name == "Core" || child.name.Contains("PlasmaBeam")) {
            //            child.gameObject.SetActive(true);
            //        }
            //        if(child.name == "LowCore") {
            //            child.gameObject.SetActive(false);
            //        }
            //    }
            //    _plasmaWarp._inactive = false;

            //    if(TimeLoop.GetSecondsRemaining() < 180) {
            //        TimeLoop.SetSecondsRemaining(180);
            //    }
            //}
        }
    }
}
