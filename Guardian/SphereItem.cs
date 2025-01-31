using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Guardian {
    public class SphereItem : OWItem {
        public static SphereItem PickedUpSphereItem;

        public const ItemType GuardianSphere = (ItemType)2048;

        public override void Awake() {
            base.Awake();
            _type = GuardianSphere;
        }

        public override string GetDisplayName() {
            return Guardian.newHorizons.GetTranslationForUI("Energy Sphere");
        }

        public override void PickUpItem(Transform holdTranform) {
            base.PickUpItem(holdTranform);
            PickedUpSphereItem = this;
        }

        public override void DropItem(Vector3 position, Vector3 normal, Transform parent, Sector sector, IItemDropTarget customDropTarget) {
            base.DropItem(position, normal, parent, sector, customDropTarget);
            PickedUpSphereItem = null;
        }

        public void Remove(Sector sector) {
            var itemTool = Locator.GetPlayerCamera().GetComponentInChildren<ItemTool>();
            if(itemTool && itemTool.GetHeldItem() != null) {
                itemTool._waitForUnsocketAnimation = false;
                itemTool.DropItemInstantly(sector, transform);
            }
            Destroy(gameObject);
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
