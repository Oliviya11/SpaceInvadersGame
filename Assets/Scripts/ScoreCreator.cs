using UnityEngine;
using UnityEngine.UI;

public struct ScoreInfo
{
    public int score;
    public Vector3 pos;
    public Color color;
}

public class ScoreCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;

    [SerializeField] 
    private GameObject prefabScore;

    public void createScore(ScoreInfo scoreInfo)
    {
        GameObject label = null;
        
        label = Instantiate(prefabScore, scoreInfo.pos, Quaternion.identity, parent.transform);
        Text textMesh = label.GetComponent<Text>();
        textMesh.text = "+"+scoreInfo.score;
        textMesh.color = scoreInfo.color;
    }
}
