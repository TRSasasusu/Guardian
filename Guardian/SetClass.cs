using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using IEnumerator = System.Collections.IEnumerator;

namespace Guardian {
    public class SetClass {
        const string MEMORY_SPACE_SUNSTATION_PATH = "SunStation_Body/Sector_SunStation/SunStationEntrance/smooth_sphere_inside";
        const string SUNSTATION_COLLIDER_PATH = "SunStation_Body/Sector_SunStation/Geometry_SunStation/SunStation_ControlModule_Geo/Structure_NOM_SunStation_ControlINT/CONTROL_INT_COLLIDE";

        public SetClass() {
            Guardian.Instance.StartCoroutine(InitializeBody());
        }

        IEnumerator InitializeBody() {
            while (true) {
                yield return null;
                var memorySpaceObj = GameObject.Find(MEMORY_SPACE_SUNSTATION_PATH);
                if(memorySpaceObj) {
                    var memorySpace = memorySpaceObj.AddComponent<MemorySpace>();
                    while(true) {
                        var sunstationColliderObj = GameObject.Find(SUNSTATION_COLLIDER_PATH);
                        if(sunstationColliderObj) {
                            memorySpace._colliders = new List<Collider> {
                                sunstationColliderObj.GetComponent<Collider>(),
                            };
                            break;
                        }
                        yield return null;
                    }
                    memorySpaceObj.transform.parent.gameObject.AddComponent<TunePosSunStationEntrance>();
                    break;
                }
            }
        }
    }
}
