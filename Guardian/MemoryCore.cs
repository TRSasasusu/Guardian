using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Guardian {
    public class MemoryCore : MonoBehaviour {
        public enum Style {
            CAUSE_SUPERNOVA,
            ENCOUNTER_SUPERNOVA,
            SUN_STATE_LOG,
            STABILIZE_WITH_SUN_STATION,
            HIDDEN_HATCH,
            LANTERN_HIT,
        }
        public Style _style;
        public Coroutine _memorySpaceAnimationCoroutine;
        public List<GameObject> _disabledObjs;

        MemorySpace _memorySpace;

        void Start () {
            _memorySpace = transform.Find("memory_space").gameObject.AddComponent<MemorySpace>();
            _memorySpace._memoryCore = this;
            _memorySpace._objs = _disabledObjs;
            _memorySpace.gameObject.SetActive(false);
        }

        //void OnCollisionEnter(Collision collision) {
        void OnTriggerEnter(Collider other) {
            //Guardian.Log("call oncollisionenter in memory core");
            Guardian.Log("call ontriggerenter in memory core");
            if(other.gameObject == Locator._playerBody.gameObject) {
                if(_memorySpaceAnimationCoroutine == null) {
                    Guardian.Log("call Animation in memory core");
                    _memorySpaceAnimationCoroutine = StartCoroutine(_memorySpace.Animation());
                }
            }
        }
    }
}
