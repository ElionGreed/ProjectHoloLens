using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class MovePlayer : MonoBehaviour
{

    Unit unit;
    public Button button;
    bool isMoving;
    bool hasMoved = false;
    Animator anim;
    Vector3 vel;
    Vector3 _prevPosition;
    GameObject player;
    [SerializeField]
    Camera camera;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    public void Initialize()
    {
        player = UnitManager.unitManager.playerCharacter;
        anim = player.GetComponent<Animator>();
        unit = player.gameObject.GetComponent<Unit>();
        button.interactable = false;
        hasMoved = false;
        StartCoroutine(Move());
    }
    // Update is called once per frame
    private IEnumerator Move()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        while (hasMoved == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    Cursor.SetCursor(null, Vector2.zero, cursorMode);
                    PathRequestManager.RequestPath(player.transform.position, hit.point, unit.OnPathFound);
                    StartCoroutine(Wait());
                    hasMoved = true;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Cursor.SetCursor(null, Vector2.zero, cursorMode);
                button.interactable = true;
                hasMoved = true; //factualy false but is needed to exit loop
            }
            yield return null;
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(FaceMovementDirection());
    }
    private IEnumerator FaceMovementDirection()
    {

        while (transform.hasChanged)
        {
            vel = (transform.position - _prevPosition) / Time.deltaTime;
            _prevPosition = transform.position;
            transform.hasChanged = false;
            if (vel != Vector3.zero)
            {
                isMoving = true;
                anim.SetBool("isMoving", true);
                transform.rotation = Quaternion.LookRotation(vel, Vector3.up);
            }
            else
            {
                isMoving = false;
                anim.SetBool("isMoving", false);
            }
            yield return null;
        }  
    }

}

