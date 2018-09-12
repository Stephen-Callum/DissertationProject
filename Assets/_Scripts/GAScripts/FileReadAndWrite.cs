using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//[Serializable]
public static class FileReadAndWrite
{
	public static void WriteToXMLFile(string filePath, GeneticSaveData objectToWrite)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(GeneticSaveData));
        using (var streamWriter = new StreamWriter(File.Open(filePath, FileMode.Create)))
        {
            xmlSerializer.Serialize(streamWriter, objectToWrite);
        }
    }

    public static GeneticSaveData ReadFromBinaryFile(string filePath)
    {
        XmlSerializer xmlDeserializer = new XmlSerializer(typeof(GeneticSaveData));
        using (var streamReader = new StreamReader(File.Open(filePath, FileMode.Open)))
        {
            return xmlDeserializer.Deserialize(streamReader) as GeneticSaveData;
        }
    }
}
