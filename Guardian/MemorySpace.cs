using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Guardian {
    public class MemorySpace : MonoBehaviour {
        public List<Collider> _colliders;

        void OnTriggerEnter(Collider other) {
            if(other.gameObject == Locator._playerBody.gameObject) {
                Guardian.Log("player enters");
                foreach(var collider in _colliders) {
                    if(collider) {
                        collider.enabled = false;
                    }
                }
            }
        }

        void OnTriggerExit(Collider other) {
            if(other.gameObject == Locator._playerBody.gameObject) {
                Guardian.Log("player exits");
                foreach(var collider in _colliders) {
                    if(collider) {
                        collider.enabled = true;
                    }
                }
            }
        }
    }
}
