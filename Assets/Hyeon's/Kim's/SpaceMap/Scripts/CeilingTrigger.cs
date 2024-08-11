using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CeilingTrigger : MonoBehaviour
{
    public GameObject[] Ceilings;
    public Material m_Material;
    public Material transparent;
    public Material Half_transparent;

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Ceiling");
        Ceilings = new GameObject[obj.transform.childCount];
        for(int i = 0; i < Ceilings.Length; i++)
        {
            Ceilings[i] = obj.transform.GetChild(i).gameObject;
        }
    }

    private void MakeObjectTransparent(GameObject obj)
    {
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = transparent;
        }
    }
    private void MakeObjectHalfTransparent(GameObject obj)
    {
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = Half_transparent;
        }
    }
    private void MakeObjectInvisible(GameObject obj) 
    {
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = m_Material;
        }
    }

    internal void OnChildTriggerEnter(Collider other, ChildCollisionHandler child)
    {
        
        if (other.transform.parent.CompareTag("Ceiling"))
        {
            if (child.CompareTag("Player"))
            {
                foreach (GameObject ceiling in Ceilings)
                {
                    if (other.gameObject == ceiling)
                    {
                        MakeObjectTransparent(ceiling);
                        break; // 배열에서 오브젝트를 찾으면 더 이상 반복하지 않음
                    }
                }
            }
            else if (child.CompareTag("GameController"))
            {
                foreach (GameObject ceiling in Ceilings)
                {
                    if (other.gameObject == ceiling)
                    {
                        MakeObjectHalfTransparent(ceiling);
                        break; // 배열에서 오브젝트를 찾으면 더 이상 반복하지 않음
                    }
                }
            }
        }
        //throw new NotImplementedException();
    }

    internal void OnChildTriggerStay(Collider other, ChildCollisionHandler child)
    {
        
        if (other.transform.parent.CompareTag("Ceiling"))
        {
            if (child.CompareTag("Player"))
            {
                foreach (GameObject ceiling in Ceilings)
                {
                    if (other.gameObject == ceiling)
                    {
                        MakeObjectTransparent(ceiling);
                        break; // 배열에서 오브젝트를 찾으면 더 이상 반복하지 않음
                    }
                }
            }
            else if (child.CompareTag("GameController"))
            {
                if(other.material == Half_transparent)
                {
                    foreach (GameObject ceiling in Ceilings)
                    {
                        if (other.gameObject == ceiling)
                        {
                            MakeObjectHalfTransparent(ceiling);
                            break; // 배열에서 오브젝트를 찾으면 더 이상 반복하지 않음
                        }
                    }
                }
            }
        }
        //throw new NotImplementedException();
    }

    internal void OnChildTriggerExit(Collider other, ChildCollisionHandler child)
    {
        
        if (other.transform.parent.CompareTag("Ceiling"))
        {
            if (child.CompareTag("Player"))
            {
                foreach (GameObject ceiling in Ceilings)
                {
                    if (other.gameObject == ceiling)
                    {
                        MakeObjectHalfTransparent(ceiling);
                        break; // 배열에서 오브젝트를 찾으면 더 이상 반복하지 않음
                    }
                }
            }
            else if (child.CompareTag("GameController"))
            {
                foreach (GameObject ceiling in Ceilings)
                {
                    if (other.gameObject == ceiling)
                    {
                        MakeObjectInvisible(ceiling);
                        break; // 배열에서 오브젝트를 찾으면 더 이상 반복하지 않음
                    }
                }
            }
        }
       //throw new NotImplementedException();
    }
}
