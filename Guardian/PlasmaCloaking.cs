using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Guardian {
    public class PlasmaCloaking : MonoBehaviour {
        public GameObject _cloakedObj;
        public GameObject _sunGravityWell;
        public GameObject _outerSurface;
        public GameObject _geometrySun;
        void OnTriggerEnter(Collider other) {
            if(Util.IsShip(other) || Util.IsPlayer(other)) {
                _cloakedObj.SetActive(true);

                if(_sunGravityWell) {
                    _sunGravityWell.SetActive(false);
                }
                var shipBody = other.GetComponentInParent<ShipBody>();
                if(shipBody) {
                    shipBody.SetVelocity(Vector3.zero);
                }
            }
        }

        void OnTriggerExit(Collider other) {
            if(Util.IsShip(other) || Util.IsPlayer(other)) {
                _cloakedObj.SetActive(false);

                if(_sunGravityWell && !_cloakedObj.transform.Find("BlockEntrance").gameObject.activeSelf) {
                    _sunGravityWell.SetActive(true);
                }
                _outerSurface.GetComponent<Collider>().enabled = true;
                if(_geometrySun) {
                    _geometrySun.SetActive(true);
                }
            }
        }
    }
}
