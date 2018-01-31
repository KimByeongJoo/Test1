using UnityEngine;
using System.Collections;
using System.IO;
using LumenWorks.Framework.IO.Csv;

public class MyCsvLoad : MonoBehaviour {
	[SerializeField]
	GameObject button_Prefab;

    [SerializeField]
    UIGrid grid;

    CsvReader reader;

	void Start()
	{
		LoadServers ();
	}

	void LoadServers()
	{
		TextAsset textAsset = Resources.Load<TextAsset> ("servers") as TextAsset;

		MemoryStream ms = new MemoryStream (textAsset.bytes);
		reader = new CsvReader (new StreamReader (ms), true);

		string[] headers = reader.GetFieldHeaders ();
        int index_id = System.Array.IndexOf(headers, "id");
        int index_name = System.Array.IndexOf(headers, "name");

        while (reader.ReadNextRecord ()) {			
			var go = NGUITools.AddChild (grid.gameObject, button_Prefab);

            ShowServerName comp = go.GetComponent<ShowServerName>();

            comp.Set(reader[index_id], reader[index_name]);
           /* 
			int r = Random.Range (10, 50);
			int r2 = Random.Range (40000, 60000);
			go.transform.Find ("label_top_second").GetComponent<UILabel> ().text = "[00ff00]원활[-]\n[606060]Lv" + r + "킹덤 " + r2;
            */
			// 0 == id
			// 1 = name
		}
		grid.Reposition ();
	}
}