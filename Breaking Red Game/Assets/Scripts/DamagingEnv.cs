using UnityEngine;

public class DamagingEnv : MonoBehaviour
{
    public int damage = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
        }
    }

    public void death()
    {
        Debug.Log("Player died moving over an edge!");
        Application.Quit();//quitting the game 
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
