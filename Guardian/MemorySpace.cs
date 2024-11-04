using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using IEnumerator = System.Collections.IEnumerator;

namespace Guardian {
    public class MemorySpace : MonoBehaviour {
        public List<Collider> _colliders;
        public List<GameObject> _objs;
        public MemoryCore _memoryCore;
        public MemoryCore.Style _style { get { return _memoryCore._style; } }

        void OnTriggerEnter(Collider other) {
            if(other.gameObject == Locator._playerBody.gameObject) {
                Guardian.Log("player enters");
                if(_colliders != null) {
                    foreach(var collider in _colliders) {
                        if(collider) {
                            collider.enabled = false;
                        }
                    }
                }
                if(_objs != null) {
                    foreach(var obj in _objs) {
                        if(obj) {
                            obj.SetActive(false);
                        }
                    }
                }
            }
        }

        void OnTriggerExit(Collider other) {
            if(other.gameObject == Locator._playerBody.gameObject) {
                Guardian.Log("player exits");
                End();
            }
        }

        void End() {
            if(_colliders != null) {
                foreach(var collider in _colliders) {
                    if(collider) {
                        collider.enabled = true;
                    }
                }
            }
            if(_objs != null) {
                foreach(var obj in _objs) {
                    if(obj) {
                        obj.SetActive(true);
                    }
                }
            }
            if(_memoryCore) {
                //foreach(var child in GetComponentsInChildren<Transform>()) {
                foreach(Transform child in transform) {
                    //if(child.gameObject == gameObject) {
                    //    continue;
                    //}
                    child.gameObject.SetActive(false);
                }
                if(_memoryCore._memorySpaceAnimationCoroutine != null) {
                    _memoryCore.StopCoroutine(_memoryCore._memorySpaceAnimationCoroutine);
                    _memoryCore._memorySpaceAnimationCoroutine = null;
                }
                var memoryCoreSpheres = new List<Transform> { _memoryCore.transform.Find("memory_core_frame/smooth_sphere"), _memoryCore.transform.Find("memory_core_frame/smooth_sphere_inside") };
                memoryCoreSpheres[0].gameObject.SetActive(true);
                memoryCoreSpheres[1].gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }

        public IEnumerator Animation() {
            Guardian.Log("start Animation");
            var memoryCoreSpheres = new List<Transform> { _memoryCore.transform.Find("memory_core_frame/smooth_sphere"), _memoryCore.transform.Find("memory_core_frame/smooth_sphere_inside") };
            var spaceSphere = transform.Find("smooth_sphere_inside");
            Guardian.Log("start expansion in Animation");
            while(true) {
                yield return null;
                memoryCoreSpheres[0].transform.localScale += Vector3.one * Time.deltaTime * 30;
                memoryCoreSpheres[1].transform.localScale += Vector3.one * Time.deltaTime * 30;
                if (memoryCoreSpheres[1].transform.localScale.x >= spaceSphere.transform.localScale.x) {
                    memoryCoreSpheres[0].transform.localScale = Vector3.one * 0.5f;
                    memoryCoreSpheres[1].transform.localScale = Vector3.one * 0.5f;
                    memoryCoreSpheres[0].gameObject.SetActive(false);
                    memoryCoreSpheres[1].gameObject.SetActive(false);
                    break;
                }
            }
            gameObject.SetActive(true);

            if(_style == MemoryCore.Style.CAUSE_SUPERNOVA) {
                var robot = transform.Find("robot");
                var robotCore = robot.Find("HiddenObjs/smooth_sphere");
                var points = new List<Transform>();
                for(var i = 0; i <= 7; ++i) {
                    points.Add(transform.Find($"robot_point{i}"));
                }
                var sun = transform.Find("smooth_sphere");
                var sunExpanded = transform.Find("smooth_sphere_expansion");
                var supernovas = new List<Transform>();
                for(var i = 0; i <= 4; ++i) {
                    supernovas.Add(transform.Find($"supernova{i}"));
                }
                var zeroGravity = transform.Find("memory_zerogravity");
                var audio = transform.Find("audio_causesupernova");

                zeroGravity.gameObject.SetActive(true);
                spaceSphere.gameObject.SetActive(true);
                sun.gameObject.SetActive(true);
                audio.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                robot.gameObject.SetActive(true);
                robotCore.gameObject.SetActive(false);
                for(var i = 1; i <= 7; ++i) {
                    yield return new WaitForSeconds(1);
                    robot.position = points[i].position;
                }
                yield return new WaitForSeconds(1);
                sun.gameObject.SetActive(false);
                sunExpanded.gameObject.SetActive(true);
                robotCore.gameObject.SetActive(true);
                for(var i = 7; i >= 3; --i) {
                    yield return new WaitForSeconds(1);
                    robot.position = points[i].position;
                }
                sunExpanded.gameObject.SetActive(false);
                supernovas[0].gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                robot.position = points[2].position;
                supernovas[0].gameObject.SetActive(false);
                supernovas[1].gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                robot.position = points[1].position;
                supernovas[1].gameObject.SetActive(false);
                supernovas[2].gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                robot.position = points[0].position;
                supernovas[2].gameObject.SetActive(false);
                supernovas[3].gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                robot.gameObject.SetActive(false);
                supernovas[3].gameObject.SetActive(false);
                supernovas[4].gameObject.SetActive(true);
                yield return new WaitForSeconds(1.5f);

                End();
            }
            else if(_style == MemoryCore.Style.STABILIZE_WITH_SUN_STATION) {
                var robot = transform.Find("robot");
                var points = new List<Transform>();
                for(var i = 0; i <= 4; ++i) {
                    points.Add(transform.Find($"robotpoint{i}"));
                }
                var sun = transform.Find("smooth_sphere");
                var sunExpanded = transform.Find("smooth_sphere_expansion");
                var sunstation = transform.Find("sun_station_entrance");
                var zeroGravity = transform.Find("memory_zerogravity");
                var audio = transform.Find("audio_stabilizewithss");

                zeroGravity.gameObject.SetActive(true);
                spaceSphere.gameObject.SetActive(true);
                sunstation.gameObject.SetActive(true);
                sun.gameObject.SetActive(true);
                audio.gameObject.SetActive(true);
                for(var j = 0; j < 3; ++j) {
                    yield return new WaitForSeconds(1.5f);
                    sun.gameObject.SetActive(false);
                    sunExpanded.gameObject.SetActive(true);
                    yield return new WaitForSeconds(1);
                    robot.gameObject.SetActive(true);
                    robot.position = points[0].position;
                    for(var i = 1; i <= 4; ++i) {
                        yield return new WaitForSeconds(1);
                        robot.position = points[i].position;
                    }
                    yield return new WaitForSeconds(1);
                    sun.gameObject.SetActive(true);
                    sunExpanded.gameObject.SetActive(false);
                }
                yield return new WaitForSeconds(1.5f);

                End();
            }
            else if(_style == MemoryCore.Style.HIDDEN_HATCH) {
                var robot = transform.Find("robot");
                var points = new List<Transform>();
                for(var i = 0; i <= 5; ++i) {
                    points.Add(transform.Find($"robotpoint{i}"));
                }
                var sun = transform.Find("smooth_sphere");
                var hatch = transform.Find("cylinder_empty_thick");
                var hatchShadow = transform.Find("smooth_sphere (1)");
                var zeroGravity = transform.Find("memory_zerogravity");
                var audio = transform.Find("audio_hiddenhatch");

                zeroGravity.gameObject.SetActive(true);
                spaceSphere.gameObject.SetActive(true);
                sun.gameObject.SetActive(true);
                audio.gameObject.SetActive(true);
                yield return new WaitForSeconds(2);
                robot.gameObject.SetActive(true);
                robot.position = points[0].position;
                for(var i = 1; i <= 3; ++i) {
                    yield return new WaitForSeconds(2);
                    robot.position = points[i].position;
                }
                hatch.gameObject.SetActive(true);
                hatchShadow.gameObject.SetActive(true);
                for(var i = 4; i <= 5; ++i) {
                    yield return new WaitForSeconds(2);
                    robot.position = points[i].position;
                }
                yield return new WaitForSeconds(1);
                hatch.gameObject.SetActive(false);
                hatchShadow.gameObject.SetActive(false);
                yield return new WaitForSeconds(1.5f);

                End();
            }
            else if(_style == MemoryCore.Style.LANTERN_HIT) {
                var robot = transform.Find("robot");
                var signal = transform.Find("robot/HiddenObjs/octagonal_cylinder/Particle System");
                var points = new List<Transform>();
                for(var i = 0; i <= 14; ++i) {
                    points.Add(transform.Find($"robotpoint{i}"));
                }
                var meteorPoints = new List<Transform>();
                for(var i = 0; i <= 3; ++i) {
                    meteorPoints.Add(transform.Find($"meteorpoint{i}"));
                }
                var lantern = transform.Find("smooth_sphere");
                var meteor = transform.Find("memory_meteor");
                var bomb = transform.Find("smooth_sphere (1)");
                var zeroGravity = transform.Find("memory_zerogravity");
                var audio = transform.Find("audio_lanternhit");

                zeroGravity.gameObject.SetActive(true);
                spaceSphere.gameObject.SetActive(true);
                lantern.gameObject.SetActive(true);
                robot.position = points[0].position;
                robot.rotation = points[0].rotation;
                robot.gameObject.SetActive(true);
                signal.gameObject.SetActive(true);
                audio.gameObject.SetActive(true);

                for(var i = 1; i <= 6; ++i) {
                    yield return new WaitForSeconds(1);
                    robot.position = points[i].position;
                    if(i == 3) {
                        meteor.gameObject.SetActive(true);
                    }
                    if(i >= 3) {
                        meteor.position = meteorPoints[i - 3].position;
                    }
                }
                yield return new WaitForSeconds(1);
                bomb.gameObject.SetActive(true);
                yield return new WaitForSeconds(2);
                bomb.gameObject.SetActive(false);
                meteor.gameObject.SetActive(false);
                signal.gameObject.SetActive(false);
                for(var i = 7; i <= 14; ++i) {
                    yield return new WaitForSeconds(1);
                    robot.position = points[i].position;
                    robot.rotation = points[i].rotation;
                }
                yield return new WaitForSeconds(1.5f);

                End();
            }
        }
    }
}
