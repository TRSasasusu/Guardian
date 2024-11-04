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
        const string PLASMA_WARP_1_PATH = "SunCore_Body/Sector/SunCoreStructure/EnergyStabilizer/PlasmaWarp";
        readonly string[] PLASMA_WARP_1_POINT_PATHS = new string[] {
            "SunCore_Body/Sector/SunCoreStructure/WaitingArea/PlasmaWarp1Point0",
            "SunCore_Body/Sector/SunCoreStructure/WaitingArea/PlasmaWarp1Point0 (1)",
            "SunCore_Body/Sector/SunCoreStructure/WaitingArea/PlasmaWarp1Point0 (2)",
            "SunCore_Body/Sector/SunCoreStructure/WaitingArea/PlasmaWarp1Point0 (3)",
        };
        const string PLASMA_WARP_2_PATH = "SunCore_Body/Sector/SunCoreStructure/WaitingArea/PlasmaWarp";
        readonly string[] PLASMA_WARP_2_POINT_PATHS = new string[] {
            "SunCore_Body/Sector/SunCoreStructure/WaitingArea/PlasmaWarp2Point0",
            "SunCore_Body/Sector/SunCoreStructure/WaitingArea/PlasmaWarp2Point1",
            "SunCore_Body/Sector/SunCoreStructure/WaitingArea/PlasmaWarp2Point2",
            "SunCore_Body/Sector/SunCoreStructure/WaitingArea/PlasmaWarp2Point3",
            "SunCore_Body/Sector/SunCoreStructure/WaitingArea/PlasmaWarp2Point4",
        };
        const string PLASMA_WARP_3_PATH = "SunCore_Body/Sector/SunCoreStructure/EnergyStabilizer/PlasmaWarpToCoreUp";
        readonly string[] PLASMA_WARP_3_POINT_PATHS = new string[] {
            "SunCore_Body/Sector/SunCoreStructure/CoreUp/PlasmaWarp3Point0",
        };
        const string PLASMA_WARP_COREUP_ESCAPE_PATH = "SunCore_Body/Sector/SunCoreStructure/CoreUp/PlasmaWarpEscape";
        const string PLASMA_WARP_CORE_FINAL_PATH = "SunCore_Body/Sector/SunCoreStructure/CoreCrush/PlasmaWarpFinal";
        const string PLASMA_WARP_WAIT_TO_CRUSH_PATH = "SunCore_Body/Sector/SunCoreStructure/WaitingArea/PlasmaWarpLocal";
        const string PLASMA_WARP_HIDDEN_TO_WAIT_PATH = "SunCore_Body/Sector/SunCoreStructure/HiddenHatch/PlasmaWarpLocal";
        const string CORE_SUN_AUDIO_PATH = "SunCore_Body/Sector/Star/Audio_Star/SurfaceAudio_Sun";
        const string CORE_HEAT_VOLUME_PATH = "SunCore_Body/Sector/Star/HeatVolume";
        const string CORE_DESTRUCTION_VOLUME_PATH = "SunCore_Body/Sector/Star/DestructionFluidVolume";
        const string CORE_PLANET_DESTRUCTION_VOLUME_PATH = "SunCore_Body/Sector/Star/PlanetDestructionVolume";
        const string CORE_GRAVITY_WELL_PATH = "SunCore_Body/GravityWell";
        const string CORE_ENERGY_STABILIZER_PATH = "SunCore_Body/Sector/SunCoreStructure/EnergyStabilizer";
        const string CORE_WAITING_AREA_PATH = "SunCore_Body/Sector/SunCoreStructure/WaitingArea";
        const string CORE_CORE_UP_PATH = "SunCore_Body/Sector/SunCoreStructure/CoreUp";
        const string CORE_CORE_CENTER_PATH = "SunCore_Body/Sector/SunCoreStructure/CoreCenter";
        const string CORE_CORE_CRUSH_PATH = "SunCore_Body/Sector/SunCoreStructure/CoreCrush";
        const string MEMORY_CORE_CAUSE_SUPERNOVA_PATH = "SunCore_Body/Sector/SunCoreStructure/EnergyStabilizer/MemoryCoreCauseSupernova";
        const string MEMORY_CORE_STABILIZE_WITH_SUNSTATION_PATH = "SunCore_Body/Sector/SunCoreStructure/EnergyStabilizer/MemoryCoreStabilizeWithSunStation";
        const string MEMORY_CORE_HIDDEN_HATCH_PATH = "SunCore_Body/Sector/SunCoreStructure/WaitingArea/MemoryCoreHiddenHatch";
        const string MEMORY_CORE_LANTERN_HIT_PATH = "SunCore_Body/Sector/SunCoreStructure/CoreUp/MemoryCoreLanternHit";
        const string MEMORY_CORE_FINAL_TH_PATH = "SunCore_Body/Sector/SunCoreStructure/CoreCenter/MemoryCore";
        const string HIDDEN_HATCH_PATH = "SunCore_Body/Sector/SunCoreStructure/HiddenHatch";
        const string ROBOT_PATH = "Robot_Body";
        const string CORE_OUTSIDE_SURFACE_PATH = "SunCore_Body/Sector/SunOutsideSurface/notsmooth_sphere_inside";
        const string ENERGY_SPHERE_ITEM_PATH = "VolcanicMoon_Body/Sector_VM/robotonlantern Variant/HiddenObjs/smooth_sphere";
        const string COMET_PATH = "Comet_Body";
        const string CORE_OF_CORE_PATH = "CoreofCore_Body/Sector/Star";
        const string REVEAL_CRUSH_INTERLOPER = "SunCore_Body/Sector/reveal_crushinterloper";
        const string REVEAL_LOW_CORE = "SunCore_Body/Sector/reveal_lowcore";

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
            GameObject coreHeatVolume;
            while(true) {
                coreHeatVolume = GameObject.Find(CORE_HEAT_VOLUME_PATH);
                if(coreHeatVolume) {
                    coreHeatVolume.GetComponent<SphereShape>().radius = 1;
                    break;
                }
                yield return null;
            }
            GameObject coreDestructionVolume;
            while(true) {
                coreDestructionVolume = GameObject.Find(CORE_DESTRUCTION_VOLUME_PATH);
                if(coreDestructionVolume) {
                    break;
                }
                yield return null;
            }
            GameObject corePlanetDestructionVolume;
            while(true) {
                corePlanetDestructionVolume = GameObject.Find(CORE_PLANET_DESTRUCTION_VOLUME_PATH);
                if(corePlanetDestructionVolume) {
                    break;
                }
                yield return null;
            }
            GameObject coreGravityWell;
            while(true) {
                coreGravityWell = GameObject.Find(CORE_GRAVITY_WELL_PATH);
                if(coreGravityWell) {
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: setting on core sun");

            Guardian.Log("start: find each area");
            GameObject energyStabilizer;
            while(true) {
                energyStabilizer = GameObject.Find(CORE_ENERGY_STABILIZER_PATH);
                if(energyStabilizer) {
                    break;
                }
                yield return null;
            }
            GameObject waitingArea;
            while(true) {
                waitingArea = GameObject.Find(CORE_WAITING_AREA_PATH);
                if(waitingArea) {
                    break;
                }
                yield return null;
            }
            GameObject coreUp;
            while(true) {
                coreUp = GameObject.Find(CORE_CORE_UP_PATH);
                if(coreUp) {
                    break;
                }
                yield return null;
            }
            GameObject coreCenter;
            while(true) {
                coreCenter = GameObject.Find(CORE_CORE_CENTER_PATH);
                if(coreCenter) {
                    break;
                }
                yield return null;
            }
            GameObject coreOfCore;
            while(true) {
                coreOfCore = GameObject.Find(CORE_OF_CORE_PATH);
                if(coreOfCore) {
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: find each area");

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

            Guardian.Log("start: set warp 1");
            while(true) {
                var warpObj = GameObject.Find(PLASMA_WARP_1_PATH);
                if(warpObj) {
                    var warp = warpObj.AddComponent<PlasmaWarp>();
                    var points = new List<Transform>();
                    foreach(var path in PLASMA_WARP_1_POINT_PATHS) {
                        while(true) {
                            var point = GameObject.Find(path);
                            if(point) {
                                points.Add(point.transform);
                                break;
                            }
                            yield return null;
                        }
                    }
                    warp.Initialize(points);
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: set warp 1");

            Guardian.Log("start: set warp 2");
            while(true) {
                var warpObj = GameObject.Find(PLASMA_WARP_2_PATH);
                if(warpObj) {
                    var warp = warpObj.AddComponent<PlasmaWarp>();
                    var points = new List<Transform>();
                    foreach(var path in PLASMA_WARP_2_POINT_PATHS) {
                        while(true) {
                            var point = GameObject.Find(path);
                            if(point) {
                                points.Add(point.transform);
                                break;
                            }
                            yield return null;
                        }
                    }
                    warp.Initialize(points);
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: set warp 2");

            Guardian.Log("start: set warp 3");
            while(true) {
                var warpObj = GameObject.Find(PLASMA_WARP_3_PATH);
                if(warpObj) {
                    var warp = warpObj.AddComponent<PlasmaWarp>();
                    var points = new List<Transform>();
                    foreach(var path in PLASMA_WARP_3_POINT_PATHS) {
                        while(true) {
                            var point = GameObject.Find(path);
                            if(point) {
                                points.Add(point.transform);
                                break;
                            }
                            yield return null;
                        }
                    }
                    warp.Initialize(points, null, new GameObject[] { coreHeatVolume, coreDestructionVolume, corePlanetDestructionVolume });
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: set warp 3");

            Guardian.Log("start: warps in coreup");
            foreach(Transform child in coreUp.transform) {
                if(!child.name.Contains("PlasmaWarpLocal")) {
                    continue;
                }
                var warp = child.gameObject.AddComponent<PlasmaWarp>();
                var points = new List<Transform>();
                foreach(Transform childOfWarp in warp.transform.Cast<Transform>().OrderBy(x => x.GetSiblingIndex())) {
                    if(!childOfWarp.name.Contains("point")) {
                        continue;
                    }
                    points.Add(childOfWarp);
                }
                warp.Initialize(points);
            }
            Guardian.Log("end: warps in coreup");

            Guardian.Log("start: coreup escape warp");
            while(true) {
                var warpObj = GameObject.Find(PLASMA_WARP_COREUP_ESCAPE_PATH);
                if(warpObj) {
                    var warp = warpObj.AddComponent<PlasmaWarp>();
                    var points = new List<Transform>();
                    foreach(Transform childOfWarp in warp.transform.Cast<Transform>().OrderBy(x => x.GetSiblingIndex())) {
                        if(!childOfWarp.name.Contains("point")) {
                            continue;
                        }
                        points.Add(childOfWarp);
                    }
                    warp.Initialize(points, null, null, new GameObject[] { coreHeatVolume, coreDestructionVolume, corePlanetDestructionVolume });
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: coreup escape warp");

            Guardian.Log("start: core final warp");
            PlasmaWarp finalWarp;
            while(true) {
                var warpObj = GameObject.Find(PLASMA_WARP_CORE_FINAL_PATH);
                if(warpObj) {
                    finalWarp = warpObj.AddComponent<PlasmaWarp>();
                    var points = new List<Transform>();
                    foreach(Transform childOfWarp in finalWarp.transform.Cast<Transform>().OrderBy(x => x.GetSiblingIndex())) {
                        if(!childOfWarp.name.Contains("point")) {
                            continue;
                        }
                        points.Add(childOfWarp);
                    }
                    finalWarp.Initialize(points, null, new GameObject[] { coreHeatVolume, coreDestructionVolume, corePlanetDestructionVolume, coreGravityWell });
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: core final warp");

            Guardian.Log("start: additional warps");
            while(true) {
                var warpObj = GameObject.Find(PLASMA_WARP_HIDDEN_TO_WAIT_PATH);
                if(warpObj) {
                    var warp = warpObj.AddComponent<PlasmaWarp>();
                    var points = new List<Transform>();
                    foreach(Transform childOfWarp in warp.transform.Cast<Transform>().OrderBy(x => x.GetSiblingIndex())) {
                        if(!childOfWarp.name.Contains("point")) {
                            continue;
                        }
                        points.Add(childOfWarp);
                    }
                    warp.Initialize(points);
                    break;
                }
                yield return null;
            }
            while(true) {
                var warpObj = GameObject.Find(PLASMA_WARP_WAIT_TO_CRUSH_PATH);
                if(warpObj) {
                    var warp = warpObj.AddComponent<PlasmaWarp>();
                    var points = new List<Transform>();
                    foreach(Transform childOfWarp in warp.transform.Cast<Transform>().OrderBy(x => x.GetSiblingIndex())) {
                        if(!childOfWarp.name.Contains("point")) {
                            continue;
                        }
                        points.Add(childOfWarp);
                    }
                    warp.Initialize(points);
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: additional warps");

            Guardian.Log("start: set memory core cause supernova");
            while (true) {
                var memoryCoreObj = GameObject.Find(MEMORY_CORE_CAUSE_SUPERNOVA_PATH);
                if(memoryCoreObj) {
                    var memoryCore = memoryCoreObj.AddComponent<MemoryCore>();
                    memoryCore._style = MemoryCore.Style.CAUSE_SUPERNOVA;
                    memoryCoreObj.transform.Find("memory_space/memory_zerogravity").gameObject.SetActive(false);
                    memoryCoreObj.transform.Find("memory_space/audio_causesupernova").gameObject.SetActive(false);
                    memoryCoreObj.transform.Find("memory_space/reveal_memory").gameObject.SetActive(false);
                    memoryCore._disabledObjs = new List<GameObject> {
                        energyStabilizer.transform.Find("scaffold (4)").gameObject,
                        energyStabilizer.transform.Find("scaffold (7)").gameObject,
                        energyStabilizer.transform.Find("scaffold (8)").gameObject,
                        energyStabilizer.transform.Find("scaffold (9)").gameObject,
                        energyStabilizer.transform.Find("scaffold (10)").gameObject,
                        energyStabilizer.transform.Find("scaffold (11)").gameObject,
                        energyStabilizer.transform.Find("scaffold (12)").gameObject,
                        energyStabilizer.transform.Find("scaffold (13)").gameObject,
                        energyStabilizer.transform.Find("scaffold (14)").gameObject,
                        energyStabilizer.transform.Find("upper_block").gameObject,
                    };
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: set memory core cause supernova");

            Guardian.Log("start: set memory core stabilize with sunstation");
            while (true) {
                var memoryCoreObj = GameObject.Find(MEMORY_CORE_STABILIZE_WITH_SUNSTATION_PATH);
                if(memoryCoreObj) {
                    var memoryCore = memoryCoreObj.AddComponent<MemoryCore>();
                    memoryCore._style = MemoryCore.Style.STABILIZE_WITH_SUN_STATION;
                    memoryCoreObj.transform.Find("memory_space/memory_zerogravity").gameObject.SetActive(false);
                    memoryCoreObj.transform.Find("memory_space/audio_stabilizewithss").gameObject.SetActive(false);
                    memoryCoreObj.transform.Find("memory_space/reveal_memory").gameObject.SetActive(false);
                    memoryCore._disabledObjs = new List<GameObject> {
                        energyStabilizer.transform.Find("upper_block").gameObject,
                    };
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: set memory core stabilize with sunstation");

            Guardian.Log("start: find outside surface");
            GameObject sunOutsideSurface;
            while(true) {
                sunOutsideSurface = GameObject.Find(CORE_OUTSIDE_SURFACE_PATH);
                if(sunOutsideSurface) {
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: find outside surface");

            Guardian.Log("start: set plasmacloaking on hiddenhatch");
            GameObject hiddenHatchObj;
            while(true) {
                hiddenHatchObj = GameObject.Find(HIDDEN_HATCH_PATH);
                if(hiddenHatchObj) {
                    hiddenHatchObj.transform.Find("PlasmaCloaking").gameObject.AddComponent<PlasmaCloaking>()._cloakedObj = hiddenHatchObj.transform.Find("HiddenObjs").gameObject;
                    var hiddenHatchSpace = hiddenHatchObj.transform.Find("HiddenObjs/smooth_sphere_inside").gameObject.AddComponent<HiddenHatchSpace>();
                    hiddenHatchSpace._disabledObjs = new List<GameObject> { sunHeatVolume, sunInnerDestructionVolume, sunDestructionFluidVolume, sunGravityWell, sunAudio };
                    hiddenHatchSpace._blockEntrance = hiddenHatchObj.transform.Find("HiddenObjs/BlockEntrance").gameObject;
                    hiddenHatchSpace._outerSurface = sunOutsideSurface;
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: set plasmacloaking on hiddenhatch");

            Guardian.Log("start: set robot");
            Robot robot;
            while(true) {
                var robotObj = GameObject.Find(ROBOT_PATH);
                if(robotObj) {
                    GameObject dummyPos;
                    while(true) {
                        dummyPos = GameObject.Find("DummyInitialPosofRobot_Body");
                        if(dummyPos) {
                            break;
                        }
                        yield return null;
                    }
                    robot = robotObj.AddComponent<Robot>();
                    robot._hatchCylinder = hiddenHatchObj.transform.Find("HiddenObjs/cylinder_empty_thick");
                    robot._dummyInitialPos = dummyPos.transform;
                    robot._coreSunSector = energyStabilizer.transform.parent.parent;
                    robot.Initialize();
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: set robot");

            Guardian.Log("start: set memory core hidden hatch");
            while (true) {
                var memoryCoreObj = GameObject.Find(MEMORY_CORE_HIDDEN_HATCH_PATH);
                if(memoryCoreObj) {
                    var memoryCore = memoryCoreObj.AddComponent<MemoryCore>();
                    memoryCore._style = MemoryCore.Style.HIDDEN_HATCH;
                    memoryCoreObj.transform.Find("memory_space/memory_zerogravity").gameObject.SetActive(false);
                    memoryCoreObj.transform.Find("memory_space/audio_hiddenhatch").gameObject.SetActive(false);
                    memoryCoreObj.transform.Find("memory_space/reveal_memory").gameObject.SetActive(false);
                    memoryCore._disabledObjs = new List<GameObject> {
                        waitingArea.transform.Find("scaffold (1)").gameObject,
                        waitingArea.transform.Find("BrokenRobot").gameObject,
                        waitingArea.transform.Find("BrokenRobot (1)").gameObject,
                        waitingArea.transform.Find("BrokenRobot (2)").gameObject,
                        waitingArea.transform.Find("PlasmaWarp").gameObject,
                        robot._robotLocal.transform.Find("HiddenObjs").gameObject,
                    };
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: set memory core hidden hatch");

            Guardian.Log("start: set memory core lantern hit");
            while (true) {
                var memoryCoreObj = GameObject.Find(MEMORY_CORE_LANTERN_HIT_PATH);
                if(memoryCoreObj) {
                    var memoryCore = memoryCoreObj.AddComponent<MemoryCore>();
                    memoryCore._style = MemoryCore.Style.LANTERN_HIT;
                    memoryCoreObj.transform.Find("memory_space/memory_zerogravity").gameObject.SetActive(false);
                    memoryCoreObj.transform.Find("memory_space/audio_lanternhit").gameObject.SetActive(false);
                    memoryCoreObj.transform.Find("memory_space/reveal_memory").gameObject.SetActive(false);
                    memoryCore._disabledObjs = new List<GameObject> {
                        coreUp.transform.Find("scaffold (33)").gameObject,
                    };
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: set memory core lantern hit");

            Guardian.Log("start: set memory core final th");
            while (true) {
                var memoryCoreObj = GameObject.Find(MEMORY_CORE_FINAL_TH_PATH);
                if(memoryCoreObj) {
                    var memoryCore = memoryCoreObj.AddComponent<MemoryCore>();
                    memoryCore._style = MemoryCore.Style.FINAL_TH;
                    memoryCoreObj.transform.Find("memory_space/memory_zerogravity").gameObject.SetActive(false);
                    memoryCoreObj.transform.Find("memory_space/audio_finalth").gameObject.SetActive(false);
                    memoryCoreObj.transform.Find("memory_space/reveal_memory").gameObject.SetActive(false);
                    memoryCore._disabledObjs = new List<GameObject> {
                        coreCenter.transform.Find("robot").gameObject,
                        coreOfCore,
                    };
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: set memory core final th");

            Guardian.Log("start: set energy sphere item");
            SphereItem energySphereItem;
            while(true) {
                var energySphereObj = GameObject.Find(ENERGY_SPHERE_ITEM_PATH);
                if(energySphereObj) {
                    energySphereItem = energySphereObj.AddComponent<SphereItem>();
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: set energy sphere item");

            Guardian.Log("start: set core crush");
            while(true) {
                var coreCrush = GameObject.Find(CORE_CORE_CRUSH_PATH);
                if(coreCrush) {
                    var finalWarpController = coreCrush.AddComponent<FinalWarpController>();
                    finalWarpController._plasmaWarp = finalWarp;
                    finalWarpController._sphereItem = energySphereItem;
                    while(true) {
                        var comet = GameObject.Find(COMET_PATH);
                        if(comet) {
                            finalWarpController._comet = comet;
                            break;
                        }
                        yield return null;
                    }
                    while(true) {
                        var reveal = GameObject.Find(REVEAL_CRUSH_INTERLOPER);
                        if(reveal) {
                            finalWarpController._soundReveal = reveal;
                            break;
                        }
                        yield return null;
                    }
                    while(true) {
                        var reveal = GameObject.Find(REVEAL_LOW_CORE);
                        if(reveal) {
                            finalWarpController._seeReveal = reveal;
                            break;
                        }
                        yield return null;
                    }
                    finalWarpController.Initialize();
                    break;
                }
                yield return null;
            }
            Guardian.Log("end: set core crush");
        }
    }
}
