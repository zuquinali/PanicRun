using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class terrainGeneration : MonoBehaviour {

    #region atributos publicos
    public GameObject[] _terrainParts;      // partes que compoe o terreno
    public GameObject _character;           // personagem que estara em foco
    public Vector3 _baseTerrainPosition;    // posicao base para que seja gerado o terreno
    public int _lengthTerrain = 16;         // tamanho do array que armazenara o terreno em cena
    #endregion


    #region atributos privados
    private GameObject[] _terrainGenerated; // terreno em cena
    private int _index;                     // indice para que seja adicionado a nova parte do terreno
    private float _secondsToGenerate;       // intervalo em segundos para executar a funcao em loop
    #endregion


    #region Unity life cicle
    // Use this for initialization
    void Start () {
        this._terrainGenerated = new GameObject[ _lengthTerrain ];
        this._index = 0;
        this._secondsToGenerate = 0.5f;

        generateFirstTime();                // quando o jogo rodar pela primeira vez, monta o terreno
        //StartCoroutine(ExeWithWait());    // gerencia o terreno a cada _secondsToGenerate segundos

    }
	

	// Update is called once per frame
	void Update () {
        generateTerrainPart();
    }
    #endregion


    #region metodos
    public void generateFirstTime()
    {
        Vector3 position = new Vector3(this._character.transform.position.x, this._baseTerrainPosition.y, this._baseTerrainPosition.z);
        GameObject clone = new GameObject();

        for (int i = 0; i < this._lengthTerrain; i++)
        {
            if(i > 0)
            {
                position = new Vector3(this._terrainGenerated[i - 1].transform.position.x + this._terrainGenerated[i - 1].transform.localScale.x * 1.25f, this._terrainGenerated[i - 1].transform.position.y, this._terrainGenerated[i - 1].transform.position.z);
            }

            int partPosition = randomPosition();
            
            bool currentIsFall = this._terrainParts[partPosition].GetComponent<Rigidbody2D>() == null;
            bool previousIsFall = (this._index > 0) ? (this._terrainGenerated[this._index - 1].GetComponent<Rigidbody2D>() == null) : false;

            if(!currentIsFall || !previousIsFall && (this._character.transform.position.x != position.x))
            {
                clone = Instantiate(this._terrainParts[partPosition], position, Quaternion.identity);
            }
            else
            {
                while (currentIsFall || (this._character.transform.position.x != position.x))
                {
                    partPosition = randomPosition();
                    currentIsFall = this._terrainParts[partPosition].GetComponent<Rigidbody2D>() == null;
                }

                clone = Instantiate(this._terrainParts[partPosition], position, Quaternion.identity);
            }

            
            this._terrainGenerated[this._index] = clone;
            this._index++;
        }
    }


    public void generateTerrainPart()
    {
        this._index = this._lengthTerrain - 1;

        Vector3 position = new Vector3(this._terrainGenerated[this._index].transform.position.x + this._terrainGenerated[this._index].transform.localScale.x * 1.25f, this._terrainGenerated[this._index].transform.position.y, this._terrainGenerated[this._index].transform.position.z);
        int partPosition = randomPosition();

        GameObject clone = new GameObject();

        bool currentIsFall = this._terrainParts[partPosition].GetComponent<Rigidbody2D>() == null;
        bool previousIsFall = this._terrainGenerated[this._index].GetComponent<Rigidbody2D>() == null;

        if (!currentIsFall || !previousIsFall)
        {
            clone = Instantiate(this._terrainParts[partPosition], position, Quaternion.identity);
        }
        else
        {
            while (currentIsFall)
            {
                partPosition = randomPosition();
                currentIsFall = this._terrainParts[partPosition].GetComponent<Rigidbody2D>() == null;
            }

            clone = Instantiate(this._terrainParts[partPosition], position, Quaternion.identity);
        }

        this._terrainGenerated[this._index] = clone;
    }


    private IEnumerator ExeWithWait()
    {
        while(true)
        {
            yield return new WaitForSeconds( this._secondsToGenerate );
            generateTerrainPart();
        }
    }
    #endregion


    #region helper
    public int randomPosition()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(this._terrainParts.Length);
    }
    #endregion
}
