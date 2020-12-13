using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCtrl : MonoBehaviour
{
    RaycastHit hit;
    float MaxDistance = 1.0f;

    public string InteractionObject = null;

    private Transform _selection;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_selection != null)
        {
            GlowObject selectionGlowObject = _selection.GetComponent<GlowObject>();
            selectionGlowObject.OnRaycastExit();
            _selection = null;
            InteractionObject = null;
        }

        Debug.DrawRay(transform.position, transform.forward * MaxDistance, Color.blue, 0.3f);
        if (Physics.Raycast(transform.position, transform.forward, out hit, MaxDistance))
        {
            if (hit.transform.CompareTag("INTERACTION"))
            {
                var selection = hit.transform;

                GlowObject selectionGlowObject = selection.GetComponent<GlowObject>();

                selectionGlowObject.OnRaycastEnter();

                _selection = selection;

                InteractionObject = hit.collider.name;
            }
            Debug.Log(hit.collider.name);
        }
    }
}
