using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Guardian {
    public class SphereItem : OWItem {
        public const ItemType GuardianSphere = (ItemType)2048;

        public override void Awake() {
            base.Awake();
            _type = GuardianSphere;
        }

        public override string GetDisplayName() {
            return "Energy Sphere";
        }

        void Update() {
            if(transform.parent && transform.parent.name == "ItemSocket") {
                transform.localScale = Vector3.one * 0.2f;
            }
            else if(transform.parent && transform.parent.name == "HiddenObjs") {
                transform.localScale = Vector3.one * 0.1f;
            }
            else {
                transform.localScale = Vector3.one;
            }
        }
    }
}
