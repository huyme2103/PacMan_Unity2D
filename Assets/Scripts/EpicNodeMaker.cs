using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicNodeMaker : MonoBehaviour
{
    public Node nodePrefab;
    public List<Node> nodeList;
    [ContextMenu("Create Nodes")]
    public void MakeNodes()
    {
        for (int x = -13; x < 13; x += 1)
        {
            for (int y = -16; y < 13; y += 1)
            {
                Node n = Instantiate(nodePrefab, new Vector2(x, y - 0.5f), Quaternion.identity);
                nodeList.Add(n);
            }
        }
    }

    [ContextMenu("Remove Empty Nodes")]
    public void RemoveNodes()
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i] == null)
            {
                nodeList.RemoveAt(i);
                i--;
            }
        }
    }

    [ContextMenu("Connect Nodes")]
    public void ConnectNodes()
    {

        for (int i = 0; i < nodeList.Count; i++)
        {
            for (int j = i + 1; j < nodeList.Count; j++)
            {
                if (Vector2.Distance(nodeList[i].transform.position, nodeList[j].transform.position) <= 1.3f)
                {
                    nodeList[i].connections.Add(nodeList[j]);
                    nodeList[j].connections.Add(nodeList[i]);
                }
            }
        }
    }
}
