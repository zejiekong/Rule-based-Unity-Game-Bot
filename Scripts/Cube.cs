using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class Cube : MonoBehaviour
{
    [SerializeField]
    float speed = 10;
    // Update is called once per frame
    List<string> list = new List<string> { "Bomb", "Bomb (1)", "Bomb (2)", "Bomb (3)", "Bomb (4)", "Bomb (5)", "Bomb (6)" };
    
    void Start()
    {
        transform.position = new Vector3(0, (float) -2.5);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        SceneManager.LoadScene(0);
    }

    Vector3 BombAvoid()
    {
        List<float> x_position = new List<float> { };
        foreach (var obj in list)
        {
            var pos = GameObject.Find(obj).transform.position.x;
            x_position.Add(pos);
        }
        x_position.Sort();
        Dictionary<float,float> dict = new Dictionary<float, float> { };
        foreach (var obj in x_position)
        {
            //try
            //{
            //    var gap = x_position[x_position.IndexOf(obj) + 1] - obj;
            //    if (gap >= 1)
            //    {
            //        new_position = (x_position[x_position.IndexOf(obj) + 1] + obj) / 2;
            //    }
            //}
            //catch (ArgumentOutOfRangeException)
            //{
            //    new_position = 11;
            //}
            try
            {
                var gap = x_position[x_position.IndexOf(obj) + 1] - obj;
                if (gap >= 3)
                {
                    dict.Add(((x_position[x_position.IndexOf(obj) + 1] + obj) / 2),Math.Abs(((x_position[x_position.IndexOf(obj) + 1] + obj) / 2)-transform.position.x));
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                break;
            }

        }
        float min;
        if (dict.Count != 0)
        {
            min = 100;
            foreach (KeyValuePair<float, float> obj in dict)
            {
                if (obj.Value < min)
                {
                    min = obj.Key;
                }
            }
        }
        else
        {
            if (Math.Abs(transform.position.x - x_position[0]) < Math.Abs(transform.position.x - x_position[x_position.Count - 1]))
            {
                min = x_position[0] - 2;
            }
            else
            {
                min = x_position[x_position.Count - 1] + 2;
            }

            //if (transform.position.x > 0)
            //{
            //    min = (float) 11.5;
            //}
            //else
            //{
            //    min = (float) -11.5;
            //}
        }

        return new Vector3(min, (float) -2.5);
    }


    bool CollisionCheck()
    {
        float x_cube = transform.position.x;
        foreach (var obj in list)
        {
            var pos = GameObject.Find(obj).transform.position.x;
            if(pos > x_cube -4 && pos < x_cube + 4)
            {
                return true;
            }
        }
        return false;
    }

    bool MovementCheck()
    {
        foreach (var obj in list)
        {
            var pos = GameObject.Find(obj).transform.position.y;
            if(pos < 5)
            {
                return false;
            }
      
        }
        return true;
    }

    void Update()
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //Vector3 movement = new Vector3(horizontal,0);

        if (CollisionCheck() && MovementCheck())
        {
            Vector3 movement = BombAvoid() - transform.position;
            transform.position += movement * Time.deltaTime * speed;
        }
        //Vector3 movement = BombAvoid() - transform.position;
        //transform.position += movement * Time.deltaTime * speed;
    }




}
