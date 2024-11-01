using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Guardian {
    public class PlasmaCloaking : MonoBehaviour {
        public GameObject _cloakedObj;

        void OnTriggerEnter(Collider other) {
            if(Util.IsShip(other) || Util.IsPlayer(other)) {
                _cloakedObj.SetActive(true);
            }
        }

        void OnTriggerExit(Collider other) {
            if(Util.IsShip(other) || Util.IsPlayer(other)) {
                _cloakedObj.SetActive(false);
            }
        }
    }
}
