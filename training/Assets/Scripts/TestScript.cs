using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class TestScript : MonoBehaviour {

    private void Start()
    {
        TestAA();
    }
    static void TestAA()
    {
        // Write each directory name to a file.
        using (StreamWriter sw = new StreamWriter("Assets/writeTest.cs"))
        {
            sw.WriteLine("using UnityEngine;");
            sw.WriteLine("[System.Serializable]");
            sw.WriteLine("public class writeTest : MonoBehaviour {");
            sw.WriteLine("}");

            sw.Close();
        }

        //// Read and show each line from the file.
        //string line = "";
        //using (StreamReader sr = new StreamReader("CDriveDirs.txt"))
        //{
        //    while ((line = sr.ReadLine()) != null)
        //    {
        //        Debug.Log(line);
        //    }
        //}
    }
}
