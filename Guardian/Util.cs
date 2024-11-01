using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Guardian {
    public static class Util {
        public static bool IsShip(Collider other) {
            return other.GetComponentInParent<ShipBody>();
        }

        public static bool IsPlayer(Collider other) {
            return other.gameObject == Locator._playerBody.gameObject;
        }
    }
}
