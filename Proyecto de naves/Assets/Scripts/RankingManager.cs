using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    private string connectionString;
    private List<HighScore> highScores = new List<HighScore>();

    public GameObject scorePrefabs;
    public Transform scoreParent;

    public int topRanks;
    public InputField enterName;

    public GameObject nameDialog;
    public bool gameOver;
    public int saveScores;
   
    public int score;

    public GameController gameController;
    public GameObject ranking;
    void Start()
    {
        connectionString = "URI=file:" + Application.dataPath + "/StreamingAssets/Ranking.sqlite";
        ranking.SetActive(false);
        
       
        DeleteExtraScore();
        
    }

   
    void Update()
    {
        
        gameOver = gameController.gameOver;
        score = gameController.score;

       
    }

    public void verRanking()
    {

        score = gameController.score;
        InsertScore(enterName.text, score);
        ShowScores();
        ranking.SetActive(true);
    }

    public void ReStart()
    {
        score = 0;
        enterName.text = string.Empty;
        
    }
    public void EnterName()
    {
        if (enterName.text != string.Empty)
        {
            //int score = UnityEngine.Random.Range(1, 500);
            //InsertScore(enterName.text , score);
            //enterName.text = string.Empty;
            nameDialog.SetActive(false);
        }
    }
 

   

 


    private void InsertScore(string name , int newScore)
    {
        
     
        
        
            GetScores();
            int hsCount = highScores.Count;

            if (highScores.Count > 0)
            {
                HighScore lowestScore = highScores[highScores.Count - 1];
                if (lowestScore != null && saveScores > 0 && highScores.Count >= saveScores && saveScores > lowestScore.Score)
                {
                    DeleteScore(lowestScore.ID);
                    hsCount--;
                }
            }
            if (hsCount < saveScores)
            {
                using (IDbConnection dbConnection = new SqliteConnection(connectionString))
                {
                    dbConnection.Open();

                    using (IDbCommand dbCmd = dbConnection.CreateCommand())
                    {
                        string sqlQuery = string.Format("INSERT INTO RankingDB(Name,Score) VALUES (\"{0}\", \"{1}\")", name, newScore);
                        dbCmd.CommandText = sqlQuery;
                        dbCmd.ExecuteScalar();
                        dbConnection.Close();


                    }
                }
            }
        
    }

    private void GetScores()
    {
        highScores.Clear();

        using(IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM RankingDB";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log(reader.GetString(1) + " " + reader.GetInt32(2));
                        highScores.Add(new HighScore(reader.GetInt32(0), reader.GetInt32(2), reader.GetString(1)));
                        
                    }
                    dbConnection.Close();
                    reader.Close();

                }
            }
            
        }
        highScores.Sort();


    }
    
    private void DeleteScore(int id)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = string.Format("DELETE FROM RankingDB WHERE PlayerID = \"{0}\"", id);
                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();


            }
        }
    }
   
    private void ShowScores()
    {
        GetScores();

        foreach (GameObject score in GameObject.FindGameObjectsWithTag("Score"))
        {
            Destroy(score);
        }
        {

        }
        for (int i = 0; i < topRanks; i++)
        {
            if (i <= highScores.Count - 1)
            {
                GameObject tmpObjec = Instantiate(scorePrefabs);
                HighScore tmpScore = highScores[i];

                tmpObjec.GetComponent<HighScoreScript>().SetScore(tmpScore.Name, tmpScore.Score.ToString(), "#" + (i + 1).ToString());

                tmpObjec.transform.SetParent(scoreParent);

                tmpObjec.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            }
           
        }
    }

    private void DeleteExtraScore()
    {
        GetScores();

        if (saveScores <= highScores.Count)
        {
            int deleteCount = highScores.Count - saveScores;
            highScores.Reverse();

            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                dbConnection.Open();



                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    for (int i = 0; i < deleteCount; i++)
                    {
                        string sqlQuery = string.Format("DELETE FROM RankingDB WHERE PlayerID = \"{0}\"", highScores[i].ID);
                        dbCmd.CommandText = sqlQuery;
                        dbCmd.ExecuteScalar();
                    }
                    dbConnection.Close();

                }
                
            }
        }
    }
}
