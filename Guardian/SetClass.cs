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
        const string SUN_HEAT_VOLUME_PATH = "Sun_Body/Sector_SUN/Volumes_SUN/HeatVolume";
        const string SUN_INNER_DESTRUCTION_VOLUME_PATH = "Sun_Body/Sector_SUN/Volumes_SUN/InnerDestructionVolume";
        const string SUN_DESTRUCTION_FLUID_VOLUME_PATH = "Sun_Body/Sector_SUN/Volumes_SUN/ScaledVolumesRoot/DestructionFluidVolume";
        const string SUN_GRAVITY_WELL_PATH = "Sun_Body/GravityWell_SUN";
        const string SUN_AUDIO_PATH = "Sun_Body/Sector_SUN/Audio_SUN";
        const string PLASMA_WARP_0_PATH = "SunStation_Body/Sector_SunStation/SunStationEntrance/PlasmaWarp0";
        readonly string[] PLASMA_WARP_0_POINT_PATHS = new string[] {
            "SunStation_Body/Sector_SunStation/SunStationEntrance/PlasmaWarp0/PlasmaWarp0Point0",
            "SunStation_Body/Sector_SunStation/SunStationEntrance/PlasmaWarp0/PlasmaWarp0Point1",
            "SunCore_Body/Sector/SunCoreStructure/EnergyStabilizer/PlasmaWarp0Point2",
            "SunCore_Body/Sector/SunCoreStructure/EnergyStabilizer/PlasmaWarp0Point3",
        };
        const string CORE_SUN_AUDIO_PATH = "SunCore_Body/Sector/Star/Audio_Star/SurfaceAudio_Sun";

        public SetClass() {
            Guardian.Instance.StartCoroutine(InitializeBody());
        }

        IEnumerator InitializeBody() {
            Guardian.Log("start: set first memoryspace");
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
            Guardian.Log("end: set first memoryspace");

            Guardian.Log("start: find sun volumes");
            GameObject sunHeatVolume;
            while(true) {
                sunHeatVolume = GameObject.Find(SUN_HEAT_VOLUME_PATH);
                if(sunHeatVolume) {
                    break;
                }
                yield return null;
            }
            GameObject sunInnerDestructionVolume;
            while(true) {
                sunInnerDestructionVolume = GameObject.Find(SUN_INNER_DESTRUCTION_VOLUME_PATH);
                if(sunInnerDestructionVolume) {
                    break;
                }
                yield return null;
            }
            GameObject sunDestructionFluidVolume;
            while(true) {
                sunDestructionFluidVolume = GameObject.Find(SUN_DESTRUCTION_FLUID_VOLUME_PATH);
                if(sunDestructionFluidVolume) {
                    break;
                }
                yield return null;
            }
            GameObject sunGravityWell;
            while(true) {
                sunGravityWell = GameObject.Find(SUN_GRAVITY_WELL_PATH);
                if(sunGravityWell) {
                    break;
                }
                yield return null;
            }
            GameObject sunAudio;
            while(true) {
                sunAudio = GameObject.Find(SUN_AUDIO_PATH);
                if(sunAudio) {
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: find sun volumes");

            Guardian.Log("start: setting on core sun");
            while(true) {
                var coreSunAudio = GameObject.Find(CORE_SUN_AUDIO_PATH);
                if(coreSunAudio) {
                    coreSunAudio.GetComponent<OWAudioSource>()._maxSourceVolume = 0.2f;
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: setting on core sun");

            Guardian.Log("start: set warp 0");
            while(true) {
                var warpObj = GameObject.Find(PLASMA_WARP_0_PATH);
                if(warpObj) {
                    var warp = warpObj.AddComponent<PlasmaWarp>();
                    var points = new List<Transform>();
                    foreach(var path in PLASMA_WARP_0_POINT_PATHS) {
                        while(true) {
                            var point = GameObject.Find(path);
                            if(point) {
                                points.Add(point.transform);
                                break;
                            }
                            yield return null;
                        }
                    }
                    warp.Initialize(points, new int[] { 2 }, new GameObject[] { sunHeatVolume, sunInnerDestructionVolume, sunDestructionFluidVolume, sunGravityWell, sunAudio });
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: set warp 0");
        }
    }
}
