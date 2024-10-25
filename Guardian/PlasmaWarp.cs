using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Guardian {
    public class PlasmaWarp : MonoBehaviour {
        List<Transform> _points;
        List<ParticleSystem> _beams;

        public void Initialize(IEnumerable<Transform> points) {
            _points = points.ToList();

            _beams = new List<ParticleSystem>();
            var plasmaBeam = transform.Find("PlasmaBeam").gameObject;
            _beams.Add(plasmaBeam.GetComponent<ParticleSystem>());
            for(var i = 1; i < _points.Count; i++) {
                var copyPlasmaBeam = Instantiate(plasmaBeam, transform);
                _beams.Add(copyPlasmaBeam.GetComponent<ParticleSystem>());
            }
        }

        void Update() {
            for(var i = 0; i < _beams.Count; i++) {
                if(i > 0) {
                    _beams[i].transform.position = _points[i - 1].transform.position;
                }
                _beams[i].transform.LookAt(_points[i]);
                var mainModule = _beams[i].main;
                mainModule.startLifetime = Vector3.Distance(_beams[i].transform.position, _points[i].transform.position) * 0.1f;
            }
        }

        void OnTriggerEnter(Collider other) {
            if(other.gameObject == Locator._playerBody.gameObject) {
                Patch._canBeDamaged = false;
                // TODO: warping
            }
        }
    }
}
