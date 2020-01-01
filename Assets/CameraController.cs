using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 mouse_position;
    Vector3 old_position;
    Vector3 rotate_amount_x;
    Vector3 rotate_amount_y;
    float width = (float) Screen.width;
    float height = (float) Screen.height;
    float fracx;
    float fracy;
    float zoom_amount;
    float zoom_frac;
    Transform pitch;
    Transform camera;

    public int sensitivity = 200;
    public float zoom_sensitivity = .5f;

    bool down = false;

    // Start is called before the first frame update
    void Start()
    {
        pitch = gameObject.transform.GetChild(0);       
        camera = pitch.GetChild(0);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressed secondary button.");
            mouse_position.x = Input.mousePosition.x / width;
            mouse_position.y = Input.mousePosition.y / height;
            old_position = mouse_position;
            down = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Released secondary button.");
            down = false;
        }
        if (down)
        {
            // The mouse is down, we want to be moving the camera if old is different from
            // current
            mouse_position.x = Input.mousePosition.x / width;
            mouse_position.y = Input.mousePosition.y / height;
            if (mouse_position != old_position)
            {
                rotate_amount_x = new Vector3(0f, (mouse_position.x - old_position.x) * sensitivity, 0f);
                gameObject.transform.Rotate(rotate_amount_x, Space.World);
                rotate_amount_y = new Vector3((old_position.y - mouse_position.y) * sensitivity, 0f, 0f); 

                if (pitch.eulerAngles.x + rotate_amount_y.x > 44 &&
                    pitch.eulerAngles.x + rotate_amount_y.x < 180)
                {
                    pitch.Rotate(new Vector3(45f - pitch.eulerAngles.x, 0f, 0f));
                }
                else if (pitch.eulerAngles.x + rotate_amount_y.x < 316 &&
                         pitch.eulerAngles.x + rotate_amount_y.x > 180)

                {
                    pitch.Rotate(new Vector3(-45f - pitch.eulerAngles.x, 0f, 0f));
                }
                else
                    pitch.Rotate(rotate_amount_y);
            }
            old_position = mouse_position;
            fracx = Mathf.Abs(rotate_amount_x.y / 20f);
            fracy = Mathf.Abs(rotate_amount_y.x / 20f);
        }
        else
        {
            if (rotate_amount_x.y != 0)
            {
                // The mouse is not down, we want to gradually slow down the previous rotation
                if (rotate_amount_x.y < fracx && rotate_amount_x.y > - fracx)
                    rotate_amount_x.y = 0;
                else if (rotate_amount_x.y < 0)
                    rotate_amount_x.y = rotate_amount_x.y + fracx;
                else
                    rotate_amount_x.y = rotate_amount_x.y - fracx;

                gameObject.transform.Rotate(rotate_amount_x);
            }

            if (rotate_amount_y.x != 0)
            {
                if (rotate_amount_y.x < fracy && rotate_amount_y.x > -fracy)
                    rotate_amount_y.x = 0;
                else if (rotate_amount_y.x < 0)
                {
                    rotate_amount_y.x = rotate_amount_y.x + fracy;
                }
                else
                {
                    rotate_amount_y.x = rotate_amount_y.x - fracy;
                }

                Debug.Log(rotate_amount_y.x);



                if (pitch.eulerAngles.x + rotate_amount_y.x > 45 &&
                    pitch.eulerAngles.x + rotate_amount_y.x < 180)

                {
                    pitch.Rotate(new Vector3(45f - pitch.eulerAngles.x, 0f, 0f));
                }
                else if (pitch.eulerAngles.x + rotate_amount_y.x < 315 &&
                         pitch.eulerAngles.x + rotate_amount_y.x > 180)

                {
                    pitch.Rotate(new Vector3(-45f - pitch.eulerAngles.x, 0f, 0f));
                }
                else
                    pitch.Rotate(rotate_amount_y);
            }
        }

        // We also check the scroll wheel delta and zoom accordingly from 3 to 30
        if (Input.mouseScrollDelta.y != 0)
        {
            float input = -Input.mouseScrollDelta.y;
            if (input < 0)
                zoom_amount = (input * zoom_sensitivity * (camera.localPosition.z + (input * zoom_sensitivity * camera.localPosition.z)));
            else
                zoom_amount = (input * zoom_sensitivity * camera.localPosition.z);
            zoom_frac = Mathf.Abs(zoom_amount / 5);
        }

        Debug.Log("ZOOM: " + zoom_amount);

        if (zoom_amount != 0)
        {
            if (zoom_amount < zoom_frac &&  zoom_amount > - zoom_frac)
                zoom_amount = 0;
            else if (zoom_amount < 0)
                zoom_amount += zoom_frac;
            else
                zoom_amount -= zoom_frac;

            if (camera.localPosition.z + zoom_amount < -30)
                camera.Translate(new Vector3(0f, 0f, -30 - camera.localPosition.z));
            else if (camera.localPosition.z + zoom_amount > -3)
                camera.Translate(new Vector3(0f, 0f, -3 - camera.localPosition.z));
            else
                camera.Translate(new Vector3(0f, 0f, zoom_amount));
        }
    }
}
