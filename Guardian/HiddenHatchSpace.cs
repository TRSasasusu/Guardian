using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Guardian {
    public class HiddenHatchSpace : MonoBehaviour {
        public List<GameObject> _disabledObjs;
        public GameObject _blockEntrance;
        public GameObject _outerSurface;

        void OnTriggerEnter(Collider other) {
            if(Util.IsShip(other) || Util.IsPlayer(other)) {
                if(_disabledObjs != null) {
                    foreach(var obj in _disabledObjs) {
                        if(obj) {
                            obj.SetActive(false);
                        }
                    }
                }
                if(_blockEntrance) {
                    _blockEntrance.SetActive(true);
                }
                if(_outerSurface) {
                    _outerSurface.GetComponent<Collider>().enabled = false;
                }

                var shipBody = other.GetComponentInParent<ShipBody>();
                if(shipBody) {
                    shipBody.SetVelocity(Vector3.zero);
                }
            }
        }

        void OnTriggerExit(Collider other) {
            if (Util.IsShip(other) || Util.IsPlayer(other)) {
                gameObject.SetActive(false);
                if(_outerSurface) {
                    _outerSurface.GetComponent<Collider>().enabled = true;
                }
            }
        }
    }
}
