using System.Collections.Generic;
public static class Data
{
    private static int kills;
    private static List<string> materials;

    public static List<string> dummy = new List<string>(new string[] { "AIRSLATE GALENA", "AIRSLATE PYRITE", "AIRSLATE RAME", "AIRSLATE TALC" });

    public static int Kills 
    {
        get 
        {
            return kills;
        }
        set 
        {
            kills = value;
        }
    }

    public static void setMateria(string mat){
        materials.Add(mat);
    }

    public static List<string> getMaterials(){
        return dummy;
    }
}