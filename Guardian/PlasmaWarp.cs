using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Guardian {
    public class PlasmaWarp : MonoBehaviour {
        void OnTriggerEnter(Collider other) {
            if(other.gameObject == Locator._playerBody.gameObject) {
                Patch._canBeDamaged = false;
                // TODO: warping
            }
        }
    }
}
