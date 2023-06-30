using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Canvas;
    public bool Reset = false;
    public bool TueEnnemie = false;

    private void Update()
    {
        if (Reset)
        {
            Vector2 thisPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 thisSize = new Vector2(transform.localScale.x, transform.localScale.y);
            Bounds thisBounds = new Bounds(thisPosition, thisSize);

            Vector2 playerPosition = new Vector2(Player.transform.position.x, Player.transform.position.y);
            if (thisBounds.Contains(playerPosition))
            {
                Canvas.GetComponent<LevelManager>().ResetPlayerPos();
            }
        }

        if (TueEnnemie)
        {
            Vector2 thisPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 thisSize = new Vector2(transform.localScale.x, transform.localScale.y);
            Bounds thisBounds = new Bounds(thisPosition, thisSize);

            GameObject[] Ennemies = GameObject.FindGameObjectsWithTag("Ennemie");
            foreach (var Ennemie in Ennemies)
            {
                Vector2 ennemiePosition = new Vector2(Ennemie.transform.position.x + Ennemie.GetComponent<BoxCollider2D>().offset.x, Ennemie.transform.position.y+ Ennemie.GetComponent<BoxCollider2D>().offset.y);
                Vector2 ennemieSize = Ennemie.GetComponent<BoxCollider2D>().size;
                Bounds ennemieBounds = new Bounds(ennemiePosition, ennemieSize);
            
                if (thisBounds.Intersects(ennemieBounds))
                {
                    Destroy(Ennemie);
                }
            }
        }
    }
}