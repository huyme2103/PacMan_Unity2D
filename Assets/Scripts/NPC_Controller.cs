using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    public Node currentNode;
    public List<Node> path = new List<Node>();
    private int currentIndex = 0;
    private Pacman pacman;
    private bool isDestroyed = false;
    private Vector3 startPosition;

    public enum StateMachine
    {
        Patrol,
        Engage,
        Evade
    }

    public StateMachine currentState;
    public Pacman player;
    public float speed = 4f;

    private void Start()
    {
        pacman = FindObjectOfType<Pacman>();
        startPosition = transform.position;
    }

    private void Update()
    {
        if (player == null || AStarManager.instance == null) return;

        switch (currentState)
        {
            case StateMachine.Patrol:
                Patrol();
                break;
            case StateMachine.Engage:
                Engage();
                break;
            case StateMachine.Evade:
                Evade();
                break;
        }

        bool playerSeen = Vector2.Distance(transform.position, player.transform.position) < 10.0f;


        if (!playerSeen && currentState != StateMachine.Evade && pacman.IsPoweredUp)
        {

            currentState = StateMachine.Evade;
            path.Clear();
        }
        else if (playerSeen && currentState != StateMachine.Engage && !pacman.IsPoweredUp)
        {
            currentState = StateMachine.Engage;
            path.Clear();
        }
        else if (!playerSeen && currentState != StateMachine.Patrol && !pacman.IsPoweredUp)
        {
            currentState = StateMachine.Patrol;
            path.Clear();
        }
        CreatePath();
    }

    void Patrol()
    {
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.AllNodes()[Random.Range(0, AStarManager.instance.AllNodes().Length)]);
            speed = 5;
            currentIndex = 0;
        }
    }

    void Engage()
    {
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(player.transform.position));
            speed = 7;
            currentIndex = 0;
        }
    }

    void Evade()
    {
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindFurthestNode(player.transform.position));
            speed = 6;
            currentIndex = 0;
        }
    }


    public void CreatePath()
    {
        if (path.Count > 0)
        {

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[currentIndex].transform.position.x, path[currentIndex].transform.position.y, -2), speed * Time.deltaTime);


            if (Vector2.Distance(transform.position, path[currentIndex].transform.position) < 0.1f)
            {
                currentNode = path[currentIndex];
                currentIndex++;


                if (currentIndex >= path.Count)
                {
                    path.Clear();
                    currentIndex = 0;
                }
            }
        }
    }


    public void DestroyGhost()
    {
        isDestroyed = true;
        StartCoroutine(RespawnGhost());
    }


    private IEnumerator RespawnGhost()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(6f);

        transform.position = startPosition;
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        isDestroyed = false;
        currentState = StateMachine.Patrol;
        path.Clear();
    }
}
