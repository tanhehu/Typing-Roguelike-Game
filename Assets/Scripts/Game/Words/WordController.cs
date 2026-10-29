using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordController : MonoBehaviour
{
    public Image image;
    public Text text;
    public IWordHolder holder;
    private Vector3 pos => holder.GameObject().transform.position;

    private void Start()
    {
        holder.destroy += Destroy;                     // Add destroy function to event trigger
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(pos + holder.wordOffset);       // Set and translate canvas to Screen (UI) position that follow the target + offset
    }

    public void Destroy()
    {
        this.gameObject.SetActive(false);
        holder.destroy -= Destroy;                     // Remove destroy function to event trigger
        holder = null;
    }
}
