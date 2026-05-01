using UnityEngine;

public class Talk : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue()
    {

        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
    }
}
